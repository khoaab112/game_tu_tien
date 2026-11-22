using UnityEngine;

namespace TuTien.Prototype
{
    [RequireComponent(typeof(Camera))]
    public sealed class PrototypeVillage : MonoBehaviour
    {
        public const int ImageWidth = 2752;
        public const int ImageHeight = 1536;
        public const float Width = 48f;
        public const float Height = Width * ImageHeight / ImageWidth;
        private const float VisibleAreaFraction = 0.2f;

        // Approximate footprints in source image pixels, measured from its top-left.
        private static readonly Rect[] BuildingFootprints =
        {
            Rect.MinMaxRect(282, 42, 755, 333),
            Rect.MinMaxRect(1240, 171, 1640, 625),
            Rect.MinMaxRect(2172, 35, 2583, 350),
            Rect.MinMaxRect(278, 631, 557, 1040),
            Rect.MinMaxRect(1550, 968, 1841, 1384),
            Rect.MinMaxRect(2357, 920, 2660, 1180),
            Rect.MinMaxRect(937, 693, 1068, 902),
            Rect.MinMaxRect(1075, 651, 1213, 805),
            Rect.MinMaxRect(1144, 879, 1284, 1014),
            Rect.MinMaxRect(1880, 585, 2014, 802)
        };

        private static readonly Vector2[] UpperStream =
        {
            new(2752, 650), new(2550, 648), new(2427, 683), new(2356, 759),
            new(2332, 811), new(2256, 898), new(2174, 974), new(2129, 1040), new(2120, 1094)
        };

        private static readonly Vector2[] LowerStream =
        {
            new(2129, 1265), new(2114, 1320), new(2043, 1401), new(1986, 1482), new(1963, 1536)
        };

        private Camera _camera;
        private Transform _followTarget;
        private Vector3 _followVelocity;
        private bool _snapToTarget;

        private void Awake() => _camera = GetComponent<Camera>();

        public void Follow(Transform target)
        {
            _followTarget = target;
            _snapToTarget = true;
        }

        public void StopFollowing(Transform target)
        {
            if (_followTarget != target) return;
            _followTarget = null;
            _snapToTarget = true;
        }

        public static float ViewHalfHeight(float aspect)
        {
            aspect = Mathf.Max(0.01f, aspect);
            var desired = 0.5f * Mathf.Sqrt(Width * Height * VisibleAreaFraction / aspect);
            return Mathf.Min(desired, 0.5f * Mathf.Min(Height, Width / aspect));
        }

        private void LateUpdate()
        {
            var aspect = Mathf.Max(0.01f, _camera.aspect);
            var halfHeight = ViewHalfHeight(aspect);
            var halfWidth = halfHeight * aspect;
            _camera.orthographicSize = halfHeight;

            var focus = _followTarget != null ? _followTarget.position : Vector3.zero;
            var horizontalLimit = Mathf.Max(0f, Width * 0.5f - halfWidth);
            var verticalLimit = Mathf.Max(0f, Height * 0.5f - halfHeight);
            var goal = new Vector3(
                Mathf.Clamp(focus.x, -horizontalLimit, horizontalLimit),
                transform.position.y,
                Mathf.Clamp(focus.z, -verticalLimit, verticalLimit));
            if (_snapToTarget)
            {
                transform.position = goal;
                _followVelocity = Vector3.zero;
                _snapToTarget = false;
                return;
            }

            var position = Vector3.SmoothDamp(transform.position, goal, ref _followVelocity,
                0.075f, Mathf.Infinity, Time.unscaledDeltaTime);
            position.x = Mathf.Clamp(position.x, -horizontalLimit, horizontalLimit);
            position.z = Mathf.Clamp(position.z, -verticalLimit, verticalLimit);
            transform.position = position;
        }

        public static Vector3 ImageToWorld(float x, float y, float elevation)
        {
            return new Vector3((x / ImageWidth - 0.5f) * Width, elevation, (0.5f - y / ImageHeight) * Height);
        }

        public static Vector3 PlayerSpawn(int clientId)
        {
            var slot = clientId % 8;
            return ImageToWorld(1320 + slot % 4 * 50, 785 + slot / 4 * 48, 1);
        }

        public static Vector3 Move(Vector3 position, Vector3 delta)
        {
            var next = position + delta;
            if (IsWalkable(next)) return next;
            next.z = position.z;
            if (IsWalkable(next)) return next;
            next.x = position.x;
            next.z = position.z + delta.z;
            return IsWalkable(next) ? next : position;
        }

        public static bool IsWalkable(Vector3 position)
        {
            var pixel = new Vector2((position.x / Width + 0.5f) * ImageWidth, (0.5f - position.z / Height) * ImageHeight);
            if (pixel.x < 45 || pixel.x > ImageWidth - 45 || pixel.y < 45 || pixel.y > ImageHeight - 45)
                return false;
            foreach (var building in BuildingFootprints)
            {
                if (building.Contains(pixel)) return false;
            }
            return !IsInStream(pixel, UpperStream) && !IsInStream(pixel, LowerStream);
        }

        private static bool IsInStream(Vector2 pixel, Vector2[] stream)
        {
            const float halfWidth = 43f;
            for (var i = 1; i < stream.Length; i++)
            {
                var start = stream[i - 1];
                var segment = stream[i] - start;
                var offset = Mathf.Clamp01(Vector2.Dot(pixel - start, segment) / segment.sqrMagnitude);
                if ((pixel - (start + segment * offset)).sqrMagnitude < halfWidth * halfWidth)
                    return true;
            }
            return false;
        }
    }
}
