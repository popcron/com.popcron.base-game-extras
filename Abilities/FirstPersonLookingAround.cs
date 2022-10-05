#nullable enable
using System;
using UnityEngine;

namespace BaseGame.Abilities
{
    [Serializable]
    public class FirstPersonLookingAround : LookingAround
    {
        private float pitch;
        private float yaw;
        
        public FirstPersonLookingAround(ID id) : base(id) { }

        protected override Vector3 GetLookDirection(float delta)
        {
            if (Player.TryGetInputState("mouseDelta", out InputState positionState))
            {
                Vector2 screenPosition = positionState.vector;
                Vector2 mouseDelta = screenPosition - new Vector2(Screen.width / 2, Screen.height / 2);
                yaw += mouseDelta.x * 0.1f;
                pitch -= mouseDelta.y * 0.1f;
                pitch = Mathf.Clamp(pitch, -90, 90);
                return Quaternion.Euler(pitch, yaw, 0) * Vector3.forward;
            }
            else
            {
                throw ExceptionBuilder.Format("Player {0} does not have an input state named {1}", Player, "mouseDelta");
            }
        }
    }
}