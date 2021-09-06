using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_idleState : StateMachineBehaviour
{
    //아이들 상태에서 움직이면 무브로 전환 
    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //속도가 바뀌면 움직이게 넣기
        if (Drill.speed > 0.1)
        {
            animator.SetBool("Move", true);
        }
        else if (Drill.speed < 0.1)
        {
            animator.SetBool("Move", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

 
}
