using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Trace : StateMachineBehaviour
{
    Transform clupTransform;
    ClupMonster clupmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clupmon = animator.GetComponent<ClupMonster>();
        clupTransform = animator.GetComponent<Transform>();

        clupmon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
        if (clupmon.filp == false || clupmon.patroll == false)
        {
            clupmon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (clupmon.playerTransform == null)
            animator.SetTrigger("Clear");
        if (clupmon.Targeton == false)
            animator.SetTrigger("Clear");
        if (clupmon.Targeton == true)
        {
            if (Vector2.Distance(clupmon.playerTransform.position, clupTransform.position) > 1.7f) //플레이어 따라 오는 함수
                clupTransform.position = Vector2.MoveTowards(clupTransform.position, clupmon.playerTransform.position, Time.deltaTime * clupmon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        clupmon.DirectionClupmonster(clupmon.playerTransform.position.x, clupTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
