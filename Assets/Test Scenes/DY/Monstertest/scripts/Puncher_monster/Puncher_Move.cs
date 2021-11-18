using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Move : StateMachineBehaviour
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
        if (puncherMon.Targeton == true)
        {
            if (Vector2.Distance(puncherMon.first, puncherTransform.position) < 0.1f || Vector2.Distance(puncherTransform.position, puncherMon.player.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                puncherMon.DirectionPunchermonster(puncherMon.first.x, puncherTransform.position.x);
                puncherTransform.position = Vector2.MoveTowards(puncherTransform.position, puncherMon.first, Time.deltaTime * puncherMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}