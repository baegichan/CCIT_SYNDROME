using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Ready : StateMachineBehaviour
{
    Transform puncherTransform;
    Punchermonster puncherMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puncherMon = animator.GetComponent<Punchermonster>();
        puncherTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        {
            animator.SetTrigger("Attack");

            if (Vector2.Distance(puncherMon.player.position, puncherTransform.position) > 1f)
                animator.SetBool("Follow", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}