using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is resting/stationary. Environment: Ground
    /// </summary>
    [CreateAssetMenu(fileName = "_Destroy", menuName = "States/Destroy")]
    public class State_Destroy : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Destroy(animator.gameObject);
        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
