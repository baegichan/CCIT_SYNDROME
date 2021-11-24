using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResercherPoison_Move : StateMachineBehaviour
{
    //���� ��ġ�� ��� �ؼ� �ٲ� �� �ֵ��� ���ľ���
    Transform ResercherPoisonTransform;
    ResercherPoisonMonster ResercherPoisonmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResercherPoisonmon = animator.GetComponent<ResercherPoisonMonster>();
        ResercherPoisonTransform = animator.GetComponent<Transform>();
        ResercherPoisonmon.patroll = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (ResercherPoisonmon.Targeton == true)
        {
            //���� �����ϰ� ��°� ���� ���� ���� ��� ���� �Ǵ� ������ �ƴ��� ���� �Ʒ� �ڵ� �����
            
            if (Vector2.Distance(ResercherPoisonmon.first, ResercherPoisonTransform.position) < 0.1f || Vector2.Distance(ResercherPoisonTransform.position, ResercherPoisonmon.playerTransform.position) > 10f)//Ž�� ����
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }     
        }
        if (ResercherPoisonmon.movable == true)
        {
            ResercherPoisonmon.DirectionResercherPoisonmonster(ResercherPoisonmon.first.x, ResercherPoisonTransform.position.x);
            if (ResercherPoisonmon.filp == true)
            {
                ResercherPoisonTransform.position = Vector2.MoveTowards(ResercherPoisonTransform.position, ResercherPoisonmon.first + Vector2.right, Time.deltaTime * ResercherPoisonmon.patrolSpeed);
            }
            else if(ResercherPoisonmon.filp == false)
            {
                ResercherPoisonTransform.position = Vector2.MoveTowards(ResercherPoisonTransform.position, ResercherPoisonmon.first + Vector2.left, Time.deltaTime * ResercherPoisonmon.patrolSpeed);
            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}
