using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{


    [Header("Boss  'ON'   ")]
    public bool Boss_Active_on;//플레이어가 보스 방에 들어오기 전까지 False 
                               //플레이어가 보스방에 입장하면 True


    [Header("Prameter")]
    public int Boss_Speed = 5;
    public float Bullet_Cool = 10;//총알 쿨



    public float Distance_To_Player = 150f;//어느 정도 거리에서 멈출꺼노?
    public float Dis;


    public GameObject Boss_Bullet;
    public Transform Player_Transform;


    void Start()
    {
        //Direction_Player();
    }

    // Update is called once per frame
    void Update()
    {

        if (Boss_Active_on == true)
        {


            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//플레이어 위치 받아올수 있게

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis= Vector3.SqrMagnitude(Distance);//플레이어와 몬스터 거리 받아옴

            Cool_Control();//쿨 cool.....


            Trace_Player();
        }
    }
    
    void Trace_Player()//플레이어 추척
    {
        if (Boss_Speed != 0)
        {
            if (Player_Transform.position.x < this.transform.position.x)
            {
                if (Dis > Distance_To_Player)
                {
                    Boss_Speed = 5;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x,transform.position.y,transform.position.z),Time.deltaTime * Boss_Speed);
                    Left();
                }
                else if(Dis == Distance_To_Player)
                {
                    Boss_Speed = 0;
                }
            }
            else if (Player_Transform.position.x > this.transform.position.x)
            {
                if (Dis > Distance_To_Player)
                {
                    Boss_Speed = 5;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * Boss_Speed);
                    Right();
                }
                else if (Dis == Distance_To_Player)
                {
                    Boss_Speed = 0;
                }
            }
        }
        else
        {
            //가까워질만큼 가까워지면 실행~~
            //수류탄 던지기 나쁘지 않은듯
        }

    }
    void Left()//왼쪽으로 돌려이잇!
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    void Right()//오른쪽으로 돌려이잇!
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
    }


    void Cool_Control()
    {
        Bullet_Cool -= Time.deltaTime;
        if(Bullet_Cool <= 0)
        {//애니메이션이 끝났는지도 확인
            

            //공격함수 실행
            GameObject BB = Instantiate(Boss_Bullet, this.transform.position, Quaternion.identity);//총알 생성 위치 조정 필요
            //BB.transform.position = Vector3.left * 50;
            Bullet_Cool = 10;
        }
    }


}
