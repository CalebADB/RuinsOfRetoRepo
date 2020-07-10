using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    [RequireComponent(typeof(PhysicsEngine), typeof(LocalCollisionManager))]
    public class LocalPhysicsEngine : MonoBehaviour
    {
        // Prep
        private Controller parentController;
        private PhysicsEngine physicsEngine;
        public LocalCollisionManager localCollisionManager;

        // Speeds
        public enum SpeedXs
        {
            newSpeed,
            zero,
            walk,
            run,
            crawl,
            slide,
            air
        }
        public Dictionary_SpeedXfloat speedXDict = new Dictionary_SpeedXfloat();
        public enum SpeedYs
        {
            newSpeed,
            zero,
            jump,
            rise
        }
        public Dictionary_SpeedYfloat speedYDict = new Dictionary_SpeedYfloat();
        public Vector2 stateSpeed;

        // Velocity
        public Vector2 inputVelocity;
        public Vector2 envVelocity;
        public Vector2 velocity;

        // final displacement
        private Vector2 displacement;

        // Misc:
        // GrapplingHook
        public bool hasGrappler;
        private Grappler grappler;

        private void Start()
        {
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
            localCollisionManager = GetComponent<LocalCollisionManager>();
            if (hasGrappler)
            {
                grappler = this.gameObject.GetComponentInChildren<Grappler>();
            }
        }

        public void updateEngine()
        {
            // Setup
            parentController = getController();
            frameReset();


            // Calculate velocity
            updateInputVelocity();
            updateEnvVelocity();
            velocity = envVelocity + inputVelocity;

            // Calculate displacement
            displacement = velocity * Time.deltaTime;

            // Collisions management
            displacement = localCollisionManager.checkDisplacement(displacement);

            // Post displacement reactions
            updateControllerImpactStrength();
            updateEnv();

            // Impact envVelocity correction
            if (localCollisionManager.collisionData.topCollision)
            {
                envVelocity.y = -inputVelocity.y;
            }
            if (localCollisionManager.collisionData.horzCollision || localCollisionManager.collisionData.bottomCollision)
            {
                envVelocity.x = 0;
            }

            // Displace object
            this.gameObject.transform.Translate(displacement);
        }

        public Controller getController()
        {
            if (parentController == null)
            {
                parentController = GetComponentInParent<Controller>();
            }
            return parentController;
        }

        private void setStateSpeedX(SpeedXs speedX)
        {
            stateSpeed.x = speedXDict[speedX];
        }
        private void setStateSpeedY(SpeedYs speedY)
        {
            stateSpeed.y = speedYDict[speedY];
        }
        private void setStateSpeed(SpeedXs speedX, SpeedYs speedY)
        {
            stateSpeed.x = speedXDict[speedX];
            stateSpeed.y = speedYDict[speedY];
        }

        private void updateInputVelocity()
        {
            // SET State Speed based on the parents environment
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    setStateSpeed(SpeedXs.run, SpeedYs.zero);
                    break;
                case Controller.EnvState.Air:
                    setStateSpeedY(SpeedYs.rise);
                    break;
                default:
                    Debug.Log("Enviroment Definition Missing");
                    break;
            }

            // UPDATE velocity using statespeed and input
            if (parentController.rise) { inputVelocity.y = stateSpeed.y; }
            if (parentController.moveRight ^ parentController.moveLeft)
            {
                if (parentController.moveRight) { inputVelocity.x = stateSpeed.x ; }
                else { inputVelocity.x = -stateSpeed.x; }
            }
        }
        private void updateEnvVelocity()
        {
            // SET State Speed based on the parents environment
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    envVelocity.y = 0f;
                    if (parentController.rise)
                    {
                        envVelocity.y += speedYDict[SpeedYs.jump];
                    }
                    break;
                case Controller.EnvState.Air:
                    // wind?
                    break;
                default:
                    Debug.Log("Enviroment Missing");
                    break;
            }

            if (hasGrappler)
            {
                grappler.updateGrapplingHook();
                if (grappler.grapplerState == Grappler.GrapplerStates.hookAttached)
                {
                    envVelocity.x += grappler.pullForce.x * Time.deltaTime;
                    envVelocity.y += grappler.pullForce.y * Time.deltaTime;
                }
            }

            envVelocity += physicsEngine.gravity.calculateGravity(this.transform.position) * Time.deltaTime;
        }

        private void updateControllerImpactStrength()
        {
            parentController.impactStrengthPercent = 0f;
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    if (parentController.rise)
                    {
                        parentController.impactStrengthPercent += 10f;
                    }
                    break;
                case Controller.EnvState.Air:
                    if (localCollisionManager.collisionData.vertCollision)
                    {
                        parentController.impactStrengthPercent += 20f;
                    }
                    //if (localCollisionManager.collisionData.horzCollision)
                    //{
                    //    parentController.impactStrengthPercent += 25f;
                    //}
                    break;
                default:
                    Debug.Log("Enviroment Missing");
                    break;
            }
        }
        private void updateEnv()
        {
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    if (parentController.rise || !localCollisionManager.collisionData.bottomCollision)
                    {
                        parentController.env = Controller.EnvState.Air;
                    }
                    break;
                case Controller.EnvState.Air:
                    if (localCollisionManager.collisionData.bottomCollision)
                    {
                        parentController.env = Controller.EnvState.Ground;
                    }
                    break;
                default:
                    Debug.Log("Enviroment Missing");
                    break;
            }
        }

        private void frameReset()
        {
            parentController.impactStrengthPercent = 0f;
            inputVelocity.Set(0f, 0f);
            displacement.Set(0f, 0f);
        }
    }
}