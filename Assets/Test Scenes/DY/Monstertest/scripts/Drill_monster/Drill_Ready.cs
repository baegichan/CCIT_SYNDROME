using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Ready : StateMachineBehaviour
{
    Transform drillTransform;
    DrillMonster drillMon;
    float Dis_;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drillMon = animator.GetComponent<DrillMonster>();
        drillTransform = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Dis_ = drillMon.player.position.sqrMagnitude - drillTransform.position.sqrMagnitude;
        if (drillMon.atkDelay <= 0)
            animator.SetTrigger(drillMon.attack);
        if (drillMon.Targeton == true)
        {
            if (Vector2.Distance(drillMon.player.position, drillTransform.position) > 2f) //�����󰡼� ���� �ϴ� ����
                animator.SetBool(drillMon.follow, true);
        }


        drillMon.Directiondrillmonster(drillMon.player.position.x, drillTransform.position.x);
    }
}


