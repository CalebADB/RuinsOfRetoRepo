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

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            localCollisionManager = GetComponent<LocalCollisionManager>();
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
            timeLeft = timer;
        }

        private void Update()
        {
            timeLeft -= Time.deltaTime;
            velocity -= physicsEngine.gravity.gravityStrength * Time.deltaTime / 2;
            Vector2 displacement = velocity * Time.deltaTime;
            displacement = localCollisionManager.checkDisplacement(displacement);

            rb.rotation = - (Mathf.Atan2(displacement.x, displacement.y * Mathf.Rad2Deg - 90f));

            rb.velocity = velocity;

            Debug.Log(localCollisionManager);
            if (timeLeft < 0)
            {
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

        private void OnTriggerEnter2D(Collider2D hitInfo)
        {
        }
    }
}