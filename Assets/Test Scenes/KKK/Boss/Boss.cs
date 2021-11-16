using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{


    [Header("Boss  'ON'   ")]
    public bool Boss_Active_on;//�÷��̾ ���� �濡 ������ ������ False 
                               //�÷��̾ �����濡 �����ϸ� True


    [Header("Prameter")]
    public int Boss_Speed = 5;
    public float Bullet_Cool = 10;//�Ѿ� ��
    public float Bullet_Delay = 0.2f;
    public float Bomb_Cool = 30;//���� ��ġ ��Ÿ��


    public float Distance_To_Player = 150f;//��� ���� �Ÿ����� ���Ⲩ��?
    public float Dis;


    [Header("GameObject")]
    public GameObject Boss_Bullet;//���� �Ѿ�
    public GameObject Bomb_Obj;//����ź
    public GameObject Black_Fog;


    [Header("Transform")]
    public Transform[] Bomb_transform = new Transform[4];//���� �����ִ� ��ġ
    public Transform Player_Transform;//�÷��̾� ��ġ
    public Transform Bullet_Transform;//�÷��̾� �ѱ� ��ġ
    public Transform Black_Fog_Transform;//�÷��̾� ���� �����Ȱ� ���� ��ġ


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


            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//�÷��̾� ��ġ �޾ƿü� �ְ�

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis = Vector3.SqrMagnitude(Distance);//�÷��̾�� ���� �Ÿ� �޾ƿ�

            Cool_Control();//�� cool.....
            Trace_Player();


       




        }
    }

    void Trace_Player()//�÷��̾� ��ô
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
            //���������ŭ ��������� ����~~
            //����ź ������ ������ ������
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
    void Black_Fog_Ins()//�����Ȱ� ����
    {
        Black_Fog_Instance = Instantiate(Black_Fog, Black_Fog_Transform.position, Quaternion.identity);

        //Destroy
    }

    void Cool_Control()//��Ÿ�� ��Ʈ��
    {
        Bullet_Cool -= Time.deltaTime;
        if (Bullet_Cool <= 0)
        {//�ִϸ��̼��� ���������� Ȯ��

            //Boss �Ѿ� �߻� �ִϸ��̼� ����


            Boss_Speed = 0;//���� �������� ��� ���߰�

            //�����Լ� ����
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
    int Normal_Atk_Count;//�Ѿ� ����
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
                Bomb[i] = Instantiate(Bomb_Obj, transform.position, Quaternion.identity);//���� �־������
                Bomb_Set(i);
            }
            Bomb_Throw_Count++;
        }
    }


    
  
    void Bomb_Set(int a)//���� �����ֱ�
    {
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].position * 1f,ForceMode2D.Impulse);
        //�ϴ� �ӵ��� 0�� ���¿��� ���ڸ� �Ѹ��� �Ϲݰ��� �߻� �� ������ �ӵ� 0���� �س��� ���� �����̸� ��

    }


}
