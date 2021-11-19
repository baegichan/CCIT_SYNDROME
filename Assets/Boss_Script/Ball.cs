using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform palyer;//플레이어 위치
    public float ball_speed;//구체 날라가는 속도
    //static public int number_of_Tri = 0;//충돌처리로 몇개 삭제 됬는지 
    float ab;//아래로 쏴주는 부채꼴 랜덤 각도
    BossScript bs;
    private void Start()
    {
        //bs = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        //palyer = GameObject.FindGameObjectWithTag("Player").transform;
        //ab = Random.Range(-90.0f, 90.0f);

    }
    private void Update()
    {
        
        Vector2 ball_move = Vector2.down * (ball_speed * 10) * Time.deltaTime;

        Quaternion aaa = Quaternion.Euler(0, 0, ab);
        Vector2 ball_move2 = aaa * ball_move;

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(ball_move2.x, ball_move2.y);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
          
        }
        
    }
   
}
