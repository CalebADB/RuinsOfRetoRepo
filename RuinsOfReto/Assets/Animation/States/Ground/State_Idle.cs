using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is resting/stationary. Environment: Ground
    /// </summary>
    [CreateAssetMenu(fileName = "_Idle", menuName = "States/Ground/Idle")]
    public class State_Idle : StateData
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

            // Check to initiate Move
            checkToMove(animator, controller, stateBase.getAnimatorHashCodes());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
