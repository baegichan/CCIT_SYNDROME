using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Trace : StateMachineBehaviour
{
    Transform drillTransform;
    DrillMonster drillMon;
    //float Dis_;

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
        //Dis_ = drillMon.player.position.sqrMagnitude - drillTransform.position.sqrMagnitude;
        if (drillMon.Targeton == true)
        {
            if (Vector2.Distance(drillMon.player.position, drillTransform.position) > 5f) //정확한 값을 가지고 플레이어를 따라가야되기에 사용 Distance
                drillTransform.position = Vector2.MoveTowards(drillTransform.position, drillMon.player.position, Time.deltaTime * drillMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        drillMon.Directiondrillmonster(drillMon.player.position.x, drillTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

