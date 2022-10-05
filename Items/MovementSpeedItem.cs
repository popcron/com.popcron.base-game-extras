#nullable enable
using UnityEngine;

namespace BaseGame
{
    public class VariableMultiplier : PlayerItem, IUnityLifecycle
    {
        [SerializeField]
        private string? name;

        [SerializeField]
        private float multiplier = 1f;

        public VariableMultiplier(ID id) : base(id) { }

        private float Multiply(float input)
        {
            return input * multiplier;
        }

        void IUnityLifecycle.OnEnabled()
        {
            foreach (Variable<float> speedVariable in Player.GetVariables<float>(name))
            {
                speedVariable.AddProcessor(Multiply);
            }
        }

        void IUnityLifecycle.OnDisabled()
        {
            foreach (Variable<float> speedVariable in Player.GetVariables<float>(name))
            {
                speedVariable.RemoveProcessor(Multiply);
            }
        }
    }
}