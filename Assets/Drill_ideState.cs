using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_idleState : StateMachineBehaviour
{
    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Drilltransform.position, Drill.player.position) <= 4)
            animator.SetBool("Follow", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

 
}
