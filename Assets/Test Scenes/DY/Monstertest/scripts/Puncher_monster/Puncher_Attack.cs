using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Attack : StateMachineBehaviour
{
    Transform puncherTransform;
    Punchermonster puncherMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puncherMon = animator.GetComponent<Punchermonster>();
        puncherTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (puncherMon.Targeton == true)
            puncherMon.DirectionPunchermonster(puncherMon.playerTransform.position.x, puncherTransform.position.x);
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puncherMon.atkDelay = puncherMon.atkCooltime;
    }
}

