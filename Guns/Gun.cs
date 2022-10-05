#nullable enable
using System;
using UnityEngine;

namespace BaseGame
{
    [Serializable]
    public class Gun : Item, IGun
    {
        [SerializeField]
        private float attackRate = 0.5f;

        public float AttackRate => attackRate;

        public Gun(ID id) : base(id) { }

        protected virtual bool TryToShoot(ref float cooldownTimer)
        {
            if (cooldownTimer == 0)
            {
                cooldownTimer = attackRate;
                return true;
            }

            return false;
        }

        bool IGun.TryToShoot(ref float time) => TryToShoot(ref time);
    }
}