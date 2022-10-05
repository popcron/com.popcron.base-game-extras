#nullable enable
using UnityEngine;

namespace BaseGame
{
    public class Projectile : MonoBehaviour
    {
        public virtual void PreInitialize(IGun gun, IPlayer player, Ray ray) { }
    }
}