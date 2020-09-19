using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Initial state where the animator is told which Animation tree to go to
    /// </summary>
    [CreateAssetMenu(fileName = "Projectile_Selection", menuName = "States/Selection/Projectile_Selection")]
    public class Projectile_Selection : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Projectile projectile = animator.gameObject.GetComponent<Projectile>();
            animator.SetInteger(stateBase.getAnimatorHashCodes().projectile, projectile.getProjectileType().GetHashCode());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
