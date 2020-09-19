using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    [RequireComponent(typeof(Animator))]
    public class Projectile : MonoBehaviour
    {
        private PhysicsEngine physicsEngine;
        public Vector2 velocity;
        private LocalCollisionManager localCollisionManager;
        private Rigidbody2D rb;
        public GameObject explosionPrefab;
        public enum ProjectileType
        {
            Grenade,
            Missle,
            Plasma
        }
        private ProjectileType projectileType;

        public bool explodeOnCollision;
        [Range(0.1f, 4f)]
        public float timer;
        private float timeLeft;

        public LayerMask damageMask;
        [Range(1, 3)]
        public int damage;
        [Range(0.02f, 1.28f)]
        public float damageRadius;
        [Range(0f, 10f)]

        public float spin;
        private float totalSpin;

        void Start()
        {
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
            rb = GetComponent<Rigidbody2D>();
            localCollisionManager = GetComponent<LocalCollisionManager>();
            timeLeft = timer;
        }

        private void Update()
        {
            timeLeft -= Time.deltaTime;
            velocity -= physicsEngine.gravity.gravityStrength * Time.deltaTime / 2;
            Vector2 displacement = velocity * Time.deltaTime;
            displacement = localCollisionManager.checkDisplacement(displacement);

            totalSpin += spin * Time.deltaTime;
            rb.rotation = - (Mathf.Atan2(displacement.x, displacement.y * Mathf.Rad2Deg - 90f)) + spin;

            rb.velocity = velocity;

            if (timeLeft < 0)
            {
                explode(damageRadius); 
            }
            if (localCollisionManager.collisionData.vertCollision || localCollisionManager.collisionData.horzCollision)
            {
                transform.Translate(displacement);
                if (explodeOnCollision)
                {
                    explode(damageRadius);
                }

                if (localCollisionManager.collisionData.vertCollision)
                {
                    velocity.y = -velocity.y / 2;
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
        
        private void explode(float explosionRadius)
        {
            float angle = 0;
            int RaysToShoot = 30;
            for (int i = 0; i < RaysToShoot; i++)
            {
                angle += 2 * Mathf.PI / RaysToShoot;
                Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, explosionRadius,damageMask);
                Debug.DrawLine(transform.position, transform.position+(new Vector3(dir.x,dir.y) * explosionRadius),Color.magenta);

                if (hit)
                {
                    //                  Debug.Log(hit.point);
                    //                  Debug.Log(hit.transform.gameObject.tag);
                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        Controller enemy = hit.transform.GetComponent<Controller>();
                        if (enemy != null)
                        {
                            enemy.takeDamage(damage);
                        }
                    }
                    if (hit.transform.gameObject.tag == "Player")
                    {
                        Controller player = hit.transform.GetComponent<Controller>();
                        if (player != null)
                        {
                            player.takeDamage(damage);
                        }
                    }
                }
            }

            GameObject explosionObject = Instantiate(explosionPrefab);
            explosionObject.transform.TryGetComponent<Explosion>(out Explosion explosion);
            explosion.transform.position = this.transform.position;
            explosion.setExplosionType(projectileType);
            Destroy(gameObject);
        }
    }
}