using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// State in which the object is moving laterally. Environment: Ground
    /// </summary>
    [CreateAssetMenu(fileName = "_Slide", menuName = "States/Ground/Slide")]
    public class State_Slide : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Controller controller = stateBase.getController(animator);
            BoxCollider2D boxCollider2D = controller.GetComponent<BoxCollider2D>();
            boxCollider2D.offset = new Vector2(0f, 0.08f);
            boxCollider2D.size = new Vector2(0.16f, 0.16f);

            // Can initiate Jump
            checkToJump(animator, controller, stateBase.getAnimatorHashCodes());

            checkToStand(animator, controller, stateBase.getAnimatorHashCodes());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            BoxCollider2D boxCollider2D = stateBase.getController(animator).GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(0.16f, 0.41f);
            boxCollider2D.offset = new Vector2(0, 0.21f);
        }
    }
}