using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Ready : StateMachineBehaviour
{
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
        if(clupmon.atkDelay <=0)
        animator.SetTrigger("Attack");

        if (Vector2.Distance(clupmon.player.position, clupTransform.position) > 5f) //�����󰡼� ���� �ϴ� ����
            animator.SetBool("Follow", true);

        clupmon.DirectionClupmonster(clupmon.player.position.x, clupTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
