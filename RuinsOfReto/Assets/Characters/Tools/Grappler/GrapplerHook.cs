using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class GrapplerHook : MonoBehaviour
    {
        private PhysicsEngine physicsEngine;
        public LocalCollisionManager localCollisionManager;
        public Vector2 velocity;
        public Vector3 attachPos;

        private Grappler grappler;

        // Start is called before the first frame update
        void Start()
        {
            grappler = GetComponentInParent<Grappler>();
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (grappler.grapplerState)
            {
                case (Grappler.GrapplerStates.hookIn):
                    this.gameObject.transform.position = grappler.anchor;
                    break;
                case (Grappler.GrapplerStates.hookOut):
                    velocity -= physicsEngine.gravity.gravityStrength * Time.deltaTime;
                    Vector2 displacement = velocity * Time.deltaTime;
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