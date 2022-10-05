#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Movement
{
    [Serializable]
    public class GroundedPlatformerMovement : GroundedMovement<CharacterVisuals>, IVariables
    {
        [SerializeField]
        private Variable<float> speed = 5;

        public GroundedPlatformerMovement(ID id) : base(id) { }

        protected override void OnUpdate(float delta, Vector2 input, CharacterVisuals visuals, bool isGrounded)
        {
            Vector3 position = visuals.transform.position;
            position.x += input.x * speed.ProcessedValue * delta;
            visuals.transform.position = position;
        }

        IEnumerable<(string, BaseVariable)> IVariables.GetVariables()
        {
            yield return (nameof(speed), speed);
        }
    }
}