using UnityEngine;

namespace JuicyLib
{
    public static class Movement
    {
        public static void MoveRigidBody(ref Rigidbody rigidbody, Vector3 force)
        {
            rigidbody.AddForce(force);
        }

        public static void MoveObjectForward(ref Transform transform, float speed)
        {
            transform.position += transform.forward * speed;
        }

        public static void MoveObject(ref Vector3 position, Vector3 velocity)
        {
            position += velocity;
        }
    }
}
