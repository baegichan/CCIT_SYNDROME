using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Attack : StateMachineBehaviour
{
    Transform fireflyTransform;
    FireflyMonster fireflyMon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon = animator.GetComponent<FireflyMonster>();
        fireflyTransform = animator.GetComponent<Transform>();
        //Instantiate(fireflyMon.fireflybullet, fireflyMon.atkpos.transform.position, Quaternion.identity);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fireflyMon.atkDelay <= 0)
        {
            //Instantiate(fireflyMon.fireflybullet, fireflyMon.atkpos.transform.position, Quaternion.identity);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireflyMon.atkDelay = fireflyMon.atkCooltime;
    }
}
