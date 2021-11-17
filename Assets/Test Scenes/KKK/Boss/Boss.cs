using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Character
{
    /*
    [SerializeField] private bool m_bDebugMode = false;

    [Header("View Config")]
    [Range(0f, 360f)]
    [SerializeField] private float m_horizontalViewAngle = 0f; //�þ߰��� ���� ����
    [SerializeField] private float m_viewRadius = 1f;  //Ž�� ����
    [Range(-180f, 180f)]
    [SerializeField] private float m_viewRotateZ = 0f; //ȸ�� ��

    [SerializeField] private LayerMask m_viewTargetMask; //Ÿ�ٷ��̾�
    [SerializeField] private LayerMask m_viewObstacleMask; //�þ߸� ���θ��� ������Ʈ ���̾�
    private List<Collider2D> hitedTargetContainer = new List<Collider2D>();

    private float m_horizontalViewHalfAngle = 0f;


    private void Awake()
    {
        m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;
    }

    private Vector3 AngleToDirZ(float angleInDegree)
    {
        float radian = (angleInDegree - transform.eulerAngles.z) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), Mathf.Cos(radian), 0f);
    }


    private void OnDrawGizmos()
    {
        if (m_bDebugMode)
        {
            m_horizontalViewHalfAngle = m_horizontalViewAngle * 0.5f;

            Vector3 originPos = transform.position;

            Gizmos.DrawWireSphere(originPos, m_viewRadius);

            Vector3 horizontalRightDir = AngleToDirZ(-m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 horizontalLeftDir = AngleToDirZ(m_horizontalViewHalfAngle + m_viewRotateZ);
            Vector3 lookDir = AngleToDirZ(m_viewRotateZ);

            Debug.DrawRay(originPos, horizontalLeftDir * m_viewRadius, Color.cyan);
            Debug.DrawRay(originPos, lookDir * m_viewRadius, Color.green);
            Debug.DrawRay(originPos, horizontalRightDir * m_viewRadius, Color.cyan);

            FindViewTargets();
        }
    }
    public Collider2D[] FindViewTargets() //����� �ν��ϴ� �ڵ�
                                          //����� �ν��� �� 3�ܰ��� Ȯ�� ������ ���ľ� �մϴ�.
                                          //1. ���� �ν� ���� �ȿ� ���� ����� �ִ°�?
                                          //2. �ν� ���� �ȿ� ���� ����� ���� �þ߰� �ȿ� �ִ°�?
                                          //3. �þ߰� �ȿ� ���� ����� �� �� ���� ���� ���� ���ع��� �����ϴ°�?
    {
        hitedTargetContainer.Clear();

        Vector2 originPos = transform.position;
        Collider2D[] hitedTargets = Physics2D.OverlapCircleAll(originPos, m_viewRadius, m_viewTargetMask); //���ȿ� ���� Ÿ���� ���� ���ϴ� Ÿ�ٸ� ���� ����

        foreach (Collider2D hitedTarget in hitedTargets)
        {
            Vector2 targetPos = hitedTarget.transform.position;
            Vector2 dir = (targetPos - originPos).normalized;
            Vector2 lookDir = AngleToDirZ(m_viewRotateZ);

            // float angle = Vector3.Angle(lookDir, dir)
            // �Ʒ� �� ���� ���� �ڵ�� �����ϰ� ������. ���� ������ ����
            float dot = Vector2.Dot(lookDir, dir);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= m_horizontalViewHalfAngle)
            {
                RaycastHit2D rayHitedTarget = Physics2D.Raycast(originPos, dir, m_viewRadius, m_viewObstacleMask); //����� ������ �ִ� ������Ʈ�� �ִ��� Ȯ���ϴ� ����ĳ��Ʈ
                if (rayHitedTarget)
                {
                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, rayHitedTarget.point, Color.yellow);
                }
                else
                {
                    hitedTargetContainer.Add(hitedTarget);

                    if (m_bDebugMode)
                        Debug.DrawLine(originPos, targetPos, Color.red);
                }
            }
        }

        if (hitedTargetContainer.Count > 0)
            return hitedTargetContainer.ToArray();
        else
            return null;
    }
    */
    public bool Boss_Active_on;//�÷��̾ ���� �濡 ������ ������ False 
                               //�÷��̾ �����濡 �����ϸ� True





    //var aa = GameManager.instance;
    float Attack_Cool = 1;
    public float Boss_Attack_Cooltime = 1;
    public float Bullet_Delay = 0.2f;


    public float Distance_To_Player = 50f;//��� ���� �Ÿ����� ���Ⲩ��?
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




    [Header("Transform")]
    public Transform[] Bomb_transform = new Transform[4];//���� �����ִ� ��ġ
    public Transform Player_Transform;//�÷��̾� ��ġ
    public Transform Bullet_Transform;//�÷��̾� �ѱ� ��ġ
    public Transform Black_Fog_Transform;//�÷��̾� ���� �����Ȱ� ���� ��ġ
    public Transform Stom_Transform;//Frame ���� ��ġ // 0,0,0



    RaycastHit2D hit;
    float MaxDistance = 15;
    [SerializeField]
    LayerMask m_viewTargetMask; //Ÿ�ٷ��̾�

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

            
            Player_Transform = GameObject.FindGameObjectWithTag("Player").transform;//�÷��̾� ��ġ �޾ƿü� �ְ�

            Vector3 Distance = Player_Transform.position - this.transform.position;
            Dis = Vector3.SqrMagnitude(Distance);//�÷��̾�� ���� �Ÿ� �޾ƿ�

            Cool_Control();//�� cool.....

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
            Debug.DrawRay(this.transform.position, transform.TransformDirection(Vector2.right) * 300f, Color.green);






            
         
            

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

        Instantiate(FireFly_Monster, new Vector3(-20, 10, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(20, 10, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(-30, 0, 0), Quaternion.identity);
        Instantiate(FireFly_Monster, new Vector3(30, 0, 0), Quaternion.identity);


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
            if (Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.left), 300f, m_viewTargetMask))
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
            if (Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), 300f, m_viewTargetMask))
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
    bool Boss_State_Check = true;//���𰡸� �ϴ����̸� false



    void Cool_Control()//��Ÿ�� ��Ʈ��
    {
        if(Boss_State_Check == true)
        {
            if(Attack_Cool > 0)
            {


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
                Invoke("Ins_Bullet", 0.3f);
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

    void Left()//�������� ��������!
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }
    void Right()//���������� ��������!
    {
        this.transform.localScale = new Vector3(-1, 1, 1);
    }



    void Ins_Bullet()//�⺻���� �Ѿ� ����
    {
        Boss_State_Check = false;
        if (Normal_Atk_Count < 4)
        {
            speed = 0;
            Instantiate(Boss_Bullet, Bullet_Transform.position, Quaternion.identity);//�Ѿ� ���� ��ġ ���� �ʿ�

            Normal_Atk_Count++;
            Invoke("Ins_Bullet", Bullet_Delay);//1,2,3

        }
        else if (Normal_Atk_Count == 4)
        {
            
            Normal_Atk_Count = 0;
            Invoke("anim_off", 0.3f);
            Invoke("speed_back", 0.3f);

        }
    }
    void Black_Fog_Ins()//�����Ȱ� ����
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
                Invoke("anim_off", 0.5f);

            }
      
    }
   
    int Normal_Atk_Count;//�Ѿ� ����
    
    GameObject[] Bomb = new GameObject[4];


    void Throw_Bomb()
    {
        Boss_State_Check = false;
        speed = 0;
        for (int i = 0; i < 3; i++)
            {
                Bomb[i] = Instantiate(Bomb_Obj, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);//���� �־������
                    Bomb_Set(i);
            }

        Invoke("speed_back", 0.3f);
        Invoke("anim_off", 0.3f);
    }

    
  
    void Bomb_Set(int a)//���� �����ֱ�
    {
        if(transform.localScale.x == 1)
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(Bomb_transform[a].localPosition * 1.5f,ForceMode2D.Impulse);
        else if(transform.localScale.x == -1)
        Bomb[a].GetComponent<Rigidbody2D>().AddForce(new Vector3(-Bomb_transform[a].localPosition.x, Bomb_transform[a].localPosition.y, Bomb_transform[a].localPosition.z) * 1.5f, ForceMode2D.Impulse);
        
        //�ϴ� �ӵ��� 0�� ���¿��� ���ڸ� �Ѹ��� �Ϲݰ��� �߻� �� ������ �ӵ� 0���� �س��� ���� �����̸� ��

    }



    void anim_off()
    {

        Boss_State_Check = true;

        Attack_Cool = Boss_Attack_Cooltime;
    }
    void speed_back()//Invoke��
    {
        speed = 5;
    }


}
