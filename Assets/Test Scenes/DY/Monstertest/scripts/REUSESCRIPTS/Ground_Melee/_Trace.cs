/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Trace : StateMachineBehaviour
{
    Transform ____Transform;
    ____Monster ____Mon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ____Mon = animator.GetComponent<____Monster>();
        ____Transform = animator.GetComponent<Transform>();

        ____Mon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (____Mon.filp == false || ____Mon.patroll == false)
        {
            ____Mon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (____Mon.Targeton == true)
        {
            if (Vector2.Distance(____Mon.player.position, ____Transform.position) > 5f) //플레이어 따라 오는 함수
                ____Transform.position = Vector2.MoveTowards(____Transform.position, ____Mon.player.position, Time.deltaTime * ____Mon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
*/
