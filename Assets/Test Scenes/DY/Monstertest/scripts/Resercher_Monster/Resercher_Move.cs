using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resercher_Move : StateMachineBehaviour
{
    //���� ��ġ�� ��� �ؼ� �ٲ� �� �ֵ��� ���ľ���
    Transform ResercherTransform;
    ResercherMonster Reserchermon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reserchermon = animator.GetComponent<ResercherMonster>();
        ResercherTransform = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Reserchermon.Targeton == true)
        {
            //���� �����ϰ� ��°� ���� ���� ���� ��� ���� �Ǵ� ������ �ƴ��� ���� �Ʒ� �ڵ� �����
            
            if (Vector2.Distance(Reserchermon.first, ResercherTransform.position) < 0.1f || Vector2.Distance(ResercherTransform.position, Reserchermon.playerTransform.position) > 10f)//Ž�� ����
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }     
        }
        if (Reserchermon.movable == true)
        {
              Reserchermon.DirectionReserchermonster(Reserchermon.first.x, ResercherTransform.position.x);
            if (Reserchermon.filp == true)
            {
                ResercherTransform.position = Vector2.MoveTowards(ResercherTransform.position, Reserchermon.first + Vector2.right, Time.deltaTime * Reserchermon.patrolSpeed);
            }
            else if(Reserchermon.filp == false)
            {
                ResercherTransform.position = Vector2.MoveTowards(ResercherTransform.position, Reserchermon.first + Vector2.left, Time.deltaTime * Reserchermon.patrolSpeed);
            }

        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}
