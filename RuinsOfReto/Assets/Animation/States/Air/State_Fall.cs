using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is rising. Environment: air
    /// </summary>
    [CreateAssetMenu(fileName = "_Fall", menuName = "States/Aerial/Fall")]
    public class State_Fall : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
            // reset state parameters
            animator.SetBool(stateBase.getAnimatorHashCodes().collidedDown, false);
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