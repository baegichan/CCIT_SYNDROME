using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_attack : MonoBehaviour
{
    public GameObject ball;//액자에서 나오는 원거리 공격
    
    //public int ball_speed =3;//공격 투사체 날라가는 속도
    public Transform player;
    //BossScript bs;

    private void Start()
    {
        //bs = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void new_ball()
    {
        Instantiate(ball, transform.position, Quaternion.identity);
    }



}
