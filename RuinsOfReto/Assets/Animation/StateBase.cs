using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Statebase is the script that facilitates the communication between its contained states and its parent animator.
    /// </summary>
    public class StateBase : StateMachineBehaviour
    {
        // protected objects
        private Controller controller;
        public Controller getController(Animator animator)
        {
            if (controller == null)
            {
                controller = animator.GetComponentInParent<Controller>();
            }
            return controller;
        }
        private AnimatorHashCodes animatorHashCodes;
        public AnimatorHashCodes getAnimatorHashCodes()
        {
            if (animatorHashCodes == null)
            {
                animatorHashCodes = GameObject.Find("HashCodes").GetComponent<AnimatorHashCodes>();
            }
            return animatorHashCodes;
        }

        // Container for States (the list allows for modularity)
        public List<StateData> states = new List<StateData>();

        // Key animation functions that are overridden. 
        // This is done so StateBase can update a LIST of states, additionally allowing for modularity
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            enterStates(this, animator, stateInfo);
        }
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            updateStates(this, animator, stateInfo);
        }
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            exitStates(this, animator, stateInfo);
        }

        // Runs the corresponding functions for each state in the state list (states).
        public void enterStates(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData state in states)
            {
                state.enterState(stateBase, animator, stateInfo);
            }
        }
        public void updateStates(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData state in states)
            {
                state.updateState(stateBase, animator, stateInfo);
            }
        }
        public void exitStates(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StateData state in states)
            {
                state.exitState(stateBase, animator, stateInfo);
            }
        }
    }
}
