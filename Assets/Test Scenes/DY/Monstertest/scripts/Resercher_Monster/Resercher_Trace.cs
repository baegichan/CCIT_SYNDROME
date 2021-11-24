using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resercher_Trace : StateMachineBehaviour
{
    Transform ResercherTransform;
    ResercherMonster Reserchermon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reserchermon = animator.GetComponent<ResercherMonster>();
        ResercherTransform = animator.GetComponent<Transform>();

        Reserchermon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (Reserchermon.filp == false || Reserchermon.patroll == false)
        {
            Reserchermon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Reserchermon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (Reserchermon.Targeton == false)
            animator.SetTrigger("Clear");
        if(Reserchermon.Targeton == true)
        {
            if (Reserchermon.movable == true)
            {
                if (Vector2.Distance(Reserchermon.playerTransform.position, ResercherTransform.position) > 1.3f) //플레이어 따라 오는 함수
                    ResercherTransform.position = Vector2.MoveTowards(ResercherTransform.position, Reserchermon.playerTransform.position, Time.deltaTime * Reserchermon.speed);
                else
                {
                    animator.SetBool("Move", false);
                    animator.SetBool("Follow", false);
                }
            }
            Reserchermon.DirectionReserchermonster(Reserchermon.playerTransform.position.x, ResercherTransform.position.x);
        } 
        //Reserchermon.DirectionReserchermonster(Reserchermon.playerTransform.position.x, ResercherTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
