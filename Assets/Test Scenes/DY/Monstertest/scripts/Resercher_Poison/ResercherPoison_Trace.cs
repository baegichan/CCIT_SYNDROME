using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResercherPoison_Trace : StateMachineBehaviour
{
    Transform ResercherPoisonTransform;
    ResercherPoisonMonster ResercherPoisonmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResercherPoisonmon = animator.GetComponent<ResercherPoisonMonster>();
        ResercherPoisonTransform = animator.GetComponent<Transform>();

        ResercherPoisonmon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (ResercherPoisonmon.filp == false || ResercherPoisonmon.patroll == false)
        {
            ResercherPoisonmon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ResercherPoisonmon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (ResercherPoisonmon.Targeton == false)
            animator.SetTrigger("Clear");
        if (ResercherPoisonmon.Targeton == true)
        {
            if (ResercherPoisonmon.movable == true)
            {
                if (Vector2.Distance(ResercherPoisonmon.playerTransform.position, ResercherPoisonTransform.position) > 4f) //플레이어 따라 오는 함수
                    ResercherPoisonTransform.position = Vector2.MoveTowards(ResercherPoisonTransform.position, ResercherPoisonmon.playerTransform.position, Time.deltaTime * ResercherPoisonmon.speed);
                else
                {
                    animator.SetBool("Move", false);
                    animator.SetBool("Follow", false);
                }
            }
            ResercherPoisonmon.DirectionResercherPoisonmonster(ResercherPoisonmon.playerTransform.position.x, ResercherPoisonTransform.position.x);
        } 
        //Reserchermon.DirectionReserchermonster(Reserchermon.playerTransform.position.x, ResercherTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
