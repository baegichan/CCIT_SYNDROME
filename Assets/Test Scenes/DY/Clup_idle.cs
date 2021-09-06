using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_idle : StateMachineBehaviour
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
        if (Vector2.Distance(clupTransform.position, clup.player.position) <= 4)
            animator.SetBool("Follow", true);
        if (clup.speed > 0.1)
        {
            animator.SetBool("Move", true);
        }
        else if (clup.speed < 0.1)
        {
            animator.SetBool("Move", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
