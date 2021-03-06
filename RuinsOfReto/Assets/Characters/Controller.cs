﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace masterFeature
{
    [RequireComponent(typeof(LocalPhysicsEngine), typeof(Animator))]//,typeof(LocalSoundEngine) 
    public abstract class Controller : MonoBehaviour
    {
        // GameData:
        // Combat
        public int health;
        public float deathTimeLength;
        public bool canRespawn;
        public Vector3 spawn; 
        private float deathTimeCur; 
        private bool dying;


        // Physics:
        // Prep
        public LocalPhysicsEngine localPhysicsEngine;

        // Input
        public bool pause;

        public bool useHook;
        public bool useWeapon;

        public bool slow;

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

        public void start()
        {
            getLocalPhysicsEngine();
            animator = getAnimator();
            animatorHashCodes = GameObject.FindObjectOfType<AnimatorHashCodes>();

            localPhysicsEngine.JumpStart_Event += JumpStart;
            localPhysicsEngine.HitTop_Event += HitTop;
            localPhysicsEngine.HitBottom_Event += HitBottom;
            localPhysicsEngine.HitRight_Event += HitRight;
            localPhysicsEngine.HitLeft_Event += HitLeft;
        }

        private void JumpStart(Vector3 jumpPoint)
        {
            //Debug.Log("player jump");
        }

        private void HitTop(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitTopVFX(hitPoint);
        }

        private void HitBottom(Vector3 hitPoint, bool isMoving)
        {
            VFXManager.Instance.StartHitBottomVFX(hitPoint, isMoving);
        }

        private void HitRight(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitRightVFX(hitPoint);
        }

        private void HitLeft(Vector3 hitPoint)
        {
            VFXManager.Instance.StartHitLeftVFX(hitPoint);
        }

        private void Update()
        {
            update();
        }

        public void update()
        {
            if (!pause)
            {
                if (dying)
                {
                    die();
                }

                // Physics
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
        public void takeDamage(int damage = 1)
        {
            health -= damage;
            if (health <= 0)
            {
                dying = true;
            }
        }
        public void die()
        {
            deathTimeCur += Time.deltaTime;
            if (deathTimeCur > deathTimeLength)
            {
                if (canRespawn)
                {
                    transform.position = spawn;
                    health = 4;
                    localPhysicsEngine.envVelocity = Vector2.zero;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}