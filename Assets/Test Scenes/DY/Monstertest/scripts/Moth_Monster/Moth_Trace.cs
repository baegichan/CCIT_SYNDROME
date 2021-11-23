using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth_Trace : StateMachineBehaviour
{
    Transform MothTransform;
    MothMonster MothMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MothMon = animator.GetComponent<MothMonster>();
        MothTransform = animator.GetComponent<Transform>();

        MothMon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (MothMon.filp == false || MothMon.patroll == false)
        {
            MothMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MothMon.Targeton == true)
        {
            if (Vector2.Distance(MothMon.playerTransform.position, MothTransform.position) > 4f) //플레이어 따라 오는 함수
                MothTransform.position = Vector2.MoveTowards(MothTransform.position, MothMon.playerTransform.position, Time.deltaTime * MothMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        MothMon.DirectionMothmonster(MothMon.playerTransform.position.x, MothTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
