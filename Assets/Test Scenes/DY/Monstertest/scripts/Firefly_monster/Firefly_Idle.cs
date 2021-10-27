using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Idle : StateMachineBehaviour
{
    FireflyMonster fireflyMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fireflyMon.Targeton == false)
        {
            animator.SetBool(fireflyMon.move, true);
        }
        else if (fireflyMon.Targeton == true)
        {
            animator.SetBool(fireflyMon.follow, true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
