using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBossUse_Ready : StateMachineBehaviour
{
    Transform FireflyBossUseTransform;
    FireflyBossUseMonster FireflyBossUseMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FireflyBossUseMon = animator.GetComponent<FireflyBossUseMonster>();
        FireflyBossUseTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (FireflyBossUseMon.atkDelay <= 0)
            animator.SetTrigger("Attack");
        if (FireflyBossUseMon.Targeton == true)
        {
            if (Vector2.Distance(FireflyBossUseMon.playerTransform.position, FireflyBossUseTransform.position) > 2f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        FireflyBossUseMon.DirectionFireflyBossUsemonster(FireflyBossUseMon.playerTransform.position.x, FireflyBossUseTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}