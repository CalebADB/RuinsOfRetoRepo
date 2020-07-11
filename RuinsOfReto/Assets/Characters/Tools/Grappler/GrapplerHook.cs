using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class GrapplerHook : MonoBehaviour
    {
        private Grappler grappler;
        private Rigidbody2D rb;

        private PhysicsEngine physicsEngine;
        public LocalCollisionManager localCollisionManager;
        public Vector2 velocity;
        public Vector3 attachPos;


        // Start is called before the first frame update
        void Start()
        {
            grappler = GetComponentInParent<Grappler>();
            rb = GetComponentInParent<Rigidbody2D>();
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
        }

        // Update is called once per frame
        public void updateGrapplerHook(Vector2 controllerDisplacement)
        {
            switch (grappler.grapplerState)
            {
                case (Grappler.GrapplerStates.hookIn):
                    
                    rb.rotation = -grappler._base.angle;
                    this.gameObject.transform.position = grappler._base.anchor;
                    break;
                case (Grappler.GrapplerStates.hookOut):
                    velocity -= controllerDisplacement;
                    velocity -= physicsEngine.gravity.gravityStrength * Time.deltaTime;
                    Vector2 displacement = velocity * Time.deltaTime;

                    rb.rotation = -( Mathf.Atan2(displacement.x, displacement.y) * Mathf.Rad2Deg - 90f);

                    Vector3 displacement3 = localCollisionManager.checkDisplacement(displacement);
                    this.gameObject.transform.position = this.gameObject.transform.position + displacement3;
                    if (localCollisionManager.collisionData.horzCollision || localCollisionManager.collisionData.vertCollision)
                    {
                        velocity = Vector2.zero;
                        attachPos = this.gameObject.transform.position;
                    }
                    break;
                case (Grappler.GrapplerStates.hookAttached):
                    this.gameObject.transform.position = attachPos;
                    break;
            }
        }
    }
}