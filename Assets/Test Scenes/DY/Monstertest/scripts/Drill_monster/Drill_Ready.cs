using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Ready : StateMachineBehaviour
{
    Transform drillTransform;
    DrillMonster drillMon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drillMon = animator.GetComponent<DrillMonster>();
        drillTransform = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (drillMon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (drillMon.Targeton == true)
        {
            if (Vector2.Distance(drillMon.player.position, drillTransform.position) > 0.7f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        drillMon.Directiondrillmonster(drillMon.player.position.x, drillTransform.position.x);
    }
}


