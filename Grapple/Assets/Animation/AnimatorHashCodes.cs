using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    public class AnimatorHashCodes : MonoBehaviour
    {
        // Hashcodes for animation references KEEP ORDER CONSISTENT
        public int environment;
        public int velocityX;
        public int velocityY;
        public int moving;
        public int jumping;
        public int dropping;
        public int objectHolding;
        public int objectImmovable;
        public int collidedUp;
        public int collidedDown;

        private void Awake()
        {
            // Hashcode initialization KEEP ORDER CONSISTENT
            environment     = Animator.StringToHash("Environment");
            velocityX       = Animator.StringToHash("VelocityX");
            velocityY       = Animator.StringToHash("VelocityY");
            moving          = Animator.StringToHash("Moving");
            jumping         = Animator.StringToHash("Jumping");
            dropping        = Animator.StringToHash("Dropping");
            objectHolding   = Animator.StringToHash("ObjectHolding");
            objectImmovable = Animator.StringToHash("ObjectImmovable");
            collidedUp      = Animator.StringToHash("CollidedUp");
            collidedDown    = Animator.StringToHash("CollidedDown");
        }   
    }
}
