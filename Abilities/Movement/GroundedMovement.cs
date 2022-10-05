#nullable enable
using System;
using UnityEngine;

namespace BaseGame.Movement
{
    [Serializable]
    public abstract class GroundedMovement<T> : Movement<T>, IGroundedState where T : CharacterVisuals
    {
        private bool isGrounded;

        public bool Value => isGrounded;

        protected GroundedMovement(ID id) : base(id) { }

        protected override sealed void OnUpdate(float delta, Vector2 input, T visuals)
        {
            CollisionEventListener listener = visuals.GetOrAddComponent<CollisionEventListener>();
            isGrounded = CheckIfGrounded(listener);
            OnUpdate(delta, input, visuals, isGrounded);
        }

        protected virtual void OnUpdate(float delta, Vector2 input, T visuals, bool isGrounded) { }

        protected virtual bool CheckIfGrounded(CollisionEventListener listener)
        {
            Vector3 gravityDirection = Physics.gravity.normalized;
            Vector2 gravityDirection2d = Physics2D.gravity.normalized;
            float surfaceAngle = 30f;
            isGrounded = false;

            foreach (Collision collision in listener.Collisions)
            {
                if (collision.contacts.Length > 0)
                {
                    Vector3 normal = collision.contacts[0].normal;
                    if (Vector3.Angle(normal, -gravityDirection) < surfaceAngle)
                    {
                        return true;
                    }
                }
            }

            foreach (Collision2D collision in listener.Collisions2D)
            {
                if (collision.contacts.Length > 0)
                {
                    Vector2 normal = collision.contacts[0].normal;
                    if (Vector2.Angle(normal, -gravityDirection2d) < surfaceAngle)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}