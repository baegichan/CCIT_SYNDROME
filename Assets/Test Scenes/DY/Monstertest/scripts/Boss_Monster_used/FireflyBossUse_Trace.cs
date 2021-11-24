using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBossUse_Trace : StateMachineBehaviour
{
    Transform FireflyBossUseTransform;
    FireflyBossUseMonster FireflyBossUseMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FireflyBossUseMon = animator.GetComponent<FireflyBossUseMonster>();
        FireflyBossUseTransform = animator.GetComponent<Transform>();

        FireflyBossUseMon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (FireflyBossUseMon.filp == false || FireflyBossUseMon.patroll == false)
        {
            FireflyBossUseMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (FireflyBossUseMon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (FireflyBossUseMon.Targeton == false)
            animator.SetTrigger("Clear");
        if (FireflyBossUseMon.Targeton == true)
        {
            if (Vector2.Distance(FireflyBossUseMon.playerTransform.position, FireflyBossUseTransform.position) > 2f) //플레이어 따라 오는 함수
                FireflyBossUseTransform.position = Vector2.MoveTowards(FireflyBossUseTransform.position, FireflyBossUseMon.playerTransform.position, Time.deltaTime * FireflyBossUseMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        FireflyBossUseMon.DirectionFireflyBossUsemonster(FireflyBossUseMon.playerTransform.position.x, FireflyBossUseTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
