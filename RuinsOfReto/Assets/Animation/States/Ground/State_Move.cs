using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is moving laterally. Environment: Ground
    /// </summary>
    [CreateAssetMenu(fileName = "_Move", menuName = "States/Ground/Move")]
    public class State_Move : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
            // reset state parameters
            animator.SetBool(stateBase.getAnimatorHashCodes().jumping, false);
        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);

            // Can initiate Jump
            checkToJump(animator, controller, stateBase.getAnimatorHashCodes());

            // Can fall off cliff
            checkToFall(animator, controller, stateBase.getAnimatorHashCodes());

            // Check to return to idle
            checkToIdle(animator, controller, stateBase.getAnimatorHashCodes());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}