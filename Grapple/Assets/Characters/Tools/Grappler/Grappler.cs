using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Grappler : MonoBehaviour
    {
        public Controller controller;
        public Vector3 anchor;
        public GameObject target;
        public GrapplerHook hook;
        public GrapplerTether tether;
        public float hookLaunchSpeed;

        public float pullStrength;
        public Vector2 pullForce;
        public float pullForceMax;

        public enum GrapplerStates
        {
            hookIn,
            hookOut,
            hookAttached
        }
        public GrapplerStates grapplerState;

        private void Start()
        {
            controller = GetComponentInParent<Controller>();
            setRender(false);
        }

        public void updateGrapplingHook()
        {
            pullForce = Vector3.zero;
            setAnchor(new Vector3(0f,0.4f,0));
            switch (grapplerState)
            {
                case GrapplerStates.hookIn:
                    if (controller.useHook)
                    {
                        hook.velocity = 10 * hookLaunchSpeed * Vector3.Normalize(target.transform.position - anchor);
                        grapplerState = GrapplerStates.hookOut;
                        setRender(true);
                    }
                    break;
                case GrapplerStates.hookOut:
                    if (!controller.useHook)
                    {
                        hook.transform.position = anchor;
                        grapplerState = GrapplerStates.hookIn;
                        setRender(false);
                    }
                    else if (hook.localCollisionManager.collisionData.horzCollision || hook.localCollisionManager.collisionData.vertCollision)
                    {
                        grapplerState = GrapplerStates.hookAttached;
                    }
                    break;
                case GrapplerStates.hookAttached:
                    if (!controller.useHook)
                    {
                        hook.transform.position = anchor;
                        grapplerState = GrapplerStates.hookIn;
                        setRender(false);
                    }
                    pullForce.x = pullStrength * (hook.attachPos.x - anchor.x);
                    pullForce.y = pullStrength * (hook.attachPos.y - anchor.y);
                    if (pullForce.magnitude > pullForceMax) { pullForce = pullForceMax* pullForce.normalized; };
                    break;
            }
        }

        public void setAnchor(Vector3 anchorCorrection)
        {
            anchor = controller.transform.position + anchorCorrection;
        }
        private void setRender(bool render)
        {
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.enabled = render;
            }
        }
    }
}