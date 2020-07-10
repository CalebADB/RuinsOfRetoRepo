using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    [RequireComponent(typeof(LocalPhysicsEngine), typeof(Animator))]//,typeof(LocalSoundEngine) 
    public class Controller : MonoBehaviour
    {
        // Physics:
        // Prep
        public LocalPhysicsEngine localPhysicsEngine;

        // Input
        public bool pause;
        public bool useHook;
        public bool moveRight;
        public bool moveLeft;
        public bool rise;
        public bool drop;

        /// <summary>
        /// impactStrengthPercent represents the severity of all impacts during the frame. 10% is a small impact, 20% is medium, 30% is high (but you could go higher) 
        /// </summary>
        public float impactStrengthPercent;

        // Environment
        public enum EnvState
        {
            Empty,
            Water,
            Ground,
            Hang,
            Air
        }
        public EnvState env;

        // Animation:
        // Prep
        private Animator animator;

        // HashCodes
        public AnimatorHashCodes animatorHashCodes;

        private void Start()
        {
            start();
        }

        public void start()
        {
            getLocalPhysicsEngine();
            animator = getAnimator();
            animatorHashCodes = GameObject.FindObjectOfType<AnimatorHashCodes>();
        }

        private void Update()
        {
            // Physics
            if (!pause)
            {
                localPhysicsEngine.updateEngine();
            }

            // Animation
            updateAnimatorParameters();
        }

        public LocalPhysicsEngine getLocalPhysicsEngine()
        {
            if (localPhysicsEngine == null)
            {
                localPhysicsEngine = this.gameObject.GetComponent<LocalPhysicsEngine>();
            }
            return localPhysicsEngine;
        }
        public Animator getAnimator()
        {
            if (animator == null)
            {
                animator = this.GetComponentInParent<Animator>();
            }
            return animator;
        }

        public void updateAnimatorParameters()
        {
            // SET animator velocity floats
            animator.SetFloat(animatorHashCodes.velocityX, localPhysicsEngine.velocity.x);
            animator.SetFloat(animatorHashCodes.velocityY, localPhysicsEngine.velocity.y);

            // SET animator vertical collision bools
            LocalCollisionManager.CollisionData collisionData = localPhysicsEngine.localCollisionManager.collisionData;
            if (collisionData.bottomCollision)
            {
                animator.SetBool(animatorHashCodes.collidedDown, true);
            }
            else if (collisionData.topCollision)
            {
                animator.SetBool(animatorHashCodes.collidedUp, true);
            }
            else
            {
                animator.SetBool(animatorHashCodes.collidedUp, false);
                animator.SetBool(animatorHashCodes.collidedDown, false);
            }

            // SET player sprite direction (Decide to mirror the sprite)
            if (moveRight && !moveLeft)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (!moveRight && moveLeft)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}

