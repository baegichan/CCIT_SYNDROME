/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Move : StateMachineBehaviour
{
    Transform ____Transform;
    ____Monster ____Mon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ____Mon = animator.GetComponent<____Monster>();
        ____Transform = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (____Mon.Targeton == true)
        {
            if (Vector2.Distance(____Mon.first, ____Transform.position) < 0.1f || Vector2.Distance(____Transform.position, ____Mon.player.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                ____Mon.Directiondrillmonster(____Mon.first.x, ____Transform.position.x);
                ____Transform.position = Vector2.MoveTowards(____Transform.position, ____Mon.first, Time.deltaTime * drillMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
*/