using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_attackonState : StateMachineBehaviour
{
    //�������� �������� ���� ��ũ��Ʈ

    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //�÷��̾�� �������� ���� �����
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
