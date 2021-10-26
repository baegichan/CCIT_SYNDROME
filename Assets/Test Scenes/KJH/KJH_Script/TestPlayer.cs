using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [Header("���� ������Ʈ")]
    [Tooltip("������ ĳ���� ����Ʈ")]
    public GameObject[] Char;
    public GameObject SelectChar;
    [Tooltip("�ɷ� ���� �޾ƿ� �Ŵ���")]
    public GameObject abilityManager;
    //Animation ani;
    public static Rigidbody2D rigid;
    AbilityManager AM;
    Camera Cam;
    Animator Ani;

    [Header("�������ͽ�")]
    ///�÷��̾� 
    //�������ͽ�
    public int P_hp;
    public int AbilityHP;
    public int DefualtHP;
    public int P_Maxhp { get { return AbilityHP + DefualtHP; } }
    public bool P_OtherWorld = false;//2021.10.12 ������
    //
    //�̵�
    public static float h;
    public int P_M_Speed;
    public static bool RedBullDash = false;
    Vector2 Mouse;//2021.10.12 ������
    Vector2 PlayerPosition;//2021.10.12 ������
    //
    //����
    public float P_JumpForce;
    
    public float P_DefaultJumpInt = 1;
    public float P_MaxJumpInt
    {
        get
        {
            if (PassiveAbility.AbCode != 6) { return 1; }
            else { return P_DefaultJumpInt; }
        }
        set
        {
            P_DefaultJumpInt = value;
            if (PassiveAbility.AbCode == 6) { P_JumpInt = 2; }
            else { P_JumpInt = 1; }
        }
    }
    public float P_JumpInt;
    //

    [Header("���� ����&�ɷ�")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        Cam = Camera.main;//2021.10.12 ������
        AM = GetComponent<AbilityManager>();
        rigid = GetComponent<Rigidbody2D>();
        Ani = GetComponent<Animator>();
        SelectChar = Char[0];
        ChangeCahr();
    }

    void FixedUpdate()
    {
        Move();
        
    }

    void Update()
    {
        PlayerPosition = Cam.WorldToScreenPoint(SelectChar.transform.position);//���콺 ������ ��ǥ�ޱ�//2021.10.12 ������
        Mouse = Input.mousePosition;//2021.10.12 ������
        MouseFilp();//2021.10.12 ������

        if (ActiveAbility.AbSprite != null)
        {
            UseSkill();
        }
        UseItem();
        if (Input.GetKeyDown(KeyCode.Space)) { Jump();}
        if (Input.GetMouseButtonDown(0)) { atk(); }
        if (Input.GetKeyDown(KeyCode.Tab)) { ChangeCahr(); }
    }

    //ĳ���� ����
    void ChangeCahr()
    {
        //�ӽ� ĳ���� ��ü
        if (SelectChar == Char[0])
        {
            Char[1].SetActive(true);
            Char[1].transform.position = Char[0].transform.position;
            SelectChar = Char[1];
            Char[0].SetActive(false);
        }
        else if (SelectChar == Char[1])
        {
            Char[0].SetActive(true);
            Char[0].transform.position = Char[1].transform.position;
            SelectChar = Char[0];
            Char[1].SetActive(false);
        }
        rigid = SelectChar.GetComponent<Rigidbody2D>();
        Ani = SelectChar.GetComponent<Animator>();

        switch (SelectChar.transform.name)
        {
            case "MainCharacter_Eden":
                atk = SelectChar.GetComponent<MentalChaild>().Attack;
                break;
            case "Eden 10_18":
                atk = SelectChar.GetComponent<MentalChaild>().Attack;
                break;
        }
    }
    //

    //�̵�
    public void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);
        switch (h)
        {
            case 0:
                Ani.SetBool("Move", false);
                break;

            case -1: case 1:
                Ani.SetBool("Move", true);
                break;
        }

    }
    //

    //����
    public delegate void Attack();
    Attack atk;
    //

    //����
    public void Jump()
    {
        if (P_JumpInt == 0) { rigid.AddForce(Vector3.up * 0); }
        else if (P_JumpInt > 0)
        {
            Ani.SetBool("GroundState", false);
            rigid.AddForce(Vector3.up * P_JumpForce * 150 * Time.deltaTime);
            P_JumpInt -= 1;
        }
    }
    //���콺 �ø�
    public void MouseFilp()//2021.10.12 ������
    {

        if (Mouse.x <= PlayerPosition.x)// 1920x1080 ���� �߰�����
        {
            SelectChar.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Mouse.x > PlayerPosition.x)
        {
            SelectChar.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    //
    public void WorldChange()//�̸鼼�� ��ȯ //2021.10.12 ������
    {
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        GameObject B = GameObject.FindGameObjectWithTag("OtherPlayer");//�ӽ�
        if (P_OtherWorld == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = true;//�̸鼼�� ���� ���� �ʿ�            
                Instantiate(B, A.transform);
                Destroy(A, 0f);
            }
        }
        if (P_OtherWorld == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = false;//�⺻���� ���� ���� �ʿ�
                Instantiate(A, B.transform);
                Destroy(B, 0f);
            }
        }
    }
    //

    //�ɷ�
    public delegate void useAbility();
    useAbility active;

    public void SelectAbility()
    {
        switch (ActiveAbility.AbCode)
        {
            case 0:
                active = new useAbility(AM.Werewolf);
                break;
            case 1:
                active = new useAbility(AM.Parao);
                break;
            case 2:
                active = new useAbility(AM.BomberMan);
                break;
            case 3:
                active = new useAbility(AM.Ability_D);
                break;
            case 4:
                active = new useAbility(AM.Ability_E);
                break;
            case 5:
                active = new useAbility(AM.Ability_F);
                break;
            case 6:
                active = new useAbility(AM.Double_Jump);
                break;
        }
    }

    void UseSkill()
    {
        switch (ActiveAbility.AbName)
        {
            case "Werewolf":
                active();
                break;
            case "Parao":
                active();
                break;
            case "BomberMan":
                active();
                break;
        }
    }

    void UseItem()
    {
        if (MulYakInt > 0 && Input.GetKeyDown(KeyCode.E))
        {
            MulYakInt--;
            Debug.Log("���� ���");
        }
        else if (AlYakInt > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            AlYakInt--;
            Debug.Log("�˾� ���");
        }
    }
    //
}
