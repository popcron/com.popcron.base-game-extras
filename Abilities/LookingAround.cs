#nullable enable
using UnityEngine;

namespace BaseGame.Abilities
{
    public abstract class LookingAround : Ability, IUpdateLoop
    {
        /// <summary>
        /// Normalized look direction.
        /// </summary>
        public Vector3 Direction { get; private set; }

        protected LookingAround(ID id) : base(id) { }

        protected abstract Vector3 GetLookDirection(float delta);

        void IUpdateLoop.OnUpdate(float delta)
        {
            Direction = GetLookDirection(delta).normalized;
        }
    }
}