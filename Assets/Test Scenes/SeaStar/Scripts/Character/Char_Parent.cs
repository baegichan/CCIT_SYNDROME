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
    Camera Cam;

    [Header("플레이어 스테이터스")]
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
    public static float h;
    public bool P_OtherWorld = false;
    public static bool RedBullDash = false;
    Vector2 Mouse;//2021.10.12 김재헌
    Vector2 PlayerPosition;//2021.10.12 김재헌

    //[Header("능력치 강화 수치")]
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
        PlayerPosition = Cam.WorldToScreenPoint(SelectChar.transform.position);//마우스 포인터 좌표받기//2021.10.12 김재헌
        Mouse = Input.mousePosition;//2021.10.12 김재헌
        MouseFilp();//2021.10.12 김재헌

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

    //캐릭터 변경
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

    //이동
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
            rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime);
            P_JumpInt -= 1;
            Ani.SetBool("Jump", true);
        }
    }
    //

    //마우스 플립

    Vector3 CharScale;

    public void MouseFilp()//2021.10.12 김재헌
    {
        if (Mouse.x <= PlayerPosition.x)// 1920x1080 기준 중간지점
        {
            SelectChar.transform.localScale = new Vector3(-CharScale.x, CharScale.y, CharScale.z);
        }
        else if (Mouse.x > PlayerPosition.x)
        {
            SelectChar.transform.localScale = new Vector3(CharScale.x, CharScale.y, CharScale.z);
        }
    }

    public void WorldChange()//이면세계 전환 //2021.10.12 김재헌
    {
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        GameObject B = GameObject.FindGameObjectWithTag("OtherPlayer");//임시
        if (P_OtherWorld == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = true;//이면세계 진입 구현 필요            
                Instantiate(B, A.transform);
                Destroy(A, 0f);
            }
        }
        if (P_OtherWorld == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = false;//기본세계 진입 구현 필요
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
                break;
        }
    }

    void UseItem()
    {
        if (MulYakInt > 0 && Input.GetKeyDown(KeyCode.E))
        {
            MulYakInt--;
            Debug.Log("물약 사용");
        }
        else if (AlYakInt > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            AlYakInt--;
            Debug.Log("알약 사용");
        }
    }
    //
}
