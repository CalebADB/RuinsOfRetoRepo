using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Initial state where the animator is told which Animation tree to go to
    /// </summary>
    [CreateAssetMenu(fileName = "New File", menuName = "States/EnvironmentSelection")]
    public class State_EnvironmentSelection : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
            animator.SetInteger(stateBase.getAnimatorHashCodes().environment, controller.env.GetHashCode());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
