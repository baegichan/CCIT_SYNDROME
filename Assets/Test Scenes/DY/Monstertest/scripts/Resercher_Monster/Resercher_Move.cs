using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resercher_Move : StateMachineBehaviour
{
    //몬스터 위치가 계속 해서 바뀔 수 있도록 고쳐야함
    Transform ResercherTransform;
    ResercherMonster Reserchermon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reserchermon = animator.GetComponent<ResercherMonster>();
        ResercherTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //각도 조절하고 쏘는거 구현 벽이 있을 경우 감지 되는 벽인지 아닌지 인지 아래 코드 지우고
        if(Reserchermon.Targeton ==true)
        {
            if (Vector2.Distance(Reserchermon.first, ResercherTransform.position) < 0.1f || Vector2.Distance(ResercherTransform.position, Reserchermon.playerTransform.position) > 10f)//탐지 범위
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                Reserchermon.DirectionReserchermonster(Reserchermon.first.x, ResercherTransform.position.x);
                ResercherTransform.position = Vector2.MoveTowards(ResercherTransform.position, Reserchermon.first, Time.deltaTime * Reserchermon.speed);

            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}
