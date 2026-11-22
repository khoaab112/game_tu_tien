using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace TuTien.Prototype
{
    public sealed class PrototypeMovement : NetworkBehaviour
    {
        public PrototypeCivilian Civilian;

        // QA fixture values for the networking spike.
        private const float TestWalkSpeed = 5.5f;
        private const float TestRunSpeed = 9f;
        private const int AutoRunAfterSeconds = 2;
        private Vector2 _serverInput;
        private Vector2 _localInput;
        private bool _serverWantsRun;
        private bool _localWantsRun;
        private int _walkedTicks;
        private readonly SyncVar<bool> _running = new();
        private float _lastInputTime;
        private Vector3 _initialPosition;
        private bool _reportedServerMovement;
        private bool _reportedClientMovement;
        private Vector3 _clientInitialPosition;
        private bool _artFixture;
        private string _lastArtFrame;
        private bool _smokeInput;
        private bool _runFixture;
        private bool _combatFixture;
        private bool _sentFixtureAttack;
        private float _nextAttackAt;
        private PrototypeDummy _target;
        private PrototypeVillage _villageCamera;
        private string _playerLabel;

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            _initialPosition = transform.position;
            _serverInput = Vector2.zero;
            _serverWantsRun = false;
            _walkedTicks = 0;
            _reportedServerMovement = false;
            _nextAttackAt = 0;
            _smokeInput = PrototypeSession.HasArgument("--smoke");
            _runFixture = _smokeInput && PrototypeSession.HasArgument("--qa-run");
            TimeManager.OnTick += Tick;
        }

        public override void OnStopNetwork()
        {
            TimeManager.OnTick -= Tick;
            base.OnStopNetwork();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            _clientInitialPosition = transform.position;
            _reportedClientMovement = false;
            _combatFixture = PrototypeSession.HasArgument("--qa-combat");
            _sentFixtureAttack = false;
            _localInput = Vector2.zero;
            _localWantsRun = false;
            _target = null;
            _playerLabel = IsOwner ? $"Player {ObjectId} (You)" : $"Player {ObjectId}";
            _artFixture = PrototypeSession.HasArgument("--qa-art");
            _lastArtFrame = null;
            if (Civilian != null) Civilian.Initialize();
            if (IsOwner && Camera.main != null && Camera.main.TryGetComponent(out _villageCamera))
                _villageCamera.Follow(transform);
            ReportArtPose("Idle");
            Debug.Log($"PROTOTYPE_VISIBLE object={ObjectId} owner={IsOwner}");
        }

        public override void OnStopClient()
        {
            if (_villageCamera != null)
            {
                _villageCamera.StopFollowing(transform);
                _villageCamera = null;
            }
            if (_smokeInput)
                Debug.Log($"PROTOTYPE_QA_DESPAWN object={ObjectId}");
            base.OnStopClient();
        }

        private void OnGUI()
        {
            if (!IsClientInitialized || Camera.main == null) return;
            var point = Camera.main.WorldToScreenPoint(transform.position + Camera.main.transform.up * 2.5f);
            if (point.z <= 0) return;
            GUI.Box(new Rect(point.x - 60, Screen.height - point.y - 25, 120, 24), _playerLabel);
            if (IsOwner)
                GUI.Label(new Rect(15, 42, 470, 24), _target == null
                    ? "Click the training dummy to select it"
                    : "Training dummy selected | Space: attack within 4 m");
        }

        private void Tick()
        {
            if (IsOwner) SubmitInput(_localInput, _localWantsRun);
            if (!IsServerInitialized) return;
            if (Time.unscaledTime - _lastInputTime > 0.5f) _serverInput = Vector2.zero;
            var automaticRun = _walkedTicks >= AutoRunAfterSeconds * TimeManager.TickRate;
            var wantsRun = _serverWantsRun || automaticRun;
            var position = PrototypeVillage.Move(transform.position, MovementStep(_serverInput, wantsRun));
            var moved = (position - transform.position).sqrMagnitude > 0.000001f;
            if (!moved || (_serverWantsRun && !automaticRun))
                _walkedTicks = 0;
            else if (!wantsRun)
                _walkedTicks++;
            _running.Value = wantsRun && moved;
            transform.position = position;
            if (!_reportedServerMovement && Vector3.Distance(position, _initialPosition) > 0.5f)
            {
                _reportedServerMovement = true;
                Debug.Log($"PROTOTYPE_MOVED object={ObjectId}");
            }
        }

        [ServerRpc]
        private void SubmitInput(Vector2 input, bool wantsRun)
        {
            if (float.IsNaN(input.x) || float.IsNaN(input.y) ||
                float.IsInfinity(input.x) || float.IsInfinity(input.y)) return;
            _serverInput = Vector2.ClampMagnitude(input, 1);
            _serverWantsRun = wantsRun;
            _lastInputTime = Time.unscaledTime;
        }

        private Vector3 MovementStep(Vector2 input, bool running)
        {
            var distance = (running ? TestRunSpeed : TestWalkSpeed) * (float)TimeManager.TickDelta;
            return new Vector3(input.x, 0, input.y) * distance;
        }

        private void Update()
        {
            if (!IsClientInitialized) return;
            var anticipateMovement = false;
            if (IsOwner)
            {
                var input = _smokeInput
                    ? new Vector2(Mathf.Sin(Time.unscaledTime), Mathf.Cos(Time.unscaledTime))
                    : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                _localInput = Vector2.ClampMagnitude(input, 1);
                _localWantsRun = _runFixture ? Time.unscaledTime % 8f < 5f :
                    Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                var step = MovementStep(_localInput, _localWantsRun || _running.Value);
                anticipateMovement = (PrototypeVillage.Move(transform.position, step) - transform.position).sqrMagnitude > 0.000025f;
            }
            if (Civilian != null)
            {
                ReportArtPose(Civilian.UpdatePose(IsOwner ? _localInput : Vector2.zero, anticipateMovement,
                    _running.Value || (IsOwner && _localWantsRun)));
                if (_artFixture && Civilian.FrameName != _lastArtFrame)
                {
                    _lastArtFrame = Civilian.FrameName;
                    Debug.Log($"PROTOTYPE_QA_FRAME object={ObjectId} owner={IsOwner} frame={_lastArtFrame}");
                }
            }
            if (IsOwner) UpdateAttackInput();
            if (_reportedClientMovement) return;
            if (Vector3.Distance(transform.position, _clientInitialPosition) <= 0.5f) return;
            _reportedClientMovement = true;
            Debug.Log($"PROTOTYPE_OBSERVED_MOVEMENT object={ObjectId} owner={IsOwner}");
        }

        private void UpdateAttackInput()
        {
            if (_combatFixture && !_sentFixtureAttack)
            {
                var dummy = FindFirstObjectByType<PrototypeDummy>();
                if (dummy == null || !dummy.IsClientInitialized) return;
                _sentFixtureAttack = true;
                Attack(dummy.NetworkObject);
                return;
            }

            if (Input.GetMouseButtonDown(0) && Camera.main != null)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                _target = Physics.Raycast(ray, out var hit) ? hit.collider.GetComponent<PrototypeDummy>() : null;
            }
            if (Input.GetKeyDown(KeyCode.Space) && _target != null && _target.IsClientInitialized)
                Attack(_target.NetworkObject);
        }

        [ServerRpc]
        private void Attack(NetworkObject target)
        {
            if (target == null || !target.TryGetComponent<PrototypeDummy>(out var dummy) ||
                !dummy.IsServerInitialized || dummy.Health == 0 || Time.unscaledTime < _nextAttackAt ||
                (target.transform.position - transform.position).sqrMagnitude > 16f) return;

            _nextAttackAt = Time.unscaledTime + 0.6f;
            dummy.TakeHit();
            var direction = target.transform.position - transform.position;
            ShowPunch(new Vector2(direction.x, direction.z));
        }

        [ObserversRpc]
        private void ShowPunch(Vector2 direction)
        {
            if (IsClientInitialized && Civilian != null)
                Civilian.PlayPunch(direction);
        }

        private void ReportArtPose(string pose)
        {
            if (_artFixture && pose != null)
                Debug.Log($"PROTOTYPE_QA_ART object={ObjectId} owner={IsOwner} pose={pose}");
        }
    }
}
