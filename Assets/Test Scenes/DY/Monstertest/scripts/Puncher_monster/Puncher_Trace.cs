using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Trace : StateMachineBehaviour
{
    Transform puncherTransform;
    Punchermonster puncherMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puncherMon = animator.GetComponent<Punchermonster>();
        puncherTransform = animator.GetComponent<Transform>();

        puncherMon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (puncherMon.filp == false || puncherMon.patroll == false)
        {
            puncherMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (puncherMon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (puncherMon.Targeton == false)
            animator.SetTrigger("Clear");
        if (puncherMon.Targeton == true)
        {
            if (Vector2.Distance(puncherMon.playerTransform.position, puncherTransform.position) > 4f) //플레이어 따라 오는 함수
                puncherTransform.position = Vector2.MoveTowards(puncherTransform.position, puncherMon.playerTransform.position, Time.deltaTime * puncherMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        //puncherMon.DirectionPunchermonster(puncherMon.playerTransform.position.x, puncherTransform.position.x);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
