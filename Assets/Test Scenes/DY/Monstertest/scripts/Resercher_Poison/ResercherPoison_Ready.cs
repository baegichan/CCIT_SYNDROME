using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResercherPoison_Ready : StateMachineBehaviour
{
    Transform ResercherPoisonTransform;
    ResercherPoisonMonster ResercherPoisonmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResercherPoisonmon = animator.GetComponent<ResercherPoisonMonster>();
        ResercherPoisonTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ResercherPoisonmon.atkDelay <=0)
        animator.SetTrigger("Attack");
        if(ResercherPoisonmon.Targeton == true)
        {
            if (Vector2.Distance(ResercherPoisonmon.playerTransform.position, ResercherPoisonTransform.position) > 4f) //따가라가서 공격 하는 범위
                animator.SetBool("Follow", true);
        }


        ResercherPoisonmon.DirectionResercherPoisonmonster(ResercherPoisonmon.playerTransform.position.x, ResercherPoisonTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
