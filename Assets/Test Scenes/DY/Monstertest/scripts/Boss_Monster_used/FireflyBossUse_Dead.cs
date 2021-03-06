using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBossUse_Dead : StateMachineBehaviour
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
        FireflyBossUseMon.DirectionFireflyBossUsemonster(FireflyBossUseMon.playerTransform.position.x, FireflyBossUseTransform.position.x);
        //아직 죽는 모션이 없음

        //현재 죽는 애니메이션이 없기 때문에 아직 구현 X

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
