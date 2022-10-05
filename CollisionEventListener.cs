#nullable enable
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BaseGame
{
    public class CollisionEventListener : UnityEngine.MonoBehaviour
    {
        public delegate void OnCollision(Collision collision);
        public delegate void OnCollision2D(Collision2D collision);
        
        public OnCollision onCollisionEnter;
        public OnCollision2D onCollisionEnter2D;
        public OnCollision onCollisionStay;
        public OnCollision2D onCollisionStay2D;

        [SerializeField]
        private UnityEvent? onCollision;

        private List<Collision> collisions = new();
        private List<Collision2D> collisions2d = new();

        public IReadOnlyList<Collision> Collisions => collisions;
        public IReadOnlyList<Collision2D> Collisions2D => collisions2d;

        private void FixedUpdate()
        {
            collisions.Clear();
            collisions2d.Clear();
        }
        
        private void OnCollisionStay(Collision other)
        {
            collisions.Add(other);
            onCollisionStay?.Invoke(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            collisions2d.Add(other);
            onCollisionStay2D?.Invoke(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            onCollisionEnter?.Invoke(collision);
            onCollision?.Invoke();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollisionEnter2D?.Invoke(collision);
            onCollision?.Invoke();
        }
    }
}