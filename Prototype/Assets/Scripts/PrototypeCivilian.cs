using System;
using UnityEngine;

namespace TuTien.Prototype
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class PrototypeCivilian : MonoBehaviour
    {
        private enum Facing { Front, Back, Right, Left }
        private enum Pose { Idle, Walk, Run, Punch }
        private const float WalkCycleDistance = 2.65f;
        private const float RunCycleDistance = 3.35f;

        [Serializable]
        private struct DirectionalFrames
        {
            [SerializeField] private Sprite[] front;
            [SerializeField] private Sprite[] back;
            [SerializeField] private Sprite[] side;

            public Sprite[] For(Facing facing) => facing switch
            {
                Facing.Front => front,
                Facing.Back => back,
                _ => side
            };
        }

        [SerializeField] private DirectionalFrames idle;
        [SerializeField] private DirectionalFrames walk;
        [SerializeField] private DirectionalFrames run;
        [SerializeField] private Sprite[] punchFront;
        [SerializeField] private Sprite[] punchSide;

        private SpriteRenderer _renderer;
        private Camera _camera;
        private Facing _facing;
        private Pose _pose;
        private Vector3 _lastPosition;
        private float _walkUntil;
        private float _gaitPhase;
        private float _punchStartedAt;
        private Sprite[] _activePunch;

        public string FrameName => _renderer.sprite.name;

        public void Initialize()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
            _lastPosition = transform.parent.position;
            _facing = Facing.Front;
            _pose = Pose.Idle;
            _walkUntil = 0f;
            _gaitPhase = 0f;
            _activePunch = null;
            _renderer.sprite = idle.For(_facing)[0];
            _renderer.flipX = false;
        }

        public string UpdatePose(Vector2 steering, bool anticipateMovement, bool running)
        {
            if (_camera != null) transform.rotation = _camera.transform.rotation;
            var position = transform.parent.position;
            var displacement = position - _lastPosition;
            _lastPosition = position;
            var planarMotion = new Vector2(displacement.x, displacement.z);
            var now = Time.unscaledTime;
            if (planarMotion.sqrMagnitude > 0.000025f)
                _walkUntil = now + 0.095f;

            var punchAge = now - _punchStartedAt;
            var punching = _activePunch != null && punchAge < (_activePunch.Length == 1 ? 0.25f : _activePunch.Length / 12f);
            var moving = anticipateMovement || now < _walkUntil;
            var next = punching ? Pose.Punch : !moving ? Pose.Idle : running ? Pose.Run : Pose.Walk;
            if (!punching) Face(steering.sqrMagnitude > 0 ? steering : planarMotion, 0.005f);

            var changed = next != _pose;
            if (changed)
            {
                if (_pose is Pose.Idle or Pose.Punch) _gaitPhase = 0f;
                _pose = next;
            }

            Sprite sprite;
            if (punching)
            {
                var frame = Mathf.Min((int)(punchAge * 12f), _activePunch.Length - 1);
                sprite = _activePunch[frame];
            }
            else if (moving)
            {
                var frames = (running ? run : walk).For(_facing);
                var cycleDistance = running ? RunCycleDistance : WalkCycleDistance;
                _gaitPhase = Mathf.Repeat(_gaitPhase + planarMotion.magnitude / cycleDistance, 1f);
                var frame = Mathf.Min((int)(_gaitPhase * frames.Length), frames.Length - 1);
                sprite = frames[frame];
            }
            else
            {
                sprite = idle.For(_facing)[0];
            }
            if (_renderer.sprite != sprite) _renderer.sprite = sprite;
            _renderer.flipX = _facing == Facing.Left;

            return changed ? _pose.ToString() : null;
        }

        public void PlayPunch(Vector2 direction)
        {
            Face(direction, 0.01f);
            _activePunch = _facing == Facing.Front ? punchFront : punchSide;
            _punchStartedAt = Time.unscaledTime;
        }

        private void Face(Vector2 direction, float threshold)
        {
            if (direction.sqrMagnitude < threshold * threshold) return;
            var sideBias = _facing is Facing.Left or Facing.Right ? 0.9f : 1.1f;
            if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y) * sideBias)
                _facing = direction.x < 0 ? Facing.Left : Facing.Right;
            else
                _facing = direction.y < 0 ? Facing.Front : Facing.Back;
        }
    }
}
