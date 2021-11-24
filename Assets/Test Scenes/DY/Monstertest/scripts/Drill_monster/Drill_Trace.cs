using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Trace : StateMachineBehaviour
{
    Transform drillTransform;
    DrillMonster drillMon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drillMon = animator.GetComponent<DrillMonster>();
        drillTransform = animator.GetComponent<Transform>();

        drillMon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (drillMon.filp == false || drillMon.patroll == false)
        {
            drillMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (drillMon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (drillMon.Targeton == false)
            animator.SetTrigger("Clear");
        if (drillMon.Targeton == true)
        {
            if (Vector2.Distance(drillMon.playerTransform.position, drillTransform.position) > 1.7f) //플레이어 따라 오는 함수
                drillTransform.position = Vector2.MoveTowards(drillTransform.position, drillMon.playerTransform.position, Time.deltaTime * drillMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        drillMon.Directiondrillmonster(drillMon.playerTransform.position.x, drillTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

