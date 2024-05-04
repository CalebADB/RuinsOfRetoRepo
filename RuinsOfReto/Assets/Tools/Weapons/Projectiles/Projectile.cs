using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Projectile : MonoBehaviour
    {

        private PhysicsEngine physicsEngine;
        public Vector2 velocity;
        private LocalCollisionManager localCollisionManager;
        private Rigidbody2D rb;
        public bool explodeOnCollision;
        [Range(1,3)]
        public int damage;
        public float timer;
        private float timeLeft;
        [Range(0f,10f)]
        public GameObject impactEffectPrefab;
        public enum ProjectileType
        {
            grenade,
            missle,
            plasma
        }
        private ProjectileType projectileType;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            localCollisionManager = GetComponent<LocalCollisionManager>();
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
            timeLeft = timer;
        }

        private void FixedUpdate()
        {
            timeLeft -= Time.fixedDeltaTime;
            velocity -= physicsEngine.gravity.gravityStrength * Time.fixedDeltaTime / 2;
            Vector2 displacement = velocity * Time.fixedDeltaTime;
            displacement = localCollisionManager.checkDisplacement(displacement);

            rb.rotation = - (Mathf.Atan2(displacement.x, displacement.y * Mathf.Rad2Deg - 90f));

            rb.velocity = velocity;

            if (timeLeft < 0)
            {
                if (!explodeOnCollision)
                    DamageAroundThisArea(8);
                Destroy(this.gameObject);
            }
            if (localCollisionManager.collisionData.vertCollision || localCollisionManager.collisionData.horzCollision)
            {
                if (explodeOnCollision)
                {
                    Controller controller = localCollisionManager.collisionData.collidedObject.GetComponent<Controller>();
                    if (controller != null)
                    {
                        controller.takeDamage();
                    }
                    Destroy(this.gameObject);
                }

                if (localCollisionManager.collisionData.vertCollision)
                {
                    Debug.Log(velocity.y);
                    velocity.y = -velocity.y / 2;
                    Debug.Log(velocity.y);
                }
                if (localCollisionManager.collisionData.horzCollision)
                {
                    velocity.x = -velocity.x / 2;
                }
            }
        }

        public void setProjectileType(ProjectileType newProjectileType)
        {
            projectileType = newProjectileType;
        }

        public ProjectileType getProjectileType()
        {
            return projectileType;
        }

        private void DamageAroundThisArea(float radius)
        {
            float angle = 0;
            int RaysToShoot = 30;
            for (int i = 0; i < RaysToShoot; i++)
            {
                float x = Mathf.Sin(angle);
                float y = Mathf.Cos(angle);
                angle += 2 * Mathf.PI / RaysToShoot;

                Vector3 dir = new Vector3(transform.position.x + x, transform.position.y + y, 0);
                RaycastHit hit;
                Debug.DrawLine(transform.position, dir, Color.red);
                if (Physics.Raycast(transform.position, dir, out hit, radius))
                {
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        var te = hit.transform.GetComponent<Controller>();
                        if (te != null)
                        {
                            te.takeDamage(damage);
                            Debug.Log("Hit Enemy Damage!");
                        }
                    }
                }
            }
        }
    }
}