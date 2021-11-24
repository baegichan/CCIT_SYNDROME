using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth_Ready : StateMachineBehaviour
{
    Transform MothTransform;
    MothMonster MothMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MothMon = animator.GetComponent<MothMonster>();
        MothTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MothMon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (MothMon.Targeton == true)
        {
            if (Vector2.Distance(MothMon.playerTransform.position, MothTransform.position) > 6f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        MothMon.DirectionMothmonster(MothMon.playerTransform.position.x, MothTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}