using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Parent : Character
{
    [Header("���� ������Ʈ")]
    [Tooltip("������ ĳ���� ����Ʈ")]
    public GameObject[] Char;
    public GameObject SelectChar;
    public Animator Ani;
    public static Rigidbody2D rigid;
    AbilityManager AM;
    Camera Cam;

    [Header("�÷��̾� �������ͽ�")]
    public int DefaultHP;
    public int CharHP;
    public int CharAP;
    public int CharDP;
    public int CharSpeed;
    //����
    public float P_JumpForce;
    float P_DefaultJumpInt = 1;
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
    public static float h;
    public bool P_OtherWorld = false;
    public static bool RedBullDash = false;
    Vector2 Mouse;//2021.10.12 ������
    Vector2 PlayerPosition;//2021.10.12 ������

    //[Header("�ɷ�ġ ��ȭ ��ġ")]
    public int Enhance_Health;
    public int Enhance_Strength;
    public int Enhance_Speed;
    public int[] Enhance_Health_Point = { 0, 1, 2, 3, 4, 5};
    public int[] Enhance_Strength_Point = { 0, 1, 2, 3, 4, 5 };
    public int[] Enhance_Speed_Point = { 0, 1, 2, 3, 4, 5 };

    [Header("���� ����&�ɷ�")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        AM = GetComponent<AbilityManager>();
        Cam = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        SelectChar = Char[1];
        ChangeChar(SelectChar);
    }

    void FixedUpdate()
    {

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
        ds();
        if (Input.GetMouseButtonDown(0)) { atk(); }
        if(Ani.GetBool("CanIThis"))
        {
            if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
            Move();
        }
    }

    //ĳ���� ����
    public void DecideChar()
    {
        switch (ActiveAbility.AbCode)
        {
            case 0:
                SelectChar = Char[1];
                break;
            case 3:
                SelectChar = Char[2];
                break;
            default:
                SelectChar = Char[0];
                break;
        }
        ChangeChar(SelectChar);
    }

    void ChangeChar(GameObject Char)
    {
        for(int i = 0; i < this.Char.Length; i++)
        {
            this.Char[i].SetActive(false);
        }
        Char.transform.position = Char.transform.position;
        Char.SetActive(true);

        UpdateStat();
    }

    public void UpdateStat()
    {
        switch (SelectChar.transform.name)
        {
            case "Defualt":
                atk = SelectChar.GetComponent<Char_Eden>().Attack;
                ds = SelectChar.GetComponent<Char_Eden>().Dash;
                CharHP = SelectChar.GetComponent<Char_Eden>().HP;
                CharDP = SelectChar.GetComponent<Char_Eden>().DP;
                break;
            case "Wolf":
                atk = SelectChar.GetComponent<Char_Wolf>().Attack;
                ds = SelectChar.GetComponent<Char_Wolf>().Dash;
                CharHP = SelectChar.GetComponent<Char_Wolf>().HP[ActiveAbility.Enhance];
                CharDP = SelectChar.GetComponent<Char_Wolf>().DP;
                break;
            case "RockHuman":
                atk = SelectChar.GetComponent<Char_RockMan>().Attack;
                ds = SelectChar.GetComponent<Char_RockMan>().Dash;
                CharHP = SelectChar.GetComponent<Char_RockMan>().HP;
                CharDP = SelectChar.GetComponent<Char_RockMan>().DP[ActiveAbility.Enhance];
                break;
        }

        rigid = SelectChar.GetComponent<Rigidbody2D>();
        Ani = SelectChar.GetComponent<Animator>();
        Hp_Max = DefaultHP + CharHP + Enhance_Health_Point[Enhance_Health];
        DP = CharDP;
        AP = CharAP + Enhance_Strength_Point[Enhance_Strength];
        speed = CharSpeed + Enhance_Speed_Point[Enhance_Speed];
        AM.py = SelectChar;
        CharScale = SelectChar.transform.localScale;
    }
    //

    //�̵�
    public void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(h * speed * Time.deltaTime, 0);

        switch (h)
        {
            case 0:
                Ani.SetBool("Move", false);
                break;
            case -1:
            case 1:
                Ani.SetBool("Move", true);
                break;
        }
    }
    //

    //�뽬
    public delegate void dash();
    dash ds;
    //

    //����
    public delegate void attack();
    attack atk;
    //

    //����
    public void Jump()
    {
        if (P_JumpInt == 0) { rigid.AddForce(Vector3.up * 0); }
        else if (P_JumpInt > 0)
        {
            rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime);
            P_JumpInt -= 1;
            Ani.SetBool("Jump", true);
        }
    }
    //

    //���콺 �ø�

    Vector3 CharScale;

    public void MouseFilp()//2021.10.12 ������
    {
        if (Mouse.x <= PlayerPosition.x)// 1920x1080 ���� �߰�����
        {
            SelectChar.transform.localScale = new Vector3(-CharScale.x, CharScale.y, CharScale.z);
        }
        else if (Mouse.x > PlayerPosition.x)
        {
            SelectChar.transform.localScale = new Vector3(CharScale.x, CharScale.y, CharScale.z);
        }
    }

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
                active = new useAbility(AM.BattleAxe);
                break;
            case 5:
             active = new useAbility(AM.Ability_E);
                break;
            case 6:
                active = new useAbility(AM.Double_Jump);
                break;
            case 9:
                active = new useAbility(AM.BlackSmoke);
                break;
        }
    }

    void UseSkill()
    {
        switch (ActiveAbility.AbCode)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 9:
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
