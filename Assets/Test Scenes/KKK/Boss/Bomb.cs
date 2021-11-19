using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    /*
    Rigidbody2D rig;

    public GameObject Player;
    Transform aa;

    Vector3 cc;

    public float BombSpeed;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        aa = Player.GetComponent<TestPlayer>().SelectChar.transform;
        cc = new Vector2(aa.position.x, aa.position.y);

        rig.AddForce(new Vector2(cc.x - this.transform.position.x, cc.y - this.transform.position.y) * BombSpeed);
    }
    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(rig.velocity.y, rig.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    *
    /*
    public Transform Bomb_Dir;
    public GameObject Player;
    public Transform Player_Dir;
    Transform aa;

    Vector3 cc;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        aa = Player.GetComponent<TestPlayer>().SelectChar.transform;


        cc = new Vector2(aa.position.x, aa.position.y);

    }
    Vector3 bb;
    void Lookat()
    {
        bb = new Vector2(cc.x - this.transform.position.x , cc.y - this.transform.position.y);

        //Bomb_Dir.right = bb;

    }
    private void Update()
    {
        Lookat();
        this.GetComponent<Rigidbody2D>().velocity = bb * 10;
    }
    */
    
    /*
    public Transform sunrise; //포물선 시작위치
    public Transform sunset; //포물선 종료위치
    Vector3 aa;
    public GameObject Player;
    public float journeyTime = 1000f; //시작위치에서 종료위치까지 도달하는 시간, 값이 높을수록 느리게 간다.
    private float startTime;
    public float reduceHeight = 3f; //Center값을 줄이기, 해당 값이 높을수록 포물선의 높이는 낮아진다.

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
        sunrise = this.transform;
        //sunset = Player.GetComponent<TestPlayer>().SelectChar.transform;//플레이어 위치 받아올수 있게
        aa = Player.GetComponent<TestPlayer>().SelectChar.transform.position;



    }
    Vector3 before;
    void Update()
    {
        Vector3 center = (sunrise.position + aa) * 0.01F; //Center 값만큼 위로 올라간다.
        center -= new Vector3(0, 0.1f * reduceHeight, 0); //y값을 높이면 높이가 낮아진다.
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = aa - center;
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, 0.05f);
        transform.position += center;
         

        if (Vector3.Distance(transform.position, setRelCenter) == 0)
        {
            Destroy(gameObject);
        }

        
    }
    
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(20);
                Destroy(gameObject);
                //Destroy(this.gameObject, 5);플레이어가 밟으면 터지는거 애니메이션 끝나면 없애줄꺼임// 5초는 그냥 설정
            }
            if (collision.CompareTag("Ground"))
            {
                
                //Destroy(gameObject);
            }
        
        }
    }
}
