using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace masterFeature
{
    /// <summary>
    /// StateData Is the parent of all states. Each animation has a "StateBase" with a list of states. There states are the scripts that control the animation.
    /// </summary>
    public abstract class StateData : ScriptableObject
    {
        // Framework for the Beginning Middle and End of an animation state
        public abstract void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo);
        public abstract void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo);

        // accessible functions for all States
        public void checkToMove(Animator animator, Controller controller, AnimatorHashCodes animatorHashCodes)
        {
            // checks for movement input
            if (controller.moveRight ^ controller.moveLeft)
            {
                animator.SetBool(animatorHashCodes.moving, true);
            }
        }
        public void checkToJump(Animator animator, Controller controller, AnimatorHashCodes animatorHashCodes)
        {
            // checks for upward/rise/jump input
            if (controller.rise)
            {
                animator.SetBool(animatorHashCodes.jumping, true);
            }
        }
        public void checkToFall(Animator animator, Controller controller, AnimatorHashCodes animatorHashCodes)
        {
            // checks for upward/rise/jump input
            if (!controller.localPhysicsEngine.localCollisionManager.collisionData.bottomCollision)
            {
                animator.SetBool(animatorHashCodes.collidedDown, false);
            }
        }
        public void checkToDrop(Animator animator, Controller controller, AnimatorHashCodes animatorHashCodes)
        {
            Debug.Log("Im an empty function:)");
        }
        public void checkToIdle(Animator animator, Controller controller, AnimatorHashCodes animatorHashCodes)
        {
            // checks to see if movement has/is stopped
            if (!controller.moveRight ^ controller.moveLeft)
            {
                animator.SetBool(animatorHashCodes.moving, false);
            }
        }
    }
}
