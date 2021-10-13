/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Ready : StateMachineBehaviour
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
        if (____Mon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (____Mon.Targeton == true)
        {
            if (Vector2.Distance(____Mon.player.position, ____Transform.position) > 2f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        drillMon.Directiondrillmonster(____Mon.player.position.x, ____Transform.position.x);
    }
}
*/