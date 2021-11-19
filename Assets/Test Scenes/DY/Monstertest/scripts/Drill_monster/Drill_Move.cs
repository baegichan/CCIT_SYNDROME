using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Move : StateMachineBehaviour
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
        if(drillMon.Targeton ==true)
        {
            if (Vector2.Distance(drillMon.first, drillTransform.position) < 0.1f || Vector2.Distance(drillTransform.position, drillMon.PlayerT.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
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

