#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace BaseGame.Abilities
{
    [AddComponentMenu("Base Game/Damage Health on collision")]
    public class DamageHealthOnCollision : MonoBehaviour, IVariables
    {
        [SerializeField]
        private Variable<int> value = 10;

        public int Value => value.ProcessedValue;

        IEnumerable<(string, BaseVariable)> IVariables.GetVariables()
        {
            yield return ("damage", value);
        }
    }
}
