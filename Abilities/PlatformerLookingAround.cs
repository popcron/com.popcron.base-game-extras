#nullable enable
using System;
using UnityEngine;

namespace BaseGame.Abilities
{
    [Serializable]
    public class LookingAround2D : LookingAround
    {
        public LookingAround2D(ID id) : base(id) { }

        protected override Vector3 GetLookDirection(float delta)
        {
            if (Player.TryGetVisuals(out PlayerVisuals? playerVisuals))
            {
                if (Player.TryGetInputState("mousePosition", out InputState positionState))
                {
                    Vector2 playerPosition = playerVisuals.Position;
                    Vector2 screenPosition = positionState.vector;
                    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
                    return (worldPosition - playerPosition).normalized;
                }
                else if (Player.TryGetInputState("lookDirection", out InputState lookDirectionState))
                {
                    return lookDirectionState.vector.normalized;
                }
                else
                {
                    return Vector3.zero;
                }
            }
            else
            {
                throw ExceptionBuilder.Format("Player {0} does not have a {1} ability", Player, typeof(PlayerVisuals));
            }
        }
    }
}