using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Idle : StateMachineBehaviour
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
        if(drillMon.Targeton == false)
        {
            animator.SetBool(drillMon.move, true);
        }
        else if(drillMon.Targeton == true)
        {
            animator.SetBool(drillMon.follow, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
