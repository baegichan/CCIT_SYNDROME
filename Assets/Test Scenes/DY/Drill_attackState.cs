using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_attackState : StateMachineBehaviour
{
    //일정 딜레이 이후 공격 하기
    //공격 하고 딜레이가 있게 만들기
    //가까이 가면 공격
    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Attack");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill.atkDelay = Drill.atkCooltime;
    }


}
