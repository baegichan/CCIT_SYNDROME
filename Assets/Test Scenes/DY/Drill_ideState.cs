using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_idleState : StateMachineBehaviour
{
    //���̵� ���¿��� �����̸� ����� ��ȯ 
    Transform Drilltransform;
    Drillmonster Drill;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Drill = animator.GetComponent<Drillmonster>();
        Drilltransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //�ӵ��� �ٲ�� �����̰� �ֱ�
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
