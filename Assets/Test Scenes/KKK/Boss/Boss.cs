using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{



    public bool Boss_Active_on;//플레이어가 보스 방에 들어오기 전까지 False 
                               //플레이어가 보스방에 입장하면 True




    [Header("Prameter")]
    //public int Boss_Speed = 5;
    public float Bullet_Cool = 10;//총알 쿨
    public float Bullet_Delay = 0.2f;
    public float Bomb_Cool = 30;//지뢰 설치 쿨타임
    public float Black_Fog_Cool = 0;//검은 안개 생성 쿨타임 // 초기 설정 0 이고 30초로 성정
    
    //var aa = GameManager.instance;

    public float Distance_To_Player = 50f;//어느 정도 거리에서 멈출꺼노?
    public float Dis;


    [Header("Boss_Info")]
    public int Boss_HP;
    public int Boss_Currnent_HP;


    int Stom_Count = 0;
    int Bomb_Throw_Count = 1;

    [Header("GameObject")]
    public GameObject Boss_Bullet;//보스 총알
    public GameObject Bomb_Obj;//수류탄
    public GameObject Black_Fog;//검은 안개
    public GameObject Stom_Obj;//검은 소용돌이
    public GameObject Big_Stom_Obj;
    public GameObject FireFly_Monster;




    [Header("Transform")]
    public Transform[] Bomb_transform = new Transform[4];//지뢰 날려주는 위치
    public Transform Player_Transform;//플레이어 위치
    public Transform Bullet_Transform;//플레이어 총구 위치
    public Transform Black_Fog_Transform;//플레이어 근접 검은안개 생성 위치
    public Transform Stom_Transform;//Frame 생성 위치 // 0,0,0



    bool Boss_HP_Half;
    bool Check_Shot_bullet = true;


    void Start()
    {
        //Direction_Player();
        //BM = GameManager.instance;

        speed = 5;
        Hp_Max = Boss_HP;
        Hp_Current = Boss_Currnent_HP;
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
            Hp_Current -= 2;

            Stoming();

            Monster_HP_Check();

            /*
            if (Input.GetMouseButtonDown(0))
            {
                Bomb_On = true;
                Throw_Bomb();
            }
            */
        }
    }
    void Monster_HP_Check()
    {
        if (Hp_Current <= Hp_Max * 0.5f)
        {
            if (Boss_HP_Half == false)
            {
                Boss_HP_Half = true;
                StartCoroutine(Respawn_Monster());
            }
        }
    }
    IEnumerator Respawn_Monster()//몬스터 리스폰 쿨타임 10초로 잡아놈
    {

        Instantiate(FireFly_Monster, new Vector3(-50, 50, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(50, 50, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(-80, 25, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(80, 25, 0), Quaternion.identity);


        yield return new WaitForSeconds(25f);
        Boss_HP_Half = false;
    }
    

    void Trace_Player()//플레이어 추척
    {
        if (speed != 0)
        {
            if (Player_Transform.position.x < this.transform.position.x)
            {
                if (Dis > Distance_To_Player)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    Left();
                    Check_Shot_bullet = true;
                }
                else if (Dis <= Distance_To_Player)
                {
                    speed = 0;
                    Check_Shot_bullet = false;

                    if (Black_Fog_Cool <= 0 && Bomb_Cool <= 0)
                    {
                        Bomb_Cool = 10;

                        Invoke("Black_Fog_Ins",1f);
                    }
                    else if(Black_Fog_Cool <= 0 || Bomb_Cool <= 0)
                    {
                        if (Black_Fog_Cool <= 0)
                        {
                            if(Bomb_Cool <= 5)
                            Bomb_Cool += 10;

                            Invoke("Black_Fog_Ins", 1f);
                        }
                        else if (Bomb_Cool <= 0)
                        {
                            if(Black_Fog_Cool<= 5)
                            Black_Fog_Cool += 10;

                            Invoke("Throw_Bomb", 1f);
                        }
                    }
                    /*
                    if(Black_Fog_Cool <= 0)
                       Black_Fog_Ins();
                    else
                    {
                        if (Bomb_Throw_Count == 0)
                        {
                            Throw_Bomb();
                        }
                        //사라지는 애니메이션 틀어줌
                        //Fade_In_and_Translate(Left_Transform); 요걸 사라아지는 애니메이션 중간 완벽하게 안보였을때 실행해줌
                        //speed_back 인보크로 틀어줌
                    }
                    */
                }
            }
            else if (Player_Transform.position.x > this.transform.position.x)
            {
                if (Dis > Distance_To_Player)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    Right();
                    Check_Shot_bullet = true;

                }
                else if (Dis <= Distance_To_Player)
                {
                    speed = 0;
                    Check_Shot_bullet = false;

                    if (Black_Fog_Cool <= 0 && Bomb_Cool <= 0)
                    {
                        Bomb_Cool = 10;

                        Invoke("Black_Fog_Ins",1);
                    }
                    else if (Black_Fog_Cool <= 0 || Bomb_Cool <= 0)
                    {
                        if (Black_Fog_Cool <= 0)
                        {
                            if(Bomb_Cool <= 5)
                            Bomb_Cool += 10;

                            Invoke("Black_Fog_Ins",1);
                        }
                        else if (Bomb_Cool <= 0)
                        {
                            if(Black_Fog_Cool <= 5)
                            Black_Fog_Cool += 10;

                            Invoke("Throw_Bomb",1);
                        }
                    }
                }
            }
        }

    }


    void speed_back()//Invoke용
    {
        speed = 5;
    }

    void Cool_Control()//쿨타임 컨트롤
    {
        Bullet_Cool -= Time.deltaTime;
        if (Bullet_Cool <= 0)
        {//애니메이션이 끝났는지도 확인

            //Boss 총알 발사 애니메이션 실행


            speed = 0;//보스 움직임을 잠깐 멈추고

            //공격함수 실행
            if (Check_Shot_bullet == true) { Ins_Bullet(); }
            else
            {
                Invoke("speed_back", 1f);
            }
            Bullet_Cool = 10;
        }

            if (Bomb_Cool > 0)
            {
                Bomb_Cool -= Time.deltaTime;
            }
          
        
        if (Black_Fog_Cool > 0)
        {
            Black_Fog_Cool -= Time.deltaTime;
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

            speed = 5;
            Normal_Atk_Count = 0;


        }
        Invoke("speed_back", 1f);
    }
    bool Fog_On = true;
    void Black_Fog_Ins()//검은안개 생성
    {

        if (Fog_On == true)
        {

            Debug.Log("Fog");
            Check_Shot_bullet = false;
            //애니메이션 on
            Invoke("Fog", 0.5f);


            //애니메이션 끝나면
            //Avoid();
            Invoke("Avoid", 5f);//애니메이션 끝나는시간으로 바꾸고
            Fog_On = false;
        }

    }
    void Fog()
    {
        Black_Fog_Instance = Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);
    }
    int Translate_Count = 0;
    void Avoid()
    {
        Invoke("Translate_Boss", 5f);//완벽하게 사라지는 시간으로 바꾸고
       
    }
    void Translate_Boss()
    {

        if(this.transform.position.x < 0)
        {
            this.transform.position = new Vector3(85, this.transform.position.y, this.transform.position.z);
            speed = 5;

            if (Black_Fog_Cool <= 0)
                Black_Fog_Cool = 30;
            else if (Bomb_Cool <= 0)
                Bomb_Cool = 30;

            Fog_On = true;
                Check_Shot_bullet = true;
                    Translate_Count = 0;
        }
            else if(this.transform.position.x > 0)
        {
            this.transform.position = new Vector3(-85, this.transform.position.y, this.transform.position.z);
            speed = 5;

            if (Black_Fog_Cool <= 0)
                Black_Fog_Cool = 30;
            else if (Bomb_Cool <= 0)
                Bomb_Cool = 30;

            Fog_On = true;
            Check_Shot_bullet = true;
                      Translate_Count = 0;

            }
    }

    GameObject frame;
    GameObject Big_frame;
    void Stoming()//액자 생성
    {
        if (Stom_Count == 0)
            if (Hp_Current <= Hp_Max * 0.75)
            {
                //위로 총 두번 두는 애니메이션 틀고
                speed = 0;

                frame = (GameObject)Instantiate(Stom_Obj,new Vector3(0,30,0), Quaternion.identity);


                frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);
                //Invoke(frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject),2f);

                Stom_Count++;

                //애니메이션 끝나면
                speed = 5;
            }
        if (Stom_Count == 1)
        {
            if(Hp_Current <= Hp_Max * 0.5)
            {
                //심연으로 바뀌고 
                Destroy(frame);
                Big_frame = (GameObject)Instantiate(Big_Stom_Obj, new Vector3(0, 30, 0), Quaternion.identity);

                Big_frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                Big_frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                Stom_Count++;
            }
        }
    }
   
    int Normal_Atk_Count;//총알 갯수
    
    GameObject[] Bomb = new GameObject[4];


    bool Bomb_On = true;
    void Throw_Bomb()
    {
        if (Bomb_On == true)
        {
            Check_Shot_bullet = false;

            for (int i = 0; i < 4; i++)
            {
                Bomb[i] = Instantiate(Bomb_Obj, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);//지뢰 넣어줘야함
                Bomb_Set(i);
            }

            Bomb_Cool = 30;


            Invoke("Avoid", 5f);//애니메이션 끝나는시간으로 바꾸고
            Bomb_On = false;
        }
    }


    
  
    void Bomb_Set(int a)//지뢰 날려주기
    {
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].localPosition * 3f,ForceMode2D.Impulse);
        //일단 속도가 0인 상태에서 지뢰를 뿌리고 일반공격 발사 할 때까지 속도 0으로 해놓고 몬스터 딜레이를 줌

    }


}
