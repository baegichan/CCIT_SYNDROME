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
        if (puncherMon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (puncherMon.Targeton == true)
        {
            if (Vector2.Distance(puncherMon.playerTransform.position, puncherTransform.position) > 4f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }
        puncherMon.DirectionPunchermonster(puncherMon.playerTransform.position.x, puncherTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}