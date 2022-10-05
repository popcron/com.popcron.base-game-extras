#nullable enable
using System;
using UnityEngine;

namespace BaseGame.Abilities
{
    [Serializable]
    public class GunShooting : Ability, IUpdateLoop
    {
        private float cooldownTimer;

        public virtual bool WantsToShoot
        {
            get
            {
                if (Player.TryGetInputState("attack", out InputState shootState))
                {
                    return shootState.value > 0.25f || shootState.isPressed;
                }
                else
                {
                    return false;
                }
            }
        }

        public virtual IGun? Gun
        {
            get
            {
                Player.TryGetVisuals(out IGun? gun);
                return gun;
            }
        }

        public GunShooting(ID id) : base(id) { }

        protected virtual Ray GetProjectileOrigin(IGun gun)
        {
            if (Player.Inventory.TryGetFirst(out LookingAround? lookingAround))
            {
                if (Player.TryGetVisuals(out PlayerVisuals? playerVisuals))
                {
                    Vector3 origin = playerVisuals.Position;
                    Vector3 direction = lookingAround.Direction;
                    return new Ray(origin, direction);
                }
                else
                {
                    throw ExceptionBuilder.Format("Player {0} does not have a {1} ability", Player, typeof(PlayerVisuals));
                }
            }
            else
            {
                throw ExceptionBuilder.Format("Player {0} does not have a {1} ability", Player, typeof(LookingAround));
            }
        }

        protected virtual void OnShotProjectile(IGun gun, Projectile projectile) { }

        void IUpdateLoop.OnUpdate(float delta)
        {
            IGun? gun = Gun;
            if (WantsToShoot && gun is not null)
            {
                if (gun.TryToShoot(ref cooldownTimer))
                {
                    if (gun is ProjectileGun projectileGun)
                    {
                        Projectile prefab = projectileGun.ProjectilePrefab;
                        Ray ray = GetProjectileOrigin(gun);

                        prefab.gameObject.SetActive(false);
                        Projectile projectile = GameObject.Instantiate(prefab);
                        prefab.gameObject.SetActive(true);

                        projectile.PreInitialize(gun, Player, ray);
                        projectileGun.ShotProjectile(projectile);
                        OnShotProjectile(gun, projectile);
                        projectile.gameObject.SetActive(true);
                    }
                    else
                    {
                        Log.LogErrorFormat("Gun shooting ability is not able to shoot {0} because its not {1}", Gun, typeof(ProjectileGun));
                    }
                }
            }

            cooldownTimer -= delta;
            if (cooldownTimer < 0)
            {
                cooldownTimer = 0;
            }
        }
    }
}