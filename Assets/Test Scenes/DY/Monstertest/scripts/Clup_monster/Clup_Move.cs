using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Move : StateMachineBehaviour
{
    //몬스터 위치가 계속 해서 바뀔 수 있도록 고쳐야함
    Transform clupTransform;
    ClupMonster clupmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clupmon = animator.GetComponent<ClupMonster>();
        clupTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //각도 조절하고 쏘는거 구현 벽이 있을 경우 감지 되는 벽인지 아닌지 인지 아래 코드 지우고
        if(clupmon.Targeton ==true)
        {
            if (Vector2.Distance(clupmon.first, clupTransform.position) < 0.1f || Vector2.Distance(clupTransform.position, clupmon.player.position) > 10f)//탐지 범위
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                clupmon.DirectionClupmonster(clupmon.first.x, clupTransform.position.x);
                clupTransform.position = Vector2.MoveTowards(clupTransform.position, clupmon.first, Time.deltaTime * clupmon.speed);

            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}
