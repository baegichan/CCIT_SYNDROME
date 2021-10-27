using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Move : StateMachineBehaviour
{
    //몬스터 위치가 계속 해서 바뀔 수 있도록 고쳐야함
    Transform clupTransform;
    ClupMonster clupmon;
    float Dis_;
    float Dis2_;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        clupmon = animator.GetComponent<ClupMonster>();
        clupTransform = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Dis_ = clupmon.first.sqrMagnitude;
        Dis2_ = clupTransform.position.sqrMagnitude - clupmon.player.position.sqrMagnitude;
        if (clupmon.Targeton ==true)
        {
            if (Dis_ < 0.01f || Mathf.Abs(Dis2_) > 100f)
            {
                animator.SetBool(clupmon.move, false);
                animator.SetBool(clupmon.follow, true);
            }
            else
            {
                clupmon.DirectionClupmonster(clupmon.first.x, clupTransform.position.x);
                clupTransform.position = Vector2.MoveTowards(clupTransform.position, clupmon.first, Time.deltaTime * clupmon.speed);

            }
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    
}
