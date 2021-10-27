using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Move : StateMachineBehaviour
{
    Transform drillTransform;
    DrillMonster drillMon;
    float Dis_;
    float Dis2_;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        drillMon = animator.GetComponent<DrillMonster>();
        drillTransform = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Dis_ = drillMon.first.sqrMagnitude;
        Dis2_ = drillTransform.position.sqrMagnitude - drillMon.player.position.sqrMagnitude;

        if (drillMon.Targeton ==true)
        {
            if (Dis_ < 0.01f || Mathf.Abs(Dis2_) > 100f)
            {
                animator.SetBool(drillMon.move, false);
                animator.SetBool(drillMon.follow, true);
            }
            else
            {
                drillMon.Directiondrillmonster(drillMon.first.x, drillTransform.position.x);
                drillTransform.position = Vector2.MoveTowards(drillTransform.position, drillMon.first, Time.deltaTime * drillMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

