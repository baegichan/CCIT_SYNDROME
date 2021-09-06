using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_ready : StateMachineBehaviour
{
    Transform clupTransform;
    Clupmonster clup;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clup = animator.GetComponent<Clupmonster>();
        clupTransform = animator.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (clup.atkDelay <= 0)
        {
            animator.SetTrigger("Attack");
        }

        if (Vector2.Distance(clup.player.position, clupTransform.position) > 1f)
        {
            animator.SetBool("Follow", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
