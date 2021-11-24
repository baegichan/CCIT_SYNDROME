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
    public static bool ShopOn;

    [Header("�÷��̾� ���")]
    public GameObject Current_Use;
    public GameObject PharaoWand_Senaka;
    public GameObject PharaoWand;
    public GameObject BattleAxe_Senaka;
    public GameObject BattleAxe;
    public GameObject EvilSword;
    public List<Ability> AbilityHistory;

    [Header("�÷��̾� �������ͽ�")]
    public bool Dead;
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
    public bool UseApPostion;
    public static float h;
    public bool P_OtherWorld = false;
    public static bool RedBullDash = false;
    public static float Active_Cool_Max;
    public static float Active_Cool = 0f;
    Vector2 Mouse;
    Vector2 PlayerPosition;

    [Header("�ɷ�ġ ��ȭ ��ġ")]
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
        Load_StateEnhance();
        AM = GetComponent<AbilityManager>();
        Cam = Camera.main;
        SelectChar = Char[0];
        ChangeChar(SelectChar);

        AM.CP = this;
        Hp_Current = Hp_Max;
    }
    void FixedUpdate()
    {
        if(!ShopOn && !Dead)
        {
            if (Ani.GetBool("CanIThis"))
            {
                Move();
            }
        }
    }
    void Update()
    {
        if(!Dead)
        {
            if (Input.GetKeyDown(KeyCode.O)) { Damage(20); } //�׽�Ʈ��
            PlayerPosition = Cam.WorldToScreenPoint(SelectChar.transform.position);
            Mouse = Input.mousePosition;
            if (!ShopOn)
            {
                if (Ani.GetBool("CanIThis"))
                {
                    if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
                    UseItem();
                }
                ds();
                atk();
                if (ActiveAbility.AbSprite != null)
                {
                    UseSkill();
                }
            }
            if (Ani.GetBool("CanIThis"))
            {
                MouseFilp();
            }
            if (Active_Cool < Active_Cool_Max) { Active_Cool += Time.deltaTime; }

            if (Input.GetKeyDown(KeyCode.P)) { PlayerPrefs.DeleteAll(); } //�׽�Ʈ��
            if (AP_Timer > 0) { AP_Time(); }
            else { if (UseApPostion) { UseApPostion = false; } }
            Die();
        }
    }

    void Die()
    {
        if (Hp_Current <= 0 && !Dead)
        {
            SelectChar = Char[0];
            AbyssManager.abyss.Darkfog = Mathf.RoundToInt(AbyssManager.abyss.Darkfog * 0.9f);
            ChangeChar(SelectChar);
            Dead = true;
            Ani.SetTrigger("Die");
        }
    }

    //ĳ���� ����
    Vector3 Before_Position;

    public void DecideChar()
    {
        Before_Position = SelectChar.transform.position;

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
        Char.transform.position = Before_Position;
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
        switchItem(ActiveAbility.AbCode);
    }
    //

    //�̵�
    public void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        SelectChar.transform.position += new Vector3(h * speed * Time.deltaTime, 0);

        switch (h)
        {
            case 0:
                Ani.SetBool("Move", false);
                break;
            case -1:
            case 1:
                if (Ani.GetBool("CanIThis") == false)
                {
                    Ani.SetBool("Move", false);
                }
                else { Ani.SetBool("Move", true); }
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
            rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime, ForceMode2D.Impulse);
           // rigid.velocity = new Vector2(0, 0);
            P_JumpInt -= 1;
            Ani.SetBool("Jump", true);
        }
    }
    //

    //���콺 �ø�

    public void MouseFilp()
    {
        if (Ani.GetBool("CanIThis"))
        {
            if (Mouse.x <= PlayerPosition.x)
            {
                SelectChar.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (Mouse.x > PlayerPosition.x)
            {
                SelectChar.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    public void WorldChange()
    {
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        GameObject B = GameObject.FindGameObjectWithTag("OtherPlayer");
        if (P_OtherWorld == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = true;
                Instantiate(B, A.transform);
                Destroy(A, 0f);
            }
        }
        if (P_OtherWorld == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = false;
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

    public delegate void usePassive();
    public usePassive passive;

    public void UsePassive()
    {
        switch (PassiveAbility.AbCode)
        {
            case 6:
                passive = new usePassive(AM.Double_Jump);
                P_MaxJumpInt = 2;
                break;
        }
    }

    float AP_Duration = 120f;
    float AP_Timer;

    void UseItem()
    {
        if (MulYakInt > 0 && Input.GetKeyDown(KeyCode.E))
        {
            MulYakInt--;
            Hp_Current += 50;
        }
        else if (AlYakInt > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            AlYakInt--;
            AP_Timer = AP_Duration;
        }
    }

    void AP_Time()
    {
        if (!UseApPostion) { UseApPostion = true; }
        AP_Timer -= Time.deltaTime;
    }
    //

    public void switchItem(int AbilityCode)
    {
        if(Current_Use != null) { Current_Use.SetActive(false); }
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
                Active_Cool_Max = 0.5f;
                break;
            case 5:
                EvillSwordSwitch();
                Current_Use = EvilSword;
                AM.EA = Current_Use.GetComponent<Animator>();
                Active_Cool_Max = 5f; 
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

    void Load_StateEnhance()
    {
        Enhance_Health = PlayerPrefs.HasKey("E_Health") ? PlayerPrefs.GetInt("E_Health") : 0;
        Enhance_Strength = PlayerPrefs.HasKey("E_Strength") ? PlayerPrefs.GetInt("E_Strength") : 0;
        Enhance_Speed = PlayerPrefs.HasKey("E_Speed") ? PlayerPrefs.GetInt("E_Speed") : 0;
    }

    public void Save_StateEnhance()
    {
        PlayerPrefs.SetInt("E_Health", Enhance_Health);
        PlayerPrefs.SetInt("E_Strength", Enhance_Strength);
        PlayerPrefs.SetInt("E_Speed", Enhance_Speed);
    }

    public void SaveAbilityHistory(Ability ability)
    {
        if(AbilityHistory.Count == 0) { AbilityHistory.Add(ability); }
        else if(AbilityHistory.Count > 0)
        {
            for(int i = 0; i < AbilityHistory.Count; i++)
            {
                if(AbilityHistory[i].AbCode == ability.AbCode) { break; }
                else if(i == AbilityHistory.Count - 1 && AbilityHistory[i].AbCode != ability.AbCode) { AbilityHistory.Add(ability); }
            }
        }
    }
    GameObject CurrentCha;
    public void Special_Load_Damage_Text(int Damage)
    {
        CurrentCha = GetComponent<Char_Parent>().SelectChar;
        GameObject Text = (GameObject)Instantiate(Resources.Load("DamageObj"), CurrentCha.transform.position + Vector3.up * 3 + new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
        Text.GetComponent<DamageOBJ>().DamageText(Damage);
    }
    void Fail()
    {
        //GameResultManager.result.ShowResult(false);
    }
}
