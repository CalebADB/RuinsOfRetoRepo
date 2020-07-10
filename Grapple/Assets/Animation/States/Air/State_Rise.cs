using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is rising. Environment: Air
    /// </summary>
    [CreateAssetMenu(fileName = "_Rise", menuName = "States/Aerial/Rise")]
    public class State_Rise : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
            // reset state parameters
            animator.SetBool(stateBase.getAnimatorHashCodes().collidedUp, false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}