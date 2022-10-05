#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Movement
{
    [AddComponentMenu("Abilities/Holiday movement")]
    public class Movement2D : Movement<CharacterVisuals>, IVariables
    {
        [SerializeField]
        private Variable<float> speed = 4f;

        [SerializeField]
        private Variable<float> acceleration = 1f;

        [SerializeField]
        private Variable<float> deceleration = 1f;

        public Movement2D(ID id) : base(id) { }

        protected override void OnUpdate(float delta, Vector2 input, CharacterVisuals visuals)
        {
            if (visuals.Rigidbody is Rigidbody2D rb)
            {
                bool wantsToMove = input.sqrMagnitude > 0;
                float a = wantsToMove ? acceleration.ProcessedValue : deceleration.ProcessedValue;
                Vector2 velocity = rb.velocity;
                velocity = Vector2.Lerp(velocity, input * speed.ProcessedValue, a * delta);
                rb.velocity = velocity;
            }
        }

        IEnumerable<(string, BaseVariable)> IVariables.GetVariables()
        {
            yield return (nameof(speed), speed);
            yield return (nameof(acceleration), acceleration);
            yield return (nameof(deceleration), deceleration);
        }
    }
}