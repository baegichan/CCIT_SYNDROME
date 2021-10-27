using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Ready : StateMachineBehaviour
{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    float Dis_;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Dis_ = fireflyMon.player.position.sqrMagnitude - fireflyTransform.position.sqrMagnitude;
        if (fireflyMon.atkDelay <= 0)
            animator.SetTrigger(fireflyMon.attack);
        if (fireflyMon.Targeton == true)
        {
            if (Mathf.Abs(Dis_) > 10f * 10f) //따가라가서 공격 하는 범위
                animator.SetBool(fireflyMon.follow, true);
        }
        //fireflyMon.DirectionFireflymonster(fireflyMon.player.position.x, fireflyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}