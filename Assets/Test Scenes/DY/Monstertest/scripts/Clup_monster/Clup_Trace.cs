using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Trace : StateMachineBehaviour
{
    Transform clupTransform;
    ClupMonster clupmon;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clupmon = animator.GetComponent<ClupMonster>();
        clupTransform = animator.GetComponent<Transform>();

        clupmon.patroll = false; //������ ��Ʈ���� ���� �� �ִ� �Լ��� �����ϰ� ���⼭ �޴´�.
        if (clupmon.filp == false || clupmon.patroll == false)
        {
            clupmon.transform.eulerAngles = new Vector3(0, 0, 0);
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
        if(clupmon.Targeton == true)
        {
            if (Vector2.Distance(clupmon.player.position, clupTransform.position) > 1.4f) //�÷��̾� ���� ���� �Լ�
                clupTransform.position = Vector2.MoveTowards(clupTransform.position, clupmon.player.position, Time.deltaTime * clupmon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        clupmon.DirectionClupmonster(clupmon.player.position.x, clupTransform.position.x);
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
