using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResercherPoison_Dead : StateMachineBehaviour
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
        ResercherPoisonmon.DirectionResercherPoisonmonster(ResercherPoisonmon.playerTransform.position.x, ResercherPoisonTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
