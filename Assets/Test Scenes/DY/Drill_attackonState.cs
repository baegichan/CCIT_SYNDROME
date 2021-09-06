using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_attackonState : StateMachineBehaviour
{
    //실질적인 데미지가 들어가는 스크립트

    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //플레이어에게 데미지가 들어가게 만들기
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
