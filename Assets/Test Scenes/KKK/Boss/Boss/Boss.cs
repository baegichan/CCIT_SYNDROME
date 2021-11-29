using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    
    public bool Boss_Active_on;//�÷��̾ ���� �濡 ������ ������ False 
                               //�÷��̾ �����濡 �����ϸ� True


    public bool Abyss_on;
    //���� ��� ���¿����� �������� ����



    public bool Player_On_Ground = true;// 
    bool Boss_State_Check = true;//���𰡸� �ϴ����̸� false
    bool Boss_HP_Frame_Check;
    bool Boss_HP_Half;
    bool Barrier;

    Animator anim;


    [Header("Boss_Cool")]
    public float Attack_Cool = 1;
    public float Boss_Attack_Cooltime = 1;
    public float Bullet_Delay = 0.15f;
    public float Boss_Translate_Cooltime = 30;
    public float Barrier_Cool = 0;



    [Header("Distance_From_Boss_To_Player")]
    public float Distance_To_Player = 30f;//��� ���� �Ÿ����� ���Ⲩ��?
    public float Distance_To_Player2 = 70f;//�밢�� ����
    public float Dis;


    [Header("Boss_Info")]
    public int Boss_Shild;


    int Stom_Count = 0;

    [Header("GameObject")]
    public GameObject Boss_Bullet;//���� �Ѿ�
    public GameObject Bomb_Obj;//����ź
    public GameObject Abyss_Bomb_Obj;//�ɿ� ����ź
    public GameObject Black_Fog;//���� �Ȱ�
    public GameObject Stom_Obj;//���� �ҿ뵹��
    public GameObject Big_Stom_Obj;
    public GameObject FireFly_Monster;
    public GameObject Bomb_Transform;
    public GameObject Barrier_Hit;
    public GameObject Abyss_Fog;
    public GameObject Boss_Use_lns_Zone;



    GameObject Boss_Controll;
    

    public GameObject[] Boss_Particle = new GameObject[8];


    [Header("Transform")]
    public Transform Player_Transform;//�÷��̾� ��ġ
    public Transform Black_Fog_Transform;//�÷��̾� ���� �����Ȱ� ���� ��ġ



    RaycastHit2D hit;
    float MaxDistance = 15;

    [SerializeField]
    LayerMask m_viewTargetMask; //Ÿ�ٷ��̾�


    int aa = 0;int bb = 0;//��?��
    public int P_bossShield 
    { 
        get
        { 
            return Shield;
        }
        set 
        {
            if (Barrier)
            {
                if(aa == 0)
                {
                    bb = Shield;
                    aa++;
                }
                if (bb != Shield) 
                {
                    Instantiate(Barrier_Hit, transform.position, Quaternion.identity);
                    aa = 0;
                }
            }; 
        }
    }

    int Boss_CurrentHP;
    public int P_bossHP
    {
        get
        {
            return Hp_Current;
        }
        set
        {
            if (Hp_Current != Boss_CurrentHP)
            {
                Bs.Hp = Hp_Current;
                Bs.Shield = Shield;

                Boss_CurrentHP = Hp_Current;

            }
        }
    }

    int Shield_Value;


    public GameObject BossUI;
    BosStateUi Bs; 

    void Start()
    {
        IsBoss = true;
        
        anim = GetComponent<Animator>();

        Boss_CurrentHP = Hp_Current;

        if (Abyss_on == false)
        { 
            speed = 1;
        }
      
        
        
        Player_Transform = GameObject.FindGameObjectWithTag("Player").GetComponent<Char_Parent>().SelectChar.transform;

        

      


        Bs = BossUI.transform.GetComponent<BosStateUi>();
        if (Abyss_on == false)
        {
            Hp_Max = 500;
            Hp_Current = Hp_Max;
            BossUI.SetActive(true);
            Bs.MaxHp = Hp_Max;
            Bs.MaxShield = Shield;
        }
        else
            StartCoroutine(Respawn_Monster());//���� �� ����




        Shield_Value = Hp_Max / 5;
        Bs.MaxShield = Shield_Value;
        Boss_Controll = transform.parent.gameObject;
        
        
    }
    int Start_Count;
    bool Boss_Dead_Check = false;//Dead �ִϸ��̼� ���ӹ���
    void Update()
    {
        if (Boss_Active_on == true)
        {

            if (Start_Count == 0)
            {
                anim.SetBool("Move_ON", true);
                Start_Count++;
            }


            Dis = Vector3.Distance(Player_Transform.position, transform.position);

            Trace_Player();

            if (Abyss_on == false)
            {
                if (Frame_Count_Check == 0)
                    Monster_HP_Frame_Check();



                if (Half_Count_Check == 0)
                    Monster_HP_Check();
            }//�븻 �����϶� ���� HP Ȯ��


            //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.left) * 300f, Color.green);
            //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.TransformDirection(Vector2.right) * 300f, Color.green);





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
            }//�÷��̾ Ground���� ������ �ִϸ��̼� �߰��� �޸��� ������ �������� ���ϴ°���
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


            P_bossShield = Shield_Value;//���� ��ȣ�� ������ ��Ʈ ����Ʈ

            P_bossHP = Hp_Current;
            //Bs.Hp = Hp_Current;

            //Bs.Shield = Shield;


            if (Abyss_on == false)
            {
                if (Boss_HP_Frame_Check == true)
                {
                    speed = 0;
                    anim.SetBool("Syclone", true);
                }
                else
                {
                    Cool_Control();
                }



                if (Boss_HP_Half == true)
                {
                    speed = 0;
                    StopCoroutine(Respawn_Monster());//���� �� �� ��
                    if(Boss_Use_lns_Zone.transform.childCount != 0)
                    for(int a = 0; a <Boss_Use_lns_Zone.transform.childCount; a++)
                    {
                        Destroy(Boss_Use_lns_Zone.transform.GetChild(a).gameObject);
                    }
                    anim.SetBool("2PAGE", true);
                }



            }
            else
            {
                Cool_Control();
            }


            if (Barrier_Cool > 0)
            {
                Barrier_Cool -= Time.deltaTime;
            }



            //���� HP ���� ���ִ°� �ɿ��̶� �븻�̶�~~
            //transform.parent.gameObject.GetComponent<Boss_Info_Trans>().Translate_Boss_State();


            if (Barrier == true)
            {
                if(Shield <= 0)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                   
                }
            }

            
            if(Hp_Current <= 0)
            {
                if (Boss_Dead_Check == false)
                {
                    StopCoroutine(Respawn_Monster());//���� �� �� ��
                    anim_off();
                    anim.SetBool("Move_ON", false);
                    anim.SetBool("Dead", true);
                    //Boss_Dead_Check = true;
                    if (Boss_Use_lns_Zone.transform.childCount != 0)
                        for (int a = 0; a < Boss_Use_lns_Zone.transform.childCount; a++)
                        {
                            Destroy(Boss_Use_lns_Zone.transform.GetChild(a).gameObject);
                        }
                }
            }



        }
    }
    void Dead_END()
    {
        anim.SetBool("Dead", false);
        StopCoroutine(Respawn_Monster());//���� �� ����

       
        Destroy(Frame2);

    
    }
    public void Boss_End()
    {
        GameResultManager.result.CountKillBoss++;
        GameResultManager.result.ShowResult(true);
        
    }
    public void Change_Abyss_Boss()
    {
        MapChangeTester.AbyssMask.test.SetTrigger("Changed");
        AbyssManager.abyss.GoAbyss();
        Boss_Controll.transform.GetChild(1).gameObject.SetActive(true);


        //2PAGE�ִϸ��̼ǿ��� �� ��ü�� �ھ����ִ� �ִϸ��̼� �������� ������ ������� �������
        this.gameObject.SetActive(false);

    }



    int Frame_Count_Check;
    void Monster_HP_Frame_Check()
    {
        if (Hp_Current <= Hp_Max * 0.75f)
        {
            if (Boss_HP_Frame_Check == false)
            {
                Boss_HP_Frame_Check = true;
                StartCoroutine(Respawn_Monster());
            }

            if(Frame_Count_Check == 0)
            {
                Frame_Count_Check++;
            }
        }
    }



    int Half_Count_Check;
    void Monster_HP_Check()
    {
        if (Hp_Current <= Hp_Max * 0.5f)
        {
            if (Boss_HP_Half == false)
            {
                Boss_HP_Half = true;
                //StartCoroutine(Respawn_Monster());
            }

            if (Half_Count_Check == 0)
                Half_Count_Check++;

        }
    }






    IEnumerator Respawn_Monster()//���� ������ ��Ÿ�� 25�ʷ� ��Ƴ�
    {

        if (FireFly_Monster != null)
        {
            GameObject a = Instantiate(FireFly_Monster, new Vector3(5, 7, 0), Quaternion.identity);
            a.transform.SetParent(Boss_Use_lns_Zone.transform);
            GameObject b = Instantiate(FireFly_Monster, new Vector3(-5, 7, 0), Quaternion.identity);
            b.transform.SetParent(Boss_Use_lns_Zone.transform);
            GameObject c = Instantiate(FireFly_Monster, new Vector3(-7, 4, 0), Quaternion.identity);
            c.transform.SetParent(Boss_Use_lns_Zone.transform);
            GameObject d = Instantiate(FireFly_Monster, new Vector3(7, 4, 0), Quaternion.identity);
            d.transform.SetParent(Boss_Use_lns_Zone.transform);
        }

        yield return new WaitForSeconds(15f);
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
                        if (Boss_State_Check == false)
                        {
                            anim.SetBool("Check_Idle", false);
                        }

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
                        if (Boss_State_Check == false)
                        {
                            anim.SetBool("Check_Idle", false);
                        }
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
                        if (Boss_State_Check == false)
                        {
                            anim.SetBool("Check_Idle", false);
                        }
                    }
                    else if (Dis <= Distance_To_Player2)
                    {
                        speed = 0;
                        anim.SetBool("Check_Idle", true);

                    }
                }
                else if (Player_Transform.position.x > this.transform.position.x)
                {
                    Right();
                    if (Dis > Distance_To_Player2)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, new Vector3(Player_Transform.position.x, transform.position.y, transform.position.z), Time.deltaTime * speed);
                        if (Boss_State_Check == false)
                        {
                            anim.SetBool("Check_Idle", false);
                        }
                    }
                    else if (Dis <= Distance_To_Player2)
                    {
                        speed = 0;
                        anim.SetBool("Check_Idle", true);
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
                Attack_Cool -= Time.deltaTime;

                if(Attack_Cool <= 0)
                {
                    Select_Pattern();
                }
            }
            else
            {
                //Select_Pattern();
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
                //Invoke("Ins_Bullet", 0.4f);
            }
            else if(Dis <= Distance_To_Player)
            {
                if (Black_Fog_Ins_Count == 0)
                {
                    anim.SetBool("Stomping", true);
                    //Invoke("Black_Fog_Ins", 1f);
                }
                else if(Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime <= 0)
                {
                    anim.SetBool("Translate_Boss", true);
                }
                else if(Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime > 0)
                {
                    Black_Fog_Ins_Count = 0;
                    int aa = Random.Range(0, 3);
                    if (aa == 0)
                    {
                        anim.SetBool("Shot_Bullet", true);
                        //Invoke("Ins_Bullet", 0.4f);
                    }
                    else if(aa == 1)
                    {
                        anim.SetBool("Stomping", true);
                        //Invoke("Black_Fog_Ins", 1f);
                    }
                    else if(aa == 2)
                    {
                        if (Barrier_Cool <= 0)
                            if(Barrier != true) { speed = 0; anim.SetBool("Barrier_Check", true); } else { Select_Pattern(); }
                        else
                            Select_Pattern();

                    }
                }
            }
        }
        else
        {
            if (Dis > Distance_To_Player2)
            {
                anim.SetBool("Throw_Bomb", true);
                //Invoke("Throw_Bomb", 0.3f);
            }
            else if (Dis <= Distance_To_Player2)
            {
                if (Black_Fog_Ins_Count == 0)
                {
                    anim.SetBool("Stomping", true);
                    //Invoke("Black_Fog_Ins", 0.17f);
                }
                else if (Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime <= 0)
                {
                    Boss_Translate_Cooltime = 15;
                    Select_Pattern(); 
                }
                else if (Black_Fog_Ins_Count == 1 && Boss_Translate_Cooltime > 0)
                {
                    Black_Fog_Ins_Count = 0;
                    int aa = Random.Range(0, 3);
                    if (aa == 0)
                    {
                        anim.SetBool("Throw_Bomb", true);
                        //Invoke("Throw_Bomb", 0.17f);
                    }
                    else if(aa == 1)
                    {
                        anim.SetBool("Stomping", true);
                        //Invoke("Black_Fog_Ins", 1f);
                    }
                    else if(aa== 2)
                    {
                        if (Barrier_Cool <= 0)
                            if (Barrier != true) { speed = 0; anim.SetBool("Barrier_Check", true); } else { Select_Pattern(); }
                        else
                            Select_Pattern();
                    }
                }
            }
        }
    }

    void Translate_Boss()
    {
        speed = 0;

    }
    void Fade_In()//���� �̵��� ������� �����°�~
    {
        anim.SetBool("Translate_Boss", false);
        anim.SetBool("Check_Idle", false);

        if (Abyss_on == false)
        {
            if (transform.position.x >= 0)
            {
                if (Player_Transform.position.x > this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                transform.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < 0)
            {
                if (Player_Transform.position.x > this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (transform.position.x >= 0)
            {
                if (Player_Transform.position.x > this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                transform.position = new Vector3(transform.position.x - 8, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < 0)
            {
                if (Player_Transform.position.x > this.transform.position.x)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }
                transform.position = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z);
            }
        }
        

        anim.SetBool("Translate_Boss2", true);

        Boss_Translate_Cooltime = 15;
        Black_Fog_Ins_Count = 0;

        //Invoke("anim_off", 0.8f);
       
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

    GameObject[] Bullet_4 = new GameObject[5];

    void Ins_Bullet_1()
    {
        Boss_State_Check = false;
        Normal_Atk_Count = 0;
    }
    void Ins_Bullet_2()
    {
        speed = 0;
        Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
        if (transform.localScale.x > 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        else if (transform.localScale.x < 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        Normal_Atk_Count++;
    }
    void Ins_Bullet_3()
    {
        speed = 0;
        Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Left_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
        if (transform.localScale.x > 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        else if (transform.localScale.x < 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        Normal_Atk_Count++;
    }
    void Ins_Bullet_4()
    {
        speed = 0;
        Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Right_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
        if (transform.localScale.x > 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        else if (transform.localScale.x < 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        Normal_Atk_Count++;
    }

    void Ins_Bullet_5()
    {
        speed = 0;
        Bullet_4[Normal_Atk_Count] = Instantiate(Boss_Bullet, Right_Hand.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
        if (transform.localScale.x > 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().right = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        else if (transform.localScale.x < 0)
        {
            Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
            if (Abyss_on == true)
                Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
        }
        Normal_Atk_Count++;


        Normal_Atk_Count = 0;

        anim_off();
        anim.SetBool("Check_Idle", false);
        Invoke("speed_back", 0.8f);
    }

    void Ins_Bullet_End()
    {
        Normal_Atk_Count = 0;

        anim_off();
        anim.SetBool("Check_Idle", false);
        speed_back();
    }

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
                    if (Abyss_on == true)
                        Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
                }
                else if(transform.localScale.x < 0)
                {
                    Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().left = true;
                    if (Abyss_on == true)
                        Bullet_4[Normal_Atk_Count].GetComponent<BulletScript>().Abyss_Bullet_State = true;
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


        GameObject BF = Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);
        if (Abyss_on == true)
        BF.GetComponent<Black_Fog>().Abycss_Boss_Monster = true;

        Black_Fog_Ins_Count++;


        //anim_off();
        //anim.SetBool("Check_Idle", false);
        //Invoke("speed_back", 1);
    }



    GameObject frame;
    GameObject Big_frame;

    int Normal_Atk_Count;//�Ѿ� ����
    
    GameObject[] Bomb = new GameObject[4];


    void Throw_Bomb()
    {
        Boss_State_Check = false;
        speed = 0;
        for (int i = 0; i < 1; i++)
        {
            if (Abyss_on == false)
                Bomb[i] = Instantiate(Bomb_Obj, Bomb_Transform.transform.position, Quaternion.identity);
            else if(Abyss_on == true)
            {
                Bomb[i] = Instantiate(Abyss_Bomb_Obj, Bomb_Transform.transform.position, Quaternion.identity);
                //Bomb[i].GetComponent<Bomb>().Abycss_state = true;
            }

        }
        anim_off();
        Invoke("speed_back", 0.4f);
    }


    public GameObject Dark_Syclone_Obj;
    


    GameObject Frame;
    GameObject Frame2;

    void Dark_Syclone()
    {
        if (Abyss_on == false)
        {
            Instantiate(Dark_Syclone_Obj, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            Frame = Instantiate(Stom_Obj, new Vector3(20,6, 0), Quaternion.identity);
            Frame.name = "Frame";// ������ �� Find�����δٰ�
            Frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
            Frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);
        }
        else if(Abyss_on == true)
        {
            Instantiate(Dark_Syclone_Obj, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            Frame2 = Instantiate(Big_Stom_Obj, new Vector3(20,6, 0), Quaternion.identity);
            Frame2.name = "Frame2";// ������ �� Find�����δٰ�
            Frame2.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
            Frame2.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);
        }
        
    }
    public void Boss_HP_Frame_Check_False()//Animation Event��
    {
        Boss_HP_Frame_Check = false;
    }

    public void Barrier_Set()
    {
        Barrier = true;
        transform.GetChild(0).gameObject.SetActive(true);
        Shield = Shield_Value;

        Barrier_Cool = 20;
    }
    public void Abyss_Fog_Ins()
    {
        Instantiate(Abyss_Fog, transform.position, Quaternion.identity);
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
        anim.SetBool("Syclone", false);
        anim.SetBool("Stomping", false);
        anim.SetBool("Throw_Bomb", false);
        anim.SetBool("Translate_Boss", false);
        anim.SetBool("Translate_Boss2", false);
        anim.SetBool("Barrier_Check", false);

        }


    void speed_zero()
    {
        speed = 0;
        GameObject Boss = GameObject.Find("Boss_Controll").transform.GetChild(1).gameObject;
        if (Boss.GetComponent<Boss>().gameObject.activeSelf == true)
        { 
            //���ڰ� ������ ����������Ŵϱ� ���������
            GameObject Small = GameObject.Find("Frame");

            if(Small != null)
            Small.SetActive(false);


        }
    }
    void speed_back()//Invoke��
    {
        if (Abyss_on == false)
            speed = 1;
        else
            speed = 3;
        Boss_State_Check = true;
        Attack_Cool = Boss_Attack_Cooltime;

    }






}
