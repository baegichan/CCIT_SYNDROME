using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resercher_Ready : StateMachineBehaviour
{
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
        if(Reserchermon.atkDelay <=0)
        animator.SetTrigger("Attack");
        if(Reserchermon.Targeton == true)
        {
            if (Vector2.Distance(Reserchermon.playerTransform.position, ResercherTransform.position) > 0.5f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        Reserchermon.DirectionReserchermonster(Reserchermon.playerTransform.position.x, ResercherTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
