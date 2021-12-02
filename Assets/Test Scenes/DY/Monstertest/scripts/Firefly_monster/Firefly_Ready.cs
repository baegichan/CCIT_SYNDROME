using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Ready : StateMachineBehaviour
{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fireflyMon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (fireflyMon.Targeton == true)
        {
            if (Vector2.Distance(fireflyMon.playerTransform.position, fireflyTransform.position) > 3f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        fireflyMon.DirectionFireflymonster(fireflyMon.playerTransform.position.x, fireflyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}