using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace TuTien.Prototype
{
    public sealed class PrototypeDummy : NetworkBehaviour
    {
        // QA fixture. The game's combat values are still open.
        private const int MaxHealth = 100;
        private const int DamagePerHit = 25;
        private readonly SyncVar<int> _health = new(MaxHealth);
        private float _resetAt;
        private int _lastReportedHealth = -1;
        private bool _reportHealth;

        public int Health => _health.Value;

        public override void OnStartClient()
        {
            base.OnStartClient();
            _reportHealth = PrototypeSession.HasArgument("--qa-combat");
            _lastReportedHealth = -1;
            if (TryGetComponent<Renderer>(out var dummyRenderer))
            {
                var properties = new MaterialPropertyBlock();
                properties.SetColor("_Color", new Color(0.45f, 0.9f, 0.25f));
                dummyRenderer.SetPropertyBlock(properties);
            }
        }

        public void TakeHit()
        {
            if (!IsServerInitialized || Health == 0) return;
            _health.Value = Mathf.Max(0, Health - DamagePerHit);
            if (Health == 0) _resetAt = Time.unscaledTime + 3;
            Debug.Log($"PROTOTYPE_QA_HIT hp={Health}");
        }

        private void Update()
        {
            if (IsServerInitialized && Health == 0 && Time.unscaledTime >= _resetAt)
                _health.Value = MaxHealth;
            if (IsClientInitialized && _reportHealth && _lastReportedHealth != Health)
            {
                _lastReportedHealth = Health;
                Debug.Log($"PROTOTYPE_QA_DUMMY hp={Health}");
            }
        }

        private void OnGUI()
        {
            if (!IsClientInitialized || Camera.main == null) return;
            var point = Camera.main.WorldToScreenPoint(transform.position + Camera.main.transform.up * 0.7f);
            if (point.z <= 0) return;
            GUI.Box(new Rect(point.x - 65, Screen.height - point.y - 24, 130, 24),
                $"Training dummy {Health}/{MaxHealth}");
        }
    }
}
