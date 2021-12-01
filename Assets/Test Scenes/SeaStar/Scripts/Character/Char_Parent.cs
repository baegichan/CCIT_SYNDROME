using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public float Default_JumpForce;
    public float P_JumpForce
    {
        get
        {
            if (PassiveAbility.AbCode != 6) { return Default_JumpForce; }
            else { return Default_JumpForce + Default_JumpForce * (AM.DoubleJumpP[PassiveAbility.Enhance] * 0.01f); }
        }
    }
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
    public int[] Enhance_Health_Point = { 0, 10, 20, 30, 40, 50};
    public int[] Enhance_Strength_Point = { 0, 5, 10, 15, 20, 25 };
    public int[] Enhance_Speed_Point = { 0, 5, 10, 15, 20, 25 };

    [Header("스킬 쿨타임")]
    public int WereWolfCool;
    public int PharaoCool;
    public int EvilSworldCool;
    public int BattleAxeCool;

    [Header("소지 물약&능력")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        AbyssManager.abyss.Darkfog = 2000;
        Before_Position = transform.position;
        Load_StateEnhance();
        AM = GetComponent<AbilityManager>();
        Cam = Camera.main;
        SelectChar = Char[0];
        ChangeChar(SelectChar);
        Hp_Current = Hp_Max;

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
            GroundCheck();
            if (AbyssManager.abyss.isHp)
            {
                Hp_Current -= 5;
                StateManager.state.Hp = Hp_Current;
                AbyssManager.abyss.isHp = false;
                AbyssManager.abyss.HpGage = Hp_Current;
            }
            Mouse = Input.mousePosition;
            if (!ShopOn)
            {
                if (Ani.GetBool("CanIThis"))
                {
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
            if (AP_Timer > 0) { AP_Time(); }
            else { if (UseApPostion) { UseApPostion = false; } }
            Die();
        }
    }

    void Die()
    {
        if (Hp_Current <= 0 && !Dead)
        {
            Before_Position = SelectChar.transform.position;
            SelectChar = Char[0];
            Dead = true;
            ChangeChar(SelectChar);
            SelectChar.transform.position = Before_Position;
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
        vel = rigid.velocity;
        Ani = SelectChar.GetComponent<Animator>();
        Hp_Max = DefaultHP + CharHP + Enhance_Health_Point[Enhance_Health];
        //if(!Dead) { Hp_Current = Hp_Max; }
        DP = CharDP;
        AP = CharAP + Enhance_Strength_Point[Enhance_Strength];
        speed = CharSpeed + Enhance_Speed_Point[Enhance_Speed];
        AM.py = SelectChar;
        //if (ActiveAbility != null) { ResourceManager.re.ActiveAbility = ActiveAbility; }
        //if (PassiveAbility != null) { ResourceManager.re.PassiveAbility = PassiveAbility; }
        AbilityCheat();
        switchItem(ActiveAbility.AbCode);
        UpdateState();
    }

    void AbilityCheat()
    {
        SelectAbility();
        PlayerSkillUI.skill.Image_Active.sprite = ActiveAbility.icon;
        PlayerSkillUI.skill.Image_CoolTime.sprite = ActiveAbility.CoolTime;
        PlayerSkillUI.skill.HpPotionInt.text = MulYakInt.ToString();
        PlayerSkillUI.skill.PillInt.text = AlYakInt.ToString();
    }

    void UpdateState()
    {
        StateManager.state.MaxHp = Hp_Max;
        StateManager.state.Hp = Hp_Current;
    }

    public void Move()
    {
        //if (Input.GetKeyDown(settingmanager.GM.left)) { h = -1; }
        //if (Input.GetKeyUp(settingmanager.GM.left)) { h = 0; }

        //if (Input.GetKeyDown(settingmanager.GM.right)) { h = 1; }
        //if (Input.GetKeyUp(settingmanager.GM.right)) { h = 0; }
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

    public PlatformEffector2D pf;
    Vector2 vel;

    public void Jump()
    {
        if (P_JumpInt == 0) { rigid.AddForce(Vector3.up * 0); }
        else if (P_JumpInt > 0)
        {
            Debug.Log(P_JumpForce);
            rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime, ForceMode2D.Impulse);
            P_JumpInt -= 1;
            Ani.SetBool("Jump", true);
            if (vel.y > P_JumpForce)
            {
                vel.y = P_JumpForce;
                rigid.velocity = vel;
            }
        }
    }

    [Range(0f, 10f)]
    public float Distance;
    public LayerMask lm;

    void GroundCheck()
    {
        RaycastHit2D Ground = Physics2D.Raycast(SelectChar.transform.position, Vector2.down, 5);
        Debug.DrawRay(SelectChar.transform.position, Vector2.down, Color.blue);
        Physics2D.queriesStartInColliders = false;
        Debug.Log(Ground.collider.tag);
        if (Ground.collider.gameObject.tag == "Ground")
        {
            if (Ground.distance < Distance)
            {
                Ani.SetBool("Jump", false);
                P_JumpInt = P_MaxJumpInt;
            }
        }
    }

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
        if (MulYakInt > 0 && Input.GetKeyDown(KeyCode.Alpha1))
        {
            MulYakInt--;
            Hp_Current += 50;
            if(Hp_Current > Hp_Max) { Hp_Current = Hp_Max; }
            UpdateState();
            PlayerSkillUI.skill.HpPotionInt.text = MulYakInt.ToString();
        }
        else if (AlYakInt > 0 && Input.GetKeyDown(KeyCode.Alpha2))
        {
            AlYakInt--;
            AP_Timer = AP_Duration;
            PlayerSkillUI.skill.PillInt.text = AlYakInt.ToString();
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
                Active_Cool_Max = WereWolfCool;
                break;
            case 1:
                PharaoWandSwitch();
                Current_Use = PharaoWand_Senaka;
                Active_Cool_Max = PharaoCool;
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
                Active_Cool_Max = BattleAxeCool;
                break;
            case 5:
                EvillSwordSwitch();
                Current_Use = EvilSword;
                AM.EA = Current_Use.GetComponent<Animator>();
                Active_Cool_Max = EvilSworldCool; 
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
       
        Enhance_Health = ResourceManager.re.Enhance_Health;
        Enhance_Strength = ResourceManager.re.Enhance_Strength;
        Enhance_Speed = ResourceManager.re.Enhance_Speed;
        //ActiveAbility = ResourceManager.re.ActiveAbility;
        //PassiveAbility = ResourceManager.re.PassiveAbility;
        StateManager.state.DarkFog = AbyssManager.abyss.Darkfog;


    }

    public void Save_StateEnhance()
    {
        ResourceManager.re.Enhance_Health = Enhance_Health;
        ResourceManager.re.Enhance_Strength = Enhance_Strength;
        ResourceManager.re.Enhance_Speed = Enhance_Speed;
        //AbyssManager.abyss.Darkfog = Mathf.RoundToInt(AbyssManager.abyss.Darkfog * 0.1f);
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
        GameObject Text = (GameObject)Instantiate(Resources.Load("DMGCANVAS2"), CurrentCha.transform.position + Vector3.up * 1 + new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
        Text.GetComponent<DamageOBJ>().DamageText(Damage);
      
        float fontExtra = Mathf.Clamp(Damage / 3 , 5.0f, 10.0f);
        float fontsize = Random.Range(0.8f * fontExtra, 1.0f * fontExtra);
        Text.GetComponent<Text>().fontSize = (int)fontsize;
    }
}
