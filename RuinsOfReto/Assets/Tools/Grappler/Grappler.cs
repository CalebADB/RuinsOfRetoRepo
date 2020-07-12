using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class Grappler : MonoBehaviour
    {
        public Controller controller;
        public GrapplerBase _base;
        public GrapplerTether tether;
        public GrapplerHook hook;
        public GameObject target;
        public float hookLaunchSpeed;

        [Range(1f, 20f)]
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
        }

        public void updateGrappler()
        {
            pullForce = Vector3.zero;

            switch (grapplerState)
            {
                case GrapplerStates.hookIn:
                    if (controller.useHook)
                    {
                        hook.velocity = 10 * hookLaunchSpeed * Vector3.Normalize(target.transform.position - _base.anchor);
                        grapplerState = GrapplerStates.hookOut;
                        setRender(true);
                    }
                    break;
                case GrapplerStates.hookOut:
                    if (!controller.useHook)
                    {
                        hook.transform.position = _base.anchor;
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
                        hook.transform.position = _base.anchor;
                        grapplerState = GrapplerStates.hookIn;
                        setRender(false);
                    }
                    pullForce.x = pullStrength * (hook.attachPos.x - _base.anchor.x);
                    pullForce.y = pullStrength * (hook.attachPos.y - _base.anchor.y);
                    if (pullForce.magnitude > pullForceMax) { pullForce = pullForceMax* pullForce.normalized; };
                    break;
            }
        }

        private void setRender(bool render)
        {
            tether.GetComponent<LineRenderer>().enabled = render;
        }
    }
}