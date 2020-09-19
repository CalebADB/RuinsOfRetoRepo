using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace masterFeature
{
    /// <summary>
    /// Initial state where the animator is told which Animation tree to go to
    /// </summary>
    [CreateAssetMenu(fileName = "ProjectileExplosion_Selection", menuName = "States/Selection/ProjectileExplosion_Selection")]
    public class ProjectileExplosion_Selection : StateData
    {
        public override void enterState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void updateState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {
            Explosion explosion = animator.gameObject.GetComponent<Explosion>();
            animator.SetInteger(stateBase.getAnimatorHashCodes().projectile, explosion.projectileType.GetHashCode());
        }

        public override void exitState(StateBase stateBase, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
