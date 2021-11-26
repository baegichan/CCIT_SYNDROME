using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Parent : Character
{
    [Header("게임 오브젝트")]
    [Tooltip("변신할 캐릭터 리스트")]
    public GameObject[] Char;
    public GameObject SelectChar;
    public Animator Ani;
    public static Rigidbody2D rigid;
    AbilityManager AM;
    public Camera Cam;
    public static bool ShopOn;

    [Header("플레이어 장비")]
    public GameObject Current_Use;
    public GameObject PharaoWand_Senaka;
    public GameObject PharaoWand;
    public GameObject BattleAxe_Senaka;
    public GameObject BattleAxe;
    public GameObject EvilSword;
    public List<Ability> AbilityHistory;

    [Header("플레이어 스테이터스")]
    public bool Dead;
    public int DefaultHP;
    public int CharHP;
    public int CharAP;
    public int CharDP;
    public int CharSpeed;
    //점프
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

    [Header("능력치 강화 수치")]
    public int Enhance_Health;
    public int Enhance_Strength;
    public int Enhance_Speed;
    public int[] Enhance_Health_Point = { 0, 1, 2, 3, 4, 5};
    public int[] Enhance_Strength_Point = { 0, 1, 2, 3, 4, 5 };
    public int[] Enhance_Speed_Point = { 0, 1, 2, 3, 4, 5 };

    [Header("소지 물약&능력")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        Before_Position = transform.position;
        Load_StateEnhance();
        AM = GetComponent<AbilityManager>();
        Cam = Camera.main;
        SelectChar = Char[0];
        ChangeChar(SelectChar);

        AM.CP = this;
    }
    void FixedUpdate()
    {
        if(!ShopOn && !Dead)
        {
            if (Ani.GetBool("CanIThis"))
            {
                if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
                Move();
            }
        }
    }
    void Update()
    {
        PlayerPosition = Cam.WorldToScreenPoint(SelectChar.transform.position);
        if (!Dead)
        {
            if (Input.GetKeyDown(KeyCode.O)) { Damage(20); } //테스트용
            Mouse = Input.mousePosition;
            if (!ShopOn)
            {
                if (Ani.GetBool("CanIThis"))
                {
                   // if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
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

            if (Input.GetKeyDown(KeyCode.P)) { PlayerPrefs.DeleteAll(); } //테스트용
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
            Before_Position = SelectChar.transform.position;
            //AbyssManager.abyss.Darkfog = Mathf.RoundToInt(AbyssManager.abyss.Darkfog * 0.9f);
            //PlayerPrefs.SetInt("DarkFog", AbyssManager.abyss.Darkfog);
            Dead = true;
            ChangeChar(SelectChar);
            Ani.SetTrigger("Die");

        }
    }

    //캐릭터 변경
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
                Char_Eden eden = SelectChar.GetComponent<Char_Eden>();
                atk = eden.Attack;
                ds = eden.Dash;
                CharHP = eden.HP;
                CharDP = eden.DP;
                StateManager.state.CharImgSelect(0);
                break;
            case "Wolf":
                Char_Wolf wolf = SelectChar.GetComponent<Char_Wolf>();
                atk = wolf.Attack;
                ds = wolf.Dash;
                CharHP = wolf.HP[ActiveAbility.Enhance];
                CharDP = wolf.DP;
                StateManager.state.CharImgSelect(1);
                break;
            case "RockHuman":
                Char_RockMan rock = SelectChar.GetComponent<Char_RockMan>();
                atk = rock.Attack;
                ds = rock.Dash;
                CharHP = rock.HP;
                CharDP = rock.DP[ActiveAbility.Enhance];
                break;
        }

        rigid = SelectChar.GetComponent<Rigidbody2D>();
        Ani = SelectChar.GetComponent<Animator>();
        Hp_Max = DefaultHP + CharHP + Enhance_Health_Point[Enhance_Health];
        if (!Dead) { Hp_Current = Hp_Max; }
        DP = CharDP;
        AP = CharAP + Enhance_Strength_Point[Enhance_Strength];
        speed = CharSpeed + Enhance_Speed_Point[Enhance_Speed];
        AM.py = SelectChar;
        switchItem(ActiveAbility.AbCode);
        UpdateState();
    }

    void UpdateState()
    {
        StateManager.state.MaxHp = Hp_Max;
        StateManager.state.Hp = Hp_Current;
    }

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

    //대쉬
    public delegate void dash();
    dash ds;
    //

    //공격
    public delegate void attack();
    attack atk;
    //

    //점프
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

    //마우스 플립

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

    //능력
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
            if(Hp_Current > Hp_Max) { Hp_Current = Hp_Max; }
            UpdateState();
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
        //AbyssManager.abyss.Darkfog = PlayerPrefs.HasKey("DarkFog") ? PlayerPrefs.GetInt("DarkFog") : 0;
    }

    public void Save_StateEnhance()
    {
        PlayerPrefs.SetInt("E_Health", Enhance_Health);
        PlayerPrefs.SetInt("E_Strength", Enhance_Strength);
        PlayerPrefs.SetInt("E_Speed", Enhance_Speed);
        //PlayerPrefs.SetInt("DarkFog", AbyssManager.abyss.Darkfog);
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
}
