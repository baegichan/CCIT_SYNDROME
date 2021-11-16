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
    public float Bullet_Delay = 0.2f;
    public float Bomb_Cool = 30;//지뢰 설치 쿨타임


    public float Distance_To_Player = 150f;//어느 정도 거리에서 멈출꺼노?
    public float Dis;


    [Header("GameObject")]
    public GameObject Boss_Bullet;//보스 총알
    public GameObject Bomb_Obj;//수류탄
    public GameObject Black_Fog;


    [Header("Transform")]
    public Transform[] Bomb_transform = new Transform[4];//지뢰 날려주는 위치
    public Transform Player_Transform;//플레이어 위치
    public Transform Bullet_Transform;//플레이어 총구 위치
    public Transform Black_Fog_Transform;//플레이어 근접 검은안개 생성 위치


    void Start()
    {
        //Direction_Player();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        if (Boss_Active_on == true)
        {


            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//플레이어 위치 받아올수 있게

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis = Vector3.SqrMagnitude(Distance);//플레이어와 몬스터 거리 받아옴

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
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * Boss_Speed);
                    Left();
                }
                else if (Dis <= Distance_To_Player)
                {
                    Boss_Speed = 0;
                    Black_Fog_Ins();

                }
            }
            else if (Player_Transform.position.x > this.transform.position.x)
            {
                if (Dis > Distance_To_Player)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * Boss_Speed);
                    Right();
                }
                else if (Dis <= Distance_To_Player)
                {
                    Boss_Speed = 0;
                    Black_Fog_Ins();
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

    GameObject Black_Fog_Instance;
    void Black_Fog_Ins()//검은안개 생성
    {
        Black_Fog_Instance = Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);

        //Destroy
    }

    void Cool_Control()//쿨타임 컨트롤
    {
        Bullet_Cool -= Time.deltaTime;
        if (Bullet_Cool <= 0)
        {//애니메이션이 끝났는지도 확인

            //Boss 총알 발사 애니메이션 실행


            Boss_Speed = 0;//보스 움직임을 잠깐 멈추고

            //공격함수 실행
            Ins_Bullet();
            Bullet_Cool = 10;
        }

        if(Bomb_Throw_Count == 1)
        {

            if (Bomb_Cool > 0)
            {
                Bomb_Cool -= Time.deltaTime;
            }
            else if (Bomb_Cool <= 0)
            {
                //Bomb_Cool -= Time.deltaTime;
                if (Boss_Speed <= 0)
                {
                    Bomb_Throw_Count = 0;
                    Bomb_Cool = 30;
                    Throw_Bomb();
                }
            }
        }
        else
        {

        }
    }
    int Normal_Atk_Count;//총알 갯수
    void Ins_Bullet()//기본공격 총알 생성
    {
        if (Normal_Atk_Count < 4)
        {
            GameObject B1 = Instantiate(Boss_Bullet, Bullet_Transform.position, Quaternion.identity);//총알 생성 위치 조정 필요

            Normal_Atk_Count++;
            Invoke("Ins_Bullet", Bullet_Delay);//1,2,3

        }
        else if (Normal_Atk_Count == 4)
        {
            //if()애니메이션 끝났는지 확인 후

            Boss_Speed = 5;
            Normal_Atk_Count = 0;


        }

    }
    GameObject[] Bomb = new GameObject[4];

    int Bomb_Throw_Count = 1;
    void Throw_Bomb()
    {
        if (Bomb_Throw_Count == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                Bomb[i] = Instantiate(Bomb_Obj, transform.position, Quaternion.identity);//지뢰 넣어줘야함
                Bomb_Set(i);
            }
            Bomb_Throw_Count++;
        }
    }


    
  
    void Bomb_Set(int a)//지뢰 날려주기
    {
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].position * 1f,ForceMode2D.Impulse);
        //일단 속도가 0인 상태에서 지뢰를 뿌리고 일반공격 발사 할 때까지 속도 0으로 해놓고 몬스터 딜레이를 줌

    }


}
