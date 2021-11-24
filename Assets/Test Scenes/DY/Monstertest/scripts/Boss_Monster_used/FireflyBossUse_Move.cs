using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBossUse_Move : StateMachineBehaviour

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
        if (FireflyBossUseMon.Targeton == true)
        {
            if (Vector2.Distance(FireflyBossUseMon.first, FireflyBossUseTransform.position) < 0.1f || Vector2.Distance(FireflyBossUseTransform.position, FireflyBossUseMon.playerTransform.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                FireflyBossUseMon.DirectionFireflyBossUsemonster(FireflyBossUseMon.first.x, FireflyBossUseTransform.position.x);
                FireflyBossUseTransform.position = Vector2.MoveTowards(FireflyBossUseTransform.position, FireflyBossUseMon.first, Time.deltaTime * FireflyBossUseMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
