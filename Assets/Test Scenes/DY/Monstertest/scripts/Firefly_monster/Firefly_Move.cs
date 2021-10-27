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
            //if (Vector2.Distance(fireflyMon.first, fireflyTransform.position) < 0.1f || Vector2.Distance(fireflyTransform.position, fireflyMon.player.position) > 10f) //탐지 범위
            if (Dis_ < 0.01f || Mathf.Abs(Dis2_) > 100f)//탐지 범위 Distance가 아닌 sqrMagnitude를 사용하고 (a,b) > (비교값에 * 비교값을 사용한다면 더욱 빨라질 것이다.)
            {//현재 sqrMagnitude를 사용하면 4초가랑 딜레이가 생김
             //Distance를 사용하면 딜레이 없이 바로 인식함
                Debug.Log("Find1");
                //또한 애니매이션 Animator.StringToHash 이것을 공부하여 최적화에 더욱 더 신경을 써야한다고 생각한다.
                //즉 animator.SetBool("string"이 아니고 헤쉬값을 써서 인트형을 받아서 좀더 가볍고 최적화에 신경을 쓰는것이 좋을거같다.)
                //public void SetBool(int id, bool value); 즉 이것을 사용한다고 생각하면 된다.
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
