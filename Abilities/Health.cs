#nullable enable
using System;
using UnityEngine;

namespace BaseGame.Abilities
{
    [Serializable]
    public class Health : Ability
    {
        [SerializeField]
        private int value;

        [SerializeField]
        private int maxValue;

        private CollisionEventListener? listener;

        public float Value => Mathf.Clamp01(value / (float)maxValue);

        public Health(ID id) : base(id) { }

        protected override void OnEnabled(IPlayer player)
        {
            if (player.TryGetVisuals(out PlayerVisuals? visuals))
            {
                listener = visuals.Visuals.GetOrAddComponent<CollisionEventListener>();
                listener.onCollisionEnter += OnPlayerCollision;
                listener.onCollisionEnter2D += OnPlayerCollision2D;
            }
        }

        protected override void OnDisabled(IPlayer player)
        {
            if (player.TryGetVisuals(out PlayerVisuals? visuals))
            {
                if (visuals.TryGetVisuals(out CharacterVisuals? characterVisuals))
                {
                    listener = visuals.Visuals.GetComponent<CollisionEventListener>();
                    if (listener is not null)
                    {
                        listener.onCollisionEnter -= OnPlayerCollision;
                        listener.onCollisionEnter2D -= OnPlayerCollision2D;
                    }
                }
            }
        }

        public void TakeDamage(int damageAmount)
        {
            value = Mathf.Clamp(value - damageAmount, 0, maxValue);
            CertifyDeath();
        }

        private void CertifyDeath()
        {
            if (value == 0)
            {
                if (Player is Player player)
                {
                    player.MarkAsDead();
                }
            }
        }

        private void OnPlayerCollision2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent(out DamageHealthOnCollision damage))
            {
                TakeDamage(damage.Value);
            }
        }

        private void OnPlayerCollision(Collision collision)
        {
            if (collision.transform.TryGetComponent(out DamageHealthOnCollision damage))
            {
                TakeDamage(damage.Value);
            }
        }
    }
}
