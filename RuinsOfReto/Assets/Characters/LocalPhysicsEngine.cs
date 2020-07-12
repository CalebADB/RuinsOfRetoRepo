using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace masterFeature
{
    [RequireComponent(typeof(LocalCollisionManager))]
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

        [Range(1f,40f)]
        public float maxEnvSpeed;

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

        // ProjectileLauncher
        public bool hasProjectileLauncher;
        private ProjectileLauncher projectileLauncher;


        public delegate void JumpStart_Delegate(Vector3 jumpPoint);
        public JumpStart_Delegate JumpStart_Event;

        public delegate void HitTop_Delegate(Vector3 hitPoint);
        public HitTop_Delegate HitTop_Event;

        public delegate void HitBottom_Delegate(Vector3 hitPoint, bool isMoving);
        public HitBottom_Delegate HitBottom_Event;

        public delegate void HitLeft_Delegate(Vector3 hitPoint);
        public HitLeft_Delegate HitLeft_Event;

        public delegate void HitRight_Delegate(Vector3 hitPoint);
        public HitRight_Delegate HitRight_Event;

        private void Start()
        {
            physicsEngine = GameObject.FindObjectOfType<PhysicsEngine>();
            localCollisionManager = GetComponent<LocalCollisionManager>();
            if (hasGrappler)
            {
                grappler = this.gameObject.GetComponentInChildren<Grappler>();
            }

            if (hasProjectileLauncher)
            {
                projectileLauncher = this.gameObject.GetComponentInChildren<ProjectileLauncher>();
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
            float frameSpeedCorrection = 1.2f;
            velocity = envVelocity / frameSpeedCorrection + inputVelocity;

            // Calculate displacement
            displacement = velocity * Time.deltaTime;

            // Collisions management
            displacement = localCollisionManager.checkDisplacement(displacement);

            // Post displacement reactions
            surfaceVelocityCorrection();
            updateControllerImpactStrength();
            detectHit();
            updateEnv();
            
            // Displace object
            this.gameObject.transform.Translate(displacement);
            if (hasGrappler)
            {
                grappler._base.updateGrapplerBase();
                grappler.hook.updateGrapplerHook(displacement);
                grappler.tether.updateGrappleTether();
            }
            if (hasProjectileLauncher)
            {
                projectileLauncher._base.updateFireArmBase();
            }
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
                    if (parentController.slow) { setStateSpeed(SpeedXs.walk, SpeedYs.zero); }
                    else { setStateSpeed(SpeedXs.run, SpeedYs.zero); };
                    break;
                case Controller.EnvState.Air:
                    setStateSpeed(SpeedXs.air, SpeedYs.rise);
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
                    if (parentController.rise) { envVelocity.y += (stateSpeed.y / 4) * Time.deltaTime; }
                    if (parentController.moveRight ^ parentController.moveLeft)
                    {
                        if (parentController.moveRight) { envVelocity.x += (stateSpeed.x / 4) * Time.deltaTime; }
                        else { envVelocity.x += -(stateSpeed.x / 4) * Time.deltaTime; }
                    }
                    break;
                default:
                    Debug.Log("Enviroment Missing");
                    break;
            }

            if (hasGrappler)
            {
                grappler.updateGrappler();
                if (grappler.grapplerState == Grappler.GrapplerStates.hookAttached)
                {
                    envVelocity.x += grappler.pullForce.x * Time.deltaTime;
                    envVelocity.y += grappler.pullForce.y * Time.deltaTime;
                }
            }
            if (hasProjectileLauncher)
            {
                projectileLauncher.updateProjectileLauncher();
                if (projectileLauncher.weaponFired)
                {
                    envVelocity.x += -projectileLauncher.recoil * (projectileLauncher.target.transform.position.x - projectileLauncher._base.anchor.x);
                    envVelocity.y += -projectileLauncher.recoil * (projectileLauncher.target.transform.position.y - projectileLauncher._base.anchor.y);
                }
            }
            envVelocity += physicsEngine.gravity.calculateGravity(this.transform.position) * Time.deltaTime;

            if (envVelocity.magnitude > maxEnvSpeed) { envVelocity = maxEnvSpeed * envVelocity.normalized; };
        }

        private void surfaceVelocityCorrection()
        {
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    if (Mathf.Abs(envVelocity.x) < 0.1)
                    {
                        envVelocity.x = 0;
                    }
                    else if (parentController.drop)
                    {
                        envVelocity.x = Mathf.Sign(envVelocity.x) * (Mathf.Abs(envVelocity.x) - (5 * Time.deltaTime));
                    }
                    else if (localCollisionManager.collisionData.bottomCollision)
                    {
                        envVelocity.x = Mathf.Sign(envVelocity.x) * (Mathf.Abs(envVelocity.x) - (15 * Time.deltaTime));
                    }

                    if (localCollisionManager.collisionData.horzCollision)
                    {
                        envVelocity.x = 0;
                    }
                    break;
                case Controller.EnvState.Air:
                    if (localCollisionManager.collisionData.topCollision)
                    {
                        envVelocity.y = -inputVelocity.y;
                        envVelocity.x = Mathf.Sign(envVelocity.x) * (Mathf.Abs(envVelocity.x) - (1 * Time.deltaTime));
                    }
                    else if (localCollisionManager.collisionData.horzCollision)
                    {
                        envVelocity.x = -envVelocity.x/4;
                    }
                    break;
            }
        }
        private void updateControllerImpactStrength()
        {
            parentController.impactStrengthPercent = 0f;
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    break;
                case Controller.EnvState.Air:
                    break;
                default:
                    Debug.Log("Enviroment Missing");
                    break;
            }
        }
        private void detectHit()
        {
            switch (parentController.env)
            {
                case Controller.EnvState.Ground:
                    if (parentController.rise || !localCollisionManager.collisionData.bottomCollision)
                    {
                        JumpStart_Event?.Invoke(Vector3.zero);
                    }
                    if (envVelocity.x > 0.5 || parentController.moveRight)
                    {
                        HitBottom_Event?.Invoke(localCollisionManager.collisionData.bottomCollisionPos + Vector3.left * (0.2f + 0.1f), true);
                    }
                    if (envVelocity.x < -0.5 || parentController.moveLeft)
                    {
                        HitBottom_Event?.Invoke(localCollisionManager.collisionData.bottomCollisionPos + Vector3.left * (0.2f - 0.1f), true);
                    }
                    if (localCollisionManager.collisionData.topCollision)
                    {
                        HitTop_Event?.Invoke(localCollisionManager.collisionData.topCollisionPos);
                    }
                    if (localCollisionManager.collisionData.rightCollision)
                    {
                        HitRight_Event?.Invoke(localCollisionManager.collisionData.rightCollisionPos);
                    }
                    if (localCollisionManager.collisionData.leftCollision)
                    {
                        HitLeft_Event?.Invoke(localCollisionManager.collisionData.leftCollisionPos);
                    }
                    break;
                case Controller.EnvState.Air:
                    if (localCollisionManager.collisionData.topCollision)
                    {
                        HitTop_Event?.Invoke(localCollisionManager.collisionData.topCollisionPos);
                    }
                    if (localCollisionManager.collisionData.bottomCollision)
                    {
                        HitBottom_Event?.Invoke(localCollisionManager.collisionData.bottomCollisionPos + Vector3.left * 0.2f, false);
                    }
                    if (localCollisionManager.collisionData.rightCollision)
                    {
                        HitRight_Event?.Invoke(localCollisionManager.collisionData.rightCollisionPos);
                    }
                    if (localCollisionManager.collisionData.leftCollision)
                    {
                        HitLeft_Event?.Invoke(localCollisionManager.collisionData.leftCollisionPos);
                    }
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

        private void StartOffGround(Vector3 jumpPoint) { }

        private void HitGround(Vector3 hitPoint) { }
    }
}