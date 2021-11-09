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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(fireflyMon.player.position, fireflyTransform.position) > 4f) //4 이하라면 다시 되돌아 가기
        {
            animator.SetBool("Move", true);
            animator.SetBool("Follow", false);
        }
        else if (Vector2.Distance(fireflyMon.player.position, fireflyTransform.position) > 1f) //플레이어와의 거리가 1이하라면 대기
        {
            fireflyTransform.position = Vector2.MoveTowards(fireflyTransform.position, fireflyMon.player.position, Time.deltaTime * fireflyMon.speed);
        }
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("Follow", false);
        }
        fireflyMon.DirectionFireflymonster(fireflyMon.player.position.x, fireflyTransform.position.x);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
