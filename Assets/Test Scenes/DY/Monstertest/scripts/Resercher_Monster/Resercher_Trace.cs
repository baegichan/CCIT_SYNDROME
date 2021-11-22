using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resercher_Trace : StateMachineBehaviour
{
    Transform ResercherTransform;
    ResercherMonster Reserchermon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Reserchermon = animator.GetComponent<ResercherMonster>();
        ResercherTransform = animator.GetComponent<Transform>();

        Reserchermon.patroll = false; //������ ��Ʈ���� ���� �� �ִ� �Լ��� �����ϰ� ���⼭ �޴´�.
        if (Reserchermon.filp == false || Reserchermon.patroll == false)
        {
            Reserchermon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        clupmon.patroll = false; //������ ��Ʈ���� ���� �� �ִ� �Լ��� �����ϰ� ���⼭ �޴´�.
        if (Vector2.Distance(clupmon.player.position, clupTransform.position) > 15f) //�÷��̾�� ������ �Ÿ� ���̰� 10�̻��̶��
        {
            animator.SetBool("Move", true);
            animator.SetBool("Follow", false);
        } 
        else if (Vector2.Distance(clupmon.player.position, clupTransform.position) > 5f)
            clupTransform.position = Vector2.MoveTowards(clupTransform.position, clupmon.player.position, Time.deltaTime * clupmon.speed);
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("Follow", false);
        }
        clupmon.DirectionClupmonster(clupmon.player.position.x, clupTransform.position.x);
        */
        if(Reserchermon.Targeton == true)
        {
            if (Vector2.Distance(Reserchermon.playerTransform.position, ResercherTransform.position) > 1.7f) //�÷��̾� ���� ���� �Լ�
                ResercherTransform.position = Vector2.MoveTowards(ResercherTransform.position, Reserchermon.playerTransform.position, Time.deltaTime * Reserchermon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        } 
        Reserchermon.DirectionReserchermonster(Reserchermon.playerTransform.position.x, ResercherTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
