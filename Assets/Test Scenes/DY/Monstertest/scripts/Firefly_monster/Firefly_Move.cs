using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Move : StateMachineBehaviour

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
        if (fireflyMon.Targeton == true)
        {
            if (Vector2.Distance(fireflyMon.first, fireflyTransform.position) < 0.1f || Vector2.Distance(fireflyTransform.position, fireflyMon.player.position) > 10f)//Å½Áö ¹üÀ§
            {
                animator.SetBool("Move", false);
                animator.SetBool("Follow", true);
            }
            else
            {
                fireflyMon.DirectionFireflymonster(fireflyMon.first.x, fireflyTransform.position.x);
                fireflyTransform.position = Vector2.MoveTowards(fireflyTransform.position, fireflyMon.first, Time.deltaTime * fireflyMon.speed);

            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
