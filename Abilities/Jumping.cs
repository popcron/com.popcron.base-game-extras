#nullable enable
using BaseGame.Movement;
using System;
using UnityEngine;

namespace BaseGame
{
    [Serializable]
    public class Jumping : Ability, IUpdateLoop
    {
        [SerializeField]
        private Variable<float> jumpHeight = 4f;

        [SerializeField]
        private Variable<float> cooldown = 0.25f;

        private float cooldownTimer = 0f;

        public Jumping(ID id) : base(id) { }

        protected override void AddedTo(IPlayer player)
        {
            if (!player.Inventory.Contains<IGroundedState>())
            {
                Log.LogErrorFormat("No ability or item present on {0} that implements {1}", player, typeof(IGroundedState));
            }
        }

        public bool TryToJump(bool ignoreGroundedState = false)
        {
            if (Player.TryGetVisuals(out PlayerVisuals? visuals))
            {
                bool canJump = true;
                if (!ignoreGroundedState && Player.Inventory.TryGetFirst(out IGroundedState? groundedState))
                {
                    canJump = groundedState.Value;
                }    

                if (canJump)
                {
                    Vector3 velocity = visuals.Velocity;
                    Vector3 gravity = visuals.PhysicsGravity;
                    float gravityScale = gravity.magnitude;
                    float force = Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(gravityScale));
                    velocity += gravity.normalized * force * -Mathf.Sign(gravityScale);
                    visuals.Velocity = velocity;
                    return true;
                }
            }

            return false;
        }

        void IUpdateLoop.OnUpdate(float delta)
        {
            if (cooldownTimer <= 0 && Player.GetInputState("jump").IsActive)
            {
                if (TryToJump())
                {
                    cooldownTimer = cooldown;
                }
            }

            cooldownTimer -= delta;
        }
    }
}