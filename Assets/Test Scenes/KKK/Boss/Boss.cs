using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{



    public bool Boss_Active_on;//�÷��̾ ���� �濡 ������ ������ False 
                               //�÷��̾ �����濡 �����ϸ� True




    [Header("Prameter")]
    //public int Boss_Speed = 5;
    public float Bullet_Cool = 10;//�Ѿ� ��
    public float Bullet_Delay = 0.2f;
    public float Bomb_Cool = 30;//���� ��ġ ��Ÿ��
    public float Black_Fog_Cool = 0;//���� �Ȱ� ���� ��Ÿ�� // �ʱ� ���� 0 �̰� 30�ʷ� ����
    
    //var aa = GameManager.instance;

    public float Distance_To_Player = 50f;//��� ���� �Ÿ����� ���Ⲩ��?
    public float Dis;


    [Header("Boss_Info")]
    public int Boss_HP;
    public int Boss_Currnent_HP;


    int Stom_Count = 0;
    int Bomb_Throw_Count = 1;

    [Header("GameObject")]
    public GameObject Boss_Bullet;//���� �Ѿ�
    public GameObject Bomb_Obj;//����ź
    public GameObject Black_Fog;//���� �Ȱ�
    public GameObject Stom_Obj;//���� �ҿ뵹��
    public GameObject Big_Stom_Obj;
    public GameObject FireFly_Monster;




    [Header("Transform")]
    public Transform[] Bomb_transform = new Transform[4];//���� �����ִ� ��ġ
    public Transform Player_Transform;//�÷��̾� ��ġ
    public Transform Bullet_Transform;//�÷��̾� �ѱ� ��ġ
    public Transform Black_Fog_Transform;//�÷��̾� ���� �����Ȱ� ���� ��ġ
    public Transform Stom_Transform;//Frame ���� ��ġ // 0,0,0



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


            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//�÷��̾� ��ġ �޾ƿü� �ְ�

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis = Vector3.SqrMagnitude(Distance);//�÷��̾�� ���� �Ÿ� �޾ƿ�

            Cool_Control();//�� cool.....

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
    IEnumerator Respawn_Monster()//���� ������ ��Ÿ�� 10�ʷ� ��Ƴ�
    {

        Instantiate(FireFly_Monster, new Vector3(-50, 50, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(50, 50, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(-80, 25, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(80, 25, 0), Quaternion.identity);


        yield return new WaitForSeconds(25f);
        Boss_HP_Half = false;
    }
    

    void Trace_Player()//�÷��̾� ��ô
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
                        //������� �ִϸ��̼� Ʋ����
                        //Fade_In_and_Translate(Left_Transform); ��� �������� �ִϸ��̼� �߰� �Ϻ��ϰ� �Ⱥ������� ��������
                        //speed_back �κ�ũ�� Ʋ����
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


    void speed_back()//Invoke��
    {
        speed = 5;
    }

    void Cool_Control()//��Ÿ�� ��Ʈ��
    {
        Bullet_Cool -= Time.deltaTime;
        if (Bullet_Cool <= 0)
        {//�ִϸ��̼��� ���������� Ȯ��

            //Boss �Ѿ� �߻� �ִϸ��̼� ����


            speed = 0;//���� �������� ��� ���߰�

            //�����Լ� ����
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
    void Left()//�������� ��������!
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    void Right()//���������� ��������!
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
    }

    GameObject Black_Fog_Instance;
    void Ins_Bullet()//�⺻���� �Ѿ� ����
    {
        if (Normal_Atk_Count < 4)
        {
            GameObject B1 = Instantiate(Boss_Bullet, Bullet_Transform.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�

            Normal_Atk_Count++;
            Invoke("Ins_Bullet", Bullet_Delay);//1,2,3

        }
        else if (Normal_Atk_Count == 4)
        {
            //if()�ִϸ��̼� �������� Ȯ�� ��

            speed = 5;
            Normal_Atk_Count = 0;


        }
        Invoke("speed_back", 1f);
    }
    bool Fog_On = true;
    void Black_Fog_Ins()//�����Ȱ� ����
    {

        if (Fog_On == true)
        {

            Debug.Log("Fog");
            Check_Shot_bullet = false;
            //�ִϸ��̼� on
            Invoke("Fog", 0.5f);


            //�ִϸ��̼� ������
            //Avoid();
            Invoke("Avoid", 5f);//�ִϸ��̼� �����½ð����� �ٲٰ�
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
        Invoke("Translate_Boss", 5f);//�Ϻ��ϰ� ������� �ð����� �ٲٰ�
       
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
    void Stoming()//���� ����
    {
        if (Stom_Count == 0)
            if (Hp_Current <= Hp_Max * 0.75)
            {
                //���� �� �ι� �δ� �ִϸ��̼� Ʋ��
                speed = 0;

                frame = (GameObject)Instantiate(Stom_Obj,new Vector3(0,30,0), Quaternion.identity);


                frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);
                //Invoke(frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject),2f);

                Stom_Count++;

                //�ִϸ��̼� ������
                speed = 5;
            }
        if (Stom_Count == 1)
        {
            if(Hp_Current <= Hp_Max * 0.5)
            {
                //�ɿ����� �ٲ�� 
                Destroy(frame);
                Big_frame = (GameObject)Instantiate(Big_Stom_Obj, new Vector3(0, 30, 0), Quaternion.identity);

                Big_frame.GetComponent<Bullet_Attack>().target = Player_Transform.gameObject;
                Big_frame.GetComponent<Bullet_Attack>().CycleAttack(Player_Transform.gameObject);

                Stom_Count++;
            }
        }
    }
   
    int Normal_Atk_Count;//�Ѿ� ����
    
    GameObject[] Bomb = new GameObject[4];


    bool Bomb_On = true;
    void Throw_Bomb()
    {
        if (Bomb_On == true)
        {
            Check_Shot_bullet = false;

            for (int i = 0; i < 4; i++)
            {
                Bomb[i] = Instantiate(Bomb_Obj, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);//���� �־������
                Bomb_Set(i);
            }

            Bomb_Cool = 30;


            Invoke("Avoid", 5f);//�ִϸ��̼� �����½ð����� �ٲٰ�
            Bomb_On = false;
        }
    }


    
  
    void Bomb_Set(int a)//���� �����ֱ�
    {
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].localPosition * 3f,ForceMode2D.Impulse);
        //�ϴ� �ӵ��� 0�� ���¿��� ���ڸ� �Ѹ��� �Ϲݰ��� �߻� �� ������ �ӵ� 0���� �س��� ���� �����̸� ��

    }


}
