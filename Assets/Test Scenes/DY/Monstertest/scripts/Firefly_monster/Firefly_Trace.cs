using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Trace : StateMachineBehaviour
{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
         
        fireflyMon.patroll = false; //������ ��Ʈ���� ���� �� �ִ� �Լ��� �����ϰ� ���⼭ �޴´�.
        if (fireflyMon.filp == false || fireflyMon.patroll == false)
        {
            fireflyMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fireflyMon.Targeton == true)
        {
            if (Vector2.Distance(fireflyMon.playerTransform.position, fireflyTransform.position) > 4f) //�÷��̾� ���� ���� �Լ�
                fireflyTransform.position = Vector2.MoveTowards(fireflyTransform.position, fireflyMon.playerTransform.position, Time.deltaTime * fireflyMon.speed);
            else
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", false);
            }
        }
        fireflyMon.DirectionFireflymonster(fireflyMon.playerTransform.position.x, fireflyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
