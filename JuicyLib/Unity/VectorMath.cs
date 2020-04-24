using UnityEngine;

namespace JuicyLib.Unity.Math
{
    public static class VectorMath
    {
        public static float Distance(Vector3 origin, Vector3 target)
        {
            Vector3 sqrOrigin = new Vector3(origin.x * origin.x, origin.y * origin.y, origin.z * origin.z);
            Vector3 sqrTarget = new Vector3(target.x * target.x, target.y * target.y, target.z * target.z);
            return Mathf.Sqrt((sqrTarget - sqrOrigin).magnitude);
        }

        public static float Distance(Vector2 origin, Vector2 target)
        {
            origin *= origin;
            target *= target;

            return Mathf.Sqrt((target - origin).magnitude);
        }

        public static Vector3 Direction(Vector3 origin, Vector3 target)
        {
            return (target - origin).normalized;
        }

        public static Vector2 MousePosition
        {
            get { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }
        }

        public static void Truncate(ref Vector3 vector, float maxLenght, bool truncateY = true)
        {
            Vector3 velo = vector;

            if (!truncateY)
                vector.y = 0;

            if (vector.magnitude > maxLenght)
            {

                vector.Normalize();
                vector *= maxLenght;

            }

            if (!truncateY)
                vector.y = velo.y;
        }

        public static void Clamp(ref float value, float min, float max)
        {
            if (value > max)
                value = max;
            else if (value < min)
                value = min;
        }

        public static void Clamp(ref float value, Vector2 boundaries)
        {
            if (value < boundaries.x)
                value = boundaries.x;
            else if (value > boundaries.y)
                value = boundaries.y;
        }
    }
}
