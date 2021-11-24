using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth_Move : StateMachineBehaviour

{
    Transform MothTransform;
    MothMonster MothMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MothMon = animator.GetComponent<MothMonster>();
        MothTransform = animator.GetComponent<Transform>();
        MothMon.patroll = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MothMon.Targeton == true)
        {
            if (Vector2.Distance(MothMon.first, MothTransform.position) < 0.1f || Vector2.Distance(MothTransform.position, MothMon.playerTransform.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                MothMon.DirectionMothmonster(MothMon.first.x, MothTransform.position.x);
                MothTransform.position = Vector2.MoveTowards(MothTransform.position, MothMon.first, Time.deltaTime * MothMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
