using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    
    public bool Boss_Active_on;//�÷��̾ ���� �濡 ������ ������ False 
                               //�÷��̾ �����濡 �����ϸ� True


    public bool Player_On_Ground = true;// 
    bool Boss_State_Check = true;//���𰡸� �ϴ����̸� false
    bool Boss_HP_Half;


    Animator anim;


    [Header("Boss_Cool")]
    float Attack_Cool = 1;
    public float Boss_Attack_Cooltime = 1;
    public float Bullet_Delay = 0.15f;
    public float Boss_Translate_Cooltime = 30;



    [Header("Distance_From_Boss_To_Player")]
    public float Distance_To_Player = 30f;//��� ���� �Ÿ����� ���Ⲩ��?
    public float Distance_To_Player2 = 70f;//�밢�� ����
    public float Dis;


    [Header("Boss_Info")]
    public int Boss_HP;
    public int Boss_Currnent_HP;


    int Stom_Count = 0;

    [Header("GameObject")]
    public GameObject Boss_Bullet;//���� �Ѿ�
    public GameObject Bomb_Obj;//����ź
    public GameObject Black_Fog;//���� �Ȱ�
    public GameObject Stom_Obj;//���� �ҿ뵹��
    public GameObject Big_Stom_Obj;
    public GameObject FireFly_Monster;
    public GameObject Bomb_Transform;


    public GameObject[] Boss_Body = new GameObject[15];
    public GameObject[] Boss_Particle = new GameObject[8];


    [Header("Transform")]
    //public Transform[] Bomb_Transform = new Transform[4];//���� �����ִ� ��ġ
    public Transform Player_Transform;//�÷��̾� ��ġ
    //public Transform Bullet_Transform;//�÷��̾� �ѱ� ��ġ
    public Transform Black_Fog_Transform;//�÷��̾� ���� �����Ȱ� ���� ��ġ



    RaycastHit2D hit;
    float MaxDistance = 15;
    [SerializeField]
    LayerMask m_viewTargetMask; //Ÿ�ٷ��̾�


    void Start()
    {
        //Direction_Player();
        //BM = GameManager.instance;
        anim = GetComponent<Animator>();
        speed = 1;
        Hp_Max = Boss_HP;
        Hp_Current = Boss_Currnent_HP;

        Player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<TestPlayer>().SelectChar.transform;//�÷��̾� ��ġ �޾ƿü� �ְ�






        //sunset = Player.GetComponent<TestPlayer>().SelectChar.transform;//�÷��̾� ��ġ �޾ƿü� �ְ�











    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        if (Boss_Active_on == true)
        {

            


            //Vector2 Distance = Player_Transform.position - this.transform.position;
            //Debug.Log(Player_Transform.position);
            //Dis = Vector2.SqrMagnitude(Distance);//�÷��̾�� ���� �Ÿ� �޾ƿ�
            Dis = Vector3.Distance(Player_Transform.position , transform.position);

            Cool_Control();//�� cool.....

            Trace_Player();

            Monster_HP_Check();



            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.left) * 300f, Color.green);
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.right) * 300f, Color.green);






            
         
            

            if (Stom_Count == 1)
            {
                if (Hp_Current <= Hp_Max * 0.5)
                {
                    //�ɿ����� �ٲ�� 
                    Destroy(frame);
                    Big_frame = (GameObject)Instantiate(Big_Stom_Obj, new Vector3(0, 2, 0), Quaternion.identity);

                    Big_frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                    Big_frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                    Stom_Count++;
                }
            }

            /*
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Boss_Move"))//���� �ȴ� �ִϸ��̼��� ���ư��� ��Ȳ�̾�� �Ҷ�
            {
                if(speed == 0)
                {
                    speed = 0;
                    anim.SetBool("Check_Idle", true);
                }
                else
                {
                    anim_off();
                }
            }
            */
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
    IEnumerator Respawn_Monster()//���� ������ ��Ÿ�� 25�ʷ� ��Ƴ�
    {

        if (FireFly_Monster != null)
        {
            Instantiate(FireFly_Monster, new Vector3(-20, 10, 0), Quaternion.identity);
            Instantiate(FireFly_Monster, new Vector3(20, 10, 0), Quaternion.identity);
            Instantiate(FireFly_Monster, new Vector3(-30, 0, 0), Quaternion.identity);
            Instantiate(FireFly_Monster, new Vector3(30, 0, 0), Quaternion.identity);
        }

        yield return new WaitForSeconds(25f);
        Boss_HP_Half = false;
    }
    

    void Trace_Player()//�÷��̾� ��ô
    {
        if (speed != 0)
        {
            if (Player_On_Ground == true)
            {
                if (Player_Transform.position.x < this.transform.position.x)
                {
                    Left();

                    if (Dis > Distance_To_Player)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    }
                    else if (Dis <= Distance_To_Player)
                    {
                        speed = 0;
                    }
                }
                else if (Player_Transform.position.x > this.transform.position.x)
                {
                    Right();

                    if (Dis > Distance_To_Player)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    }
                    else if (Dis <= Distance_To_Player)
                    {
                        speed = 0;

                    }
                }
            }
            else
            {
                if (Player_Transform.position.x < this.transform.position.x)
                {
                    Left();
                    if (Dis > Distance_To_Player2)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                    }
                    else if (Dis <= Distance_To_Player2)
                    {
                        speed = 0;
                    }
                }
                else if (Player_Transform.position.x > this.transform.position.x)
                {
                    Right();
                    if (Dis > Distance_To_Player2)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                        Right();

                    }
                    else if (Dis <= Distance_To_Player2)
                    {
                        speed = 0;

                    }
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




    void Cool_Control()//��Ÿ�� ��Ʈ��
    {
        if(Boss_State_Check == true)
        {
            if(Attack_Cool > 0)
            {
                /*
                if (Stom_Count == 0)
                    if (Hp_Current <= Hp_Max * 0.75)
                    {
                        //���� �� �ι� �δ� �ִϸ��̼� Ʋ��
                        speed = 0;

                        frame = (GameObject)Instantiate(Stom_Obj, new Vector3(0, 2, 0), Quaternion.identity);


                        frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                        frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                        Stom_Count++;

                        Invoke("speed_back", 0.5f);

                    }
                */
                /*
                if(speed == 0)
                anim.SetBool("Check_Idle", true);
                */



                Attack_Cool -= Time.deltaTime;


                if(Attack_Cool <= 0)
                {
                    if (Player_On_Ground == true)
                    {
                        if (Dis > Distance_To_Player)
                        {
                            //anim.SetBool("Check_Idle", false);
                            anim.SetBool("Distance_Check", false);
                        }
                        else if (Dis <= Distance_To_Player)
                        {
                            anim.SetBool("Distance_Check", true);
                        }
                    }
                    else
                    {
                        if (Dis > Distance_To_Player2)
                        {
                            //anim.SetBool("Check_Idle", false);
                            anim.SetBool("Distance_Check", false);
                        }
                        else if (Dis <= Distance_To_Player2)
                        {
                            anim.SetBool("Distance_Check", true);
                        }

                    }
                    

                    Select_Pattern();
                }
            }
        }
        



        if(Boss_Translate_Cooltime > 0)
        {
            Boss_Translate_Cooltime -= Time.deltaTime;
        }
    }

    void Select_Pattern()
    {
        if(Player_On_Ground == true)
        {
            if(Dis > Distance_To_Player)
            {
                anim.SetBool("Shot_Bullet", true);
                Invoke("Ins_Bullet", 0.4f);
            }
            else if(Dis <= Distance_To_Player)
            {
                if (Black_Fog_Ins_Count == 0)
                {
                    anim.SetBool("Stomping", true);
                    Invoke("Black_Fog_Ins", 1f);
                }
                else if(Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime <= 0)
                {
                    Translate_Boss();
                }
                else if(Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime > 0)
                {
                    Black_Fog_Ins_Count = 0;
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        anim.SetBool("Shot_Bullet", true);
                        Invoke("Ins_Bullet", 0.4f);
                    }
                    else
                    {
                        anim.SetBool("Stomping", true);
                        Invoke("Black_Fog_Ins", 1f);
                    }
                }
            }
        }
        else
        {
            if (Dis > Distance_To_Player2)
            {
                anim.SetBool("Throw_Bomb", true);
                Invoke("Throw_Bomb", 0.3f);
            }
            else if (Dis <= Distance_To_Player2)
            {
                if (Black_Fog_Ins_Count == 0)
                {
                    anim.SetBool("Stomping", true);
                    Invoke("Black_Fog_Ins", 0.17f);
                }
                else if (Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime <= 0)
                {
                    Boss_Translate_Cooltime = 15;
                    Select_Pattern(); 
                }
                else if (Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime > 0)
                {
                    Black_Fog_Ins_Count = 0;
                    int aa = Random.Range(0, 2);
                    if (aa == 0)
                    {
                        anim.SetBool("Throw_Bomb", true);
                        Invoke("Throw_Bomb", 0.17f);
                    }
                    else
                    {
                        anim.SetBool("Stomping", true);
                        Invoke("Black_Fog_Ins", 1f);
                    }
                }
            }
        }
    }

    void Translate_Boss()
    {
        anim.SetBool("Translate_Boss", true);
        speed = 0;

        Invoke("Fade_In", 1f);

    }
    void Fade_In()//���� �̵��� ������� �����°�~
    {
        if (transform.position.x >= 0)
        {
            transform.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < 0)
        {
            transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
        }

        anim_off();

        anim.SetBool("Translate_Boss2", true);

        Boss_Translate_Cooltime = 15;
        Black_Fog_Ins_Count = 0;

        Invoke("anim_off", 0.5f);
       
        //anim_off();
        Invoke("speed_back", 1f);
    }


    void Set_Boss_Translate_Fog_lns()
    {
        GameObject aa = Instantiate(Boss_Particle[7], new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), Quaternion.identity);
        Destroy(aa, 3f);
    }

    void Left()//�������� ��������!
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
    }
    void Right()//���������� ��������!
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }


    public Transform Left_Hand;
    public Transform Right_Hand;

    GameObject[] Bullet_4 = new GameObject[4];

    void Ins_Bullet()//�⺻���� �Ѿ� ����
    {
        
        Boss_State_Check = false;
        if (Normal_Atk_Count < 4)
        {
            speed = 0;
            if (Normal_Atk_Count == 0 || Normal_Atk_Count == 1)
            {
                Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
                if(transform.localScale.x > 0)
                {
                    Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
                }
                else if(transform.localScale.x < 0)
                {
                    Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
                }
            }
            else if (Normal_Atk_Count == 2 || Normal_Atk_Count == 3)
            {
                Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
                if (transform.localScale.x > 0)
                {
                    Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
                }
                else if (transform.localScale.x < 0)
                {
                    Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
                }
            }


            Normal_Atk_Count++;
            Invoke("Ins_Bullet", Bullet_Delay);//1,2,3

        }
        else if (Normal_Atk_Count == 4)
        {
            
            Normal_Atk_Count = 0;

            anim_off();
            anim.SetBool("Check_Idle", false);
            Invoke("speed_back", 0.8f);

        }
    }


    int Black_Fog_Ins_Count = 0;
    void Black_Fog_Ins()//�����Ȱ� ����
    {
        Boss_State_Check = false;
        speed = 0;


        Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);

        Black_Fog_Ins_Count++;


        anim_off();
        //anim.SetBool("Check_Idle", false);
        Invoke("speed_back", 1);
    }



    GameObject frame;
    GameObject Big_frame;
    void Stoming()//���� ����
    {
        if(Stom_Count == 0)
            if (Hp_Current <= Hp_Max * 0.75)
            {
                //���� �� �ι� �δ� �ִϸ��̼� Ʋ��
                speed = 0;

                frame = (GameObject)Instantiate(Stom_Obj,new Vector3(0,2,0), Quaternion.identity);


                frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                Stom_Count++;




                Invoke("speed_back", 0.5f);

            }
      
    }
   
    int Normal_Atk_Count;//�Ѿ� ����
    
    GameObject[] Bomb = new GameObject[4];


    void Throw_Bomb()
    {
        Boss_State_Check = false;
        speed = 0;
        for(int i = 0;i < 1; i++)
        {
            Bomb[i] = Instantiate(Bomb_Obj, Bomb_Transform.transform.position, Quaternion.identity);
            //Bomb[i] = Instantiate(Bomb_Obj, Bomb_Transform.transform.localPosition, Quaternion.identity);

        }

        //anim.SetBool("Throw_Bomb", true);
        //Invoke("Throw_Bomb", 0.3f);
        anim_off();
        Invoke("speed_back", 0.4f);
    }



    void Bomb_Set(int a)//���� �����ֱ�
    {
        Debug.Log(23);
        //Bomb[a].transform.position = Vector3.Slerp(transform.position, Player_Transform.position, 3f);


        //�ϴ� �ӵ��� 0�� ���¿��� ���ڸ� �Ѹ��� �Ϲݰ��� �߻� �� ������ �ӵ� 0���� �س��� ���� �����̸� ��



        //Bomb_Target
    }



    void anim_off()
    {
        if (Player_On_Ground == true)
        {
            if (Dis <= Distance_To_Player)
            {
                anim.SetBool("Check_Idle", true);
                anim.SetBool("Distance_Check", true);
            }
            else
            {
                anim.SetBool("Check_Idle", false);
                anim.SetBool("Distance_Check", false);
            }
        }
        else if(Player_On_Ground == false)
        {
            if (Dis <= Distance_To_Player2)
            {
                anim.SetBool("Check_Idle", true);
                anim.SetBool("Distance_Check", true);
            }
            else
            {
                anim.SetBool("Check_Idle", false);
                anim.SetBool("Distance_Check", false);
            }
        }
        anim.SetBool("Shot_Bullet", false);
            anim.SetBool("Stomping", false);
            anim.SetBool("Throw_Bomb", false);
            anim.SetBool("Translate_Boss", false);
            anim.SetBool("Translate_Boss2", false);


        }
    void speed_back()//Invoke��
    {
        speed = 1;
        Boss_State_Check = true;
        Attack_Cool = Boss_Attack_Cooltime;

    }


}
