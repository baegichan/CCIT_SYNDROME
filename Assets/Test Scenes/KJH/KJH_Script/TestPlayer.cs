using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : Character
{
    [Header("���� ������Ʈ")]
    [Tooltip("������ ĳ���� ����Ʈ")]
    public GameObject[] Char;
    public GameObject SelectChar;
    public Animator Ani;
    public static Rigidbody2D rigid;
    Abduru AM;
    Camera Cam;

    [Header("�÷��̾� ���")]
    public GameObject Current_Use;
    public GameObject PharaoWand_Senaka;
    public GameObject PharaoWand;
    public GameObject BattleAxe_Senaka;
    public GameObject BattleAxe;
    public GameObject EvilSword;

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
    public static float Active_Cool_Max;
    public static float Active_Cool = 0f;  
    Vector2 Mouse;//2021.10.12 ������
    Vector2 PlayerPosition;//2021.10.12 ������

    //[Header("�ɷ�ġ ��ȭ ��ġ")]
    public int Enhance_Health;
    public int Enhance_Strength;
    public int Enhance_Speed;
    public int[] Enhance_Health_Point = { 0, 1, 2, 3, 4, 5 };
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
        AM = GetComponent<Abduru>();
        Cam = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
        SelectChar = Char[0];
        ChangeChar(SelectChar);
        SelectAbility();
    }

    void FixedUpdate()
    {
        if (Ani.GetBool("CanIThis"))
        {
            if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
            Move();
        }
    }

    void Update()
    {
        PlayerPosition = Cam.WorldToScreenPoint(SelectChar.transform.position);//���콺 ������ ��ǥ�ޱ�//2021.10.12 ������
        Mouse = Input.mousePosition;//2021.10.12 ������
        MouseFilp();//2021.10.12 ������

        UseSkill();
        UseItem();
        ds();
        atk();
        //if (Ani.GetBool("CanIThis"))
        //{
        //    if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        //    Move();
        //}        
        if (Active_Cool < Active_Cool_Max) { Active_Cool += Time.deltaTime; }
        if (Input.GetKeyDown(KeyCode.O)) { DecideChar(); SelectAbility(); }
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
        for (int i = 0; i < this.Char.Length; i++)
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
                atk = SelectChar.GetComponent<MentalChaild>().Attack;
                ds = SelectChar.GetComponent<MentalChaild>().Dash;
                CharHP = SelectChar.GetComponent<MentalChaild>().HP;
                CharDP = SelectChar.GetComponent<MentalChaild>().DP;
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
        switchItem(ActiveAbility.AbCode);
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
            case -1: case 1:
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
        if (P_JumpInt == 0)
        {
            rigid.AddForce(Vector3.up * 0);         
        }
        else if (P_JumpInt > 0)
        {
            rigid.AddForce(Vector3.up * P_JumpForce * Time.deltaTime, ForceMode2D.Impulse);
            Debug.Log("������ 1����");
            //rigid.velocity = new Vector2(0, 0);
            P_JumpInt -= 1;
            Ani.SetBool("Jump", true);
        }
    }
    //

    //���콺 �ø�

    Vector3 CharScale;

    public void MouseFilp()//2021.10.12 ������
    {
        if (Ani.GetBool("CanIThis"))
        {
            if (Mouse.x <= PlayerPosition.x)
            {
                SelectChar.transform.localScale = new Vector3(-CharScale.x, CharScale.y, CharScale.z);
            }
            else if (Mouse.x > PlayerPosition.x)
            {
                SelectChar.transform.localScale = new Vector3(CharScale.x, CharScale.y, CharScale.z);
            }
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
                Ani.SetInteger("AbilityNum", ActiveAbility.AbCode);
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

    public void switchItem(int AbilityCode)
    {
        if (Current_Use != null) { Current_Use.SetActive(false); }
        switch (AbilityCode)
        {
            case 0:
                Active_Cool_Max = 4f;
                break;
            case 1:
                PharaoWandSwitch();
                Current_Use = PharaoWand_Senaka;
                Active_Cool_Max = 4f;
                break;
            case 2:
                Active_Cool_Max = 4f;
                break;
            case 3:
                Active_Cool_Max = 4f;
                break;
            case 4:
                OffBattleAxe();
                Current_Use = BattleAxe_Senaka;
                Active_Cool_Max = 4f;
                break;
            case 5:
                EvillSwordSwitch();
                Current_Use = EvilSword;
                AM.EA = Current_Use.GetComponent<Animator>();
                Active_Cool_Max = 4f; 
                break;
            case 9:
                Active_Cool_Max = 4f;
                break;
        }
        Active_Cool = Active_Cool_Max;
    }

    public void PharaoWandSwitch()
    {
        if (PharaoWand_Senaka.activeSelf) { PharaoWand_Senaka.SetActive(false); }
        else { PharaoWand_Senaka.SetActive(true); }
    }

    public void OffBattleAxe()
    {
        BattleAxe_Senaka.SetActive(true);
        BattleAxe.SetActive(false);
    }
    
    public void OnBattleAxe()
    {
        BattleAxe_Senaka.SetActive(false);
        BattleAxe.SetActive(true);
    }

    public void EvillSwordSwitch()
    {
        if (EvilSword.activeSelf) { EvilSword.SetActive(false); }
        else { EvilSword.SetActive(true); }
    }
}