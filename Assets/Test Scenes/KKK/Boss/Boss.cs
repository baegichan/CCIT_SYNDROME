using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    
    public bool Boss_Active_on;//플레이어가 보스 방에 들어오기 전까지 False 
                               //플레이어가 보스방에 입장하면 True


    Animation anim;


    //var aa = GameManager.instance;
    float Attack_Cool = 1;
    public float Boss_Attack_Cooltime = 1;
    public float Bullet_Delay = 0.1f;


    public float Distance_To_Player = 50f;//어느 정도 거리에서 멈출꺼노?
    public float Dis;


    [Header("Boss_Info")]
    public int Boss_HP;
    public int Boss_Currnent_HP;


    int Stom_Count = 0;

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



    RaycastHit2D hit;
    float MaxDistance = 15;
    [SerializeField]
    LayerMask m_viewTargetMask; //타겟레이어

    bool Boss_HP_Half;


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
            //Hp_Current -= 1;

            //Stoming();

            Monster_HP_Check();



            if (speed != 0)
            {
                if (Player_Transform.position.x < this.transform.position.x)
                {

                }
                else if (Player_Transform.position.x > this.transform.position.x)
                {

                  
                }
            }

            //Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.left) * 300f, Color.green);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.right) * 300f, Color.green);






            
         
            

            if (Stom_Count == 1)
            {
                if (Hp_Current <= Hp_Max * 0.5)
                {
                    //심연으로 바뀌고 
                    Destroy(frame);
                    Big_frame = (GameObject)Instantiate(Big_Stom_Obj, new Vector3(0, 2, 0), Quaternion.identity);

                    Big_frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                    Big_frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                    Stom_Count++;
                }
            }



            /*
            if (Input.GetMouseButtonDown(0))
            {
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
    IEnumerator Respawn_Monster()//몬스터 리스폰 쿨타임 25초로 잡아놈
    {

        Instantiate(FireFly_Monster, new Vector3(-20, 10, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(20, 10, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(-30, 0, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(30, 0, 0), Quaternion.identity);


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
                }
                else if (Dis <= Distance_To_Player)
                {
                    speed = 0;
                }
            }
            else if (Player_Transform.position.x > this.transform.position.x)
            {
                
                if (Dis > Distance_To_Player)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    Right();

                }
                else if (Dis <= Distance_To_Player)
                {
                    speed = 0;

                }
            }
        }


        if (Player_Transform.position.x < this.transform.position.x)
        {
            if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.left), 300f, m_viewTargetMask))
            {
                Player_On_Ground = true;
            }
            else
            {
                Player_On_Ground = false;
            }
        }
        else if (Player_Transform.position.x > this.transform.position.x)
        {
            if (Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.right), 300f, m_viewTargetMask))
            {
                Player_On_Ground = true;
            }
            else
            {
                Player_On_Ground = false;
            }
        }

    }

    public bool Player_On_Ground = true;// 
    bool Boss_State_Check = true;//무언가를 하는중이면 false



    void Cool_Control()//쿨타임 컨트롤
    {
        if(Boss_State_Check == true)
        {
            if(Attack_Cool > 0)
            {


                if (Stom_Count == 0)
                    if (Hp_Current <= Hp_Max * 0.75)
                    {
                        //위로 총 두번 두는 애니메이션 틀고
                        speed = 0;

                        frame = (GameObject)Instantiate(Stom_Obj, new Vector3(0, 2, 0), Quaternion.identity);


                        frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                        frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                        Stom_Count++;

                        Invoke("speed_back", 0.5f);

                    }






                Attack_Cool -= Time.deltaTime;


                if(Attack_Cool <= 0)
                {
                    Select_Pattern();
                }
            }
        }
    }

    void Select_Pattern()
    {
        if(Player_On_Ground == true)
        {
            if(Dis > Distance_To_Player)
            {
                Invoke("Ins_Bullet", 0.15f);
            }
            else if(Dis <= Distance_To_Player)
            {
                Invoke("Black_Fog_Ins", 0.3f);
            }
        }
        else
        {
            if (Dis > Distance_To_Player)
            {
                Invoke("Throw_Bomb", 0.3f);
            }
            else if (Dis <= Distance_To_Player)
            {
                Invoke("Black_Fog_Ins", 0.3f);
            }
        }
    }

    void Left()//왼쪽으로 돌려이잇!
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
    }
    void Right()//오른쪽으로 돌려이잇!
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }


    public Transform Left_Hand;
    public Transform Right_Hand;
    void Ins_Bullet()//기본공격 총알 생성
    {

        Boss_State_Check = false;
        if (Normal_Atk_Count < 4)
        {
            speed = 0;
            if(Normal_Atk_Count == 0 || Normal_Atk_Count == 1)
            Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//총알 생성 위치 조정 필요
            else if(Normal_Atk_Count == 2 || Normal_Atk_Count == 3)
            Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//총알 생성 위치 조정 필요


            Normal_Atk_Count++;
            Invoke("Ins_Bullet", Bullet_Delay);//1,2,3

        }
        else if (Normal_Atk_Count == 4)
        {
            
            Normal_Atk_Count = 0;
            Invoke("anim_off", 0.2f);
            Invoke("speed_back", 0.2f);

        }
    }
    void Black_Fog_Ins()//검은안개 생성
    {
        Boss_State_Check = false;
        speed = 0;

        Invoke("Fog", 0.5f);

        Invoke("speed_back", 3);
        Invoke("anim_off", 3);
    }
    void Fog()
    {
        Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);
    }




    GameObject frame;
    GameObject Big_frame;
    void Stoming()//액자 생성
    {
        if(Stom_Count == 0)
            if (Hp_Current <= Hp_Max * 0.75)
            {
                //위로 총 두번 두는 애니메이션 틀고
                speed = 0;

                frame = (GameObject)Instantiate(Stom_Obj,new Vector3(0,2,0), Quaternion.identity);


                frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                Stom_Count++;

                Invoke("speed_back", 0.5f);
                Invoke("anim_off", 0.5f);

            }
      
    }
   
    int Normal_Atk_Count;//총알 갯수
    
    GameObject[] Bomb = new GameObject[4];


    void Throw_Bomb()
    {
        Boss_State_Check = false;
        speed = 0;
        for (int i = 0; i < 3; i++)
            {
                Bomb[i] = Instantiate(Bomb_Obj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);//지뢰 넣어줘야함
                    Bomb_Set(i);
            }

        Invoke("speed_back", 0.3f);
        Invoke("anim_off", 0.3f);
    }

    
  
    void Bomb_Set(int a)//지뢰 날려주기
    {
        if(transform.localScale.x > 0)
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].localPosition * 1.5f,ForceMode2D.Impulse);
        else if(transform.localScale.x < 0)
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(new Vector3(-Bomb_transform[a].localPosition.x, Bomb_transform[a].localPosition.y, Bomb_transform[a].localPosition.z) * 1.5f, ForceMode2D.Impulse);
        
        //일단 속도가 0인 상태에서 지뢰를 뿌리고 일반공격 발사 할 때까지 속도 0으로 해놓고 몬스터 딜레이를 줌

    }



    void anim_off()
    {

        Boss_State_Check = true;

        Attack_Cool = Boss_Attack_Cooltime;
    }
    void speed_back()//Invoke용
    {
        speed = 5;
    }


}
