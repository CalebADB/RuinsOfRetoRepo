using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Explosion : MonoBehaviour
    {
        private Animator animator;
        public Projectile.ProjectileType projectileType;

        public void setExplosionType(Projectile.ProjectileType newProjectileType)
        {
            projectileType = newProjectileType;
        }
    }
}