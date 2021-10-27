using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Move : StateMachineBehaviour

{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    float Dis_;
    float Dis2_;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
        //fireflyMon.trace = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Dis_ = fireflyMon.first.sqrMagnitude;
        Dis2_ = fireflyTransform.position.sqrMagnitude - fireflyMon.player.position.sqrMagnitude;
        //Debug.Log(Dis2_);

        if (fireflyMon.Targeton == true)
        {
            Debug.Log("Find     Dis : "+Dis_+ "     Dis2 : " + Dis2_);
            //if (Vector2.Distance(fireflyMon.first, fireflyTransform.position) < 0.1f || Vector2.Distance(fireflyTransform.position, fireflyMon.player.position) > 10f) //Ž�� ����
            if (Dis_ < 0.01f || Mathf.Abs(Dis2_) > 100f)//Ž�� ���� Distance�� �ƴ� sqrMagnitude�� ����ϰ� (a,b) > (�񱳰��� * �񱳰��� ����Ѵٸ� ���� ������ ���̴�.)
            {//���� sqrMagnitude�� ����ϸ� 4�ʰ��� �����̰� ����
             //Distance�� ����ϸ� ������ ���� �ٷ� �ν���
                Debug.Log("Find1");
                //���� �ִϸ��̼� Animator.StringToHash �̰��� �����Ͽ� ����ȭ�� ���� �� �Ű��� ����Ѵٰ� �����Ѵ�.
                //�� animator.SetBool("string"�� �ƴϰ� �콬���� �Ἥ ��Ʈ���� �޾Ƽ� ���� ������ ����ȭ�� �Ű��� ���°��� �����Ű���.)
                //public void SetBool(int id, bool value); �� �̰��� ����Ѵٰ� �����ϸ� �ȴ�.
                animator.SetBool(fireflyMon.move, false);
                animator.SetBool(fireflyMon.follow, true);
                Debug.Log("Find2");
            }
        }
        else
        {
            fireflyMon.DirectionFireflymonster(fireflyMon.first.x, fireflyTransform.position.x);
            fireflyTransform.position = Vector2.MoveTowards(fireflyTransform.position, fireflyMon.first, Time.deltaTime * fireflyMon.speed);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
