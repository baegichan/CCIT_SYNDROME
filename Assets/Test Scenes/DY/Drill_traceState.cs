using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_traceState : StateMachineBehaviour
{
    //움직임은 먼저 패트롤로 처리
    //일정 거리 이상으로 들어오면 트레이스로 전환 계속 따라가게
    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(Drill.player.position, Drilltransform.position) < 4)
        {
            animator.SetBool("Follow", true);
        }
        Drilltransform.position = Vector2.MoveTowards(Drilltransform.position, Drill.player.position, Time.deltaTime * Drill.speed);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
