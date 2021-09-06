using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_move : StateMachineBehaviour
{
    Transform clupTransform;
    Clupmonster clup;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clup = animator.GetComponent<Clupmonster>();
        clupTransform = animator.GetComponent<Transform>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clupTransform.position = Vector2.MoveTowards(clupTransform.position, clup.player.position, Time.deltaTime * clup.speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
