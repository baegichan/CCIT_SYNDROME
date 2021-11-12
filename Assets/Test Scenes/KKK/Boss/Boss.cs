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



    public float Distance_To_Player = 150f;//��� ���� �Ÿ����� ���Ⲩ��?
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


            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//�÷��̾� ��ġ �޾ƿü� �ְ�

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis= Vector3.SqrMagnitude(Distance);//�÷��̾�� ���� �Ÿ� �޾ƿ�

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


    void Cool_Control()
    {
        Bullet_Cool -= Time.deltaTime;
        if(Bullet_Cool <= 0)
        {//�ִϸ��̼��� ���������� Ȯ��
            

            //�����Լ� ����
            GameObject BB = Instantiate(Boss_Bullet, this.transform.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�
            //BB.transform.position = Vector3.left * 50;
            Bullet_Cool = 10;
        }
    }


}
