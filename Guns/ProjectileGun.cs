#nullable enable
using System;
using UnityEngine;

namespace BaseGame
{
    [Serializable]
    public class ProjectileGun : Gun, IShootForce
    {
        [SerializeField]
        private Variable<float> shootForce = 10f;

        [SerializeField, MustBeAssigned]
        private Projectile? projectilePrefab;

        public Projectile ProjectilePrefab
        {
            get
            {
                if (projectilePrefab is null)
                {
                    throw ExceptionBuilder.Format("Projectile prefab not assigned to {0}", this);
                }

                if (!projectilePrefab)
                {
                    throw ExceptionBuilder.Format("Projectile prefab is missing on {0}", this);
                }

                return projectilePrefab;
            }
        }

        Variable<float> IShootForce.Value => shootForce;

        public ProjectileGun(ID id) : base(id) { }

        public virtual void ShotProjectile(Projectile projectile) { }
    }
}