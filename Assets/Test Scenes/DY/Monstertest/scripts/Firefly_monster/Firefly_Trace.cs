using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Trace : StateMachineBehaviour
{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    //float Dis_;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
        fireflyMon.patroll = false; //몬스터의 패트롤을 끝낼 수 있는 함수를 생성하고 여기서 받는다.
       
        if (fireflyMon.filp == false || fireflyMon.patroll == false)
        {
            fireflyMon.transform.eulerAngles = new Vector3(0, 0, 0);
        }
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Dis_ = fireflyMon.player.position.sqrMagnitude - fireflyTransform.position.sqrMagnitude;
        if (fireflyMon.Targeton == true)
        {
            if (Vector2.Distance(fireflyMon.player.position, fireflyTransform.position) >  15f) //정확한 값을 가지고 플레이어를 따라가야되기에 사용 Distance
            //if (Mathf.Abs(Dis_) > 225f)
                fireflyTransform.position = Vector2.MoveTowards(fireflyTransform.position, fireflyMon.player.position, Time.deltaTime * fireflyMon.speed);
            else
            {
                animator.SetBool(fireflyMon.move, false);
                animator.SetBool(fireflyMon.follow, false);
            }
        }
        fireflyMon.DirectionFireflymonster(fireflyMon.player.position.x, fireflyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
