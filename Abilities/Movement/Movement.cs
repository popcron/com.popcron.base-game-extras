#nullable enable

using UnityEngine;

namespace BaseGame
{
    public abstract class Movement<T> : Ability, IUpdateLoop where T : CharacterVisuals
    {
        protected Movement(ID id) : base(id) { }

        protected abstract void OnUpdate(float delta, Vector2 input, T visuals);

        void IUpdateLoop.OnUpdate(float delta)
        {
            Vector2 input = Vector2.zero;
            if (Player.TryGetInputState("moveDirection", out InputState state))
            {
                input = state.vector.normalized;
            }

            if (Player.TryGetVisuals(out PlayerVisuals? visuals))
            {
                if (visuals.Visuals is T t)
                {
                    OnUpdate(delta, input, t);
                }
                else
                {
                    Log.LogErrorFormat("Visuals of type {0} is not of type {1}", visuals.Visuals.GetType(), typeof(T));
                }
            }
            else
            {
                Log.LogErrorFormat("PlayerVisuals not found for {0}", Player);
            }
        }
    }
}