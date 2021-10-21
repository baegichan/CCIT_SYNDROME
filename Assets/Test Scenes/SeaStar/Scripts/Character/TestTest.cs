using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : Character
{
    [Header("게임 오브젝트")]
    [Tooltip("변신할 캐릭터 리스트")]
    public GameObject[] Char;
    public GameObject SelectChar;
    //Animation ani;
    public static Rigidbody2D rigid;
    AbilityManager AM;

    [Header("플레이어 스테이터스")]
    public int DefaultHP;
    public int CharHP;
    public int CharDP;
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
    public static bool IsCharging;

    [Header("소지 물약&능력")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        AM = GetComponent<AbilityManager>();
        rigid = GetComponent<Rigidbody2D>();
        SelectChar = Char[0];
        ChangeChar(SelectChar);
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        if (ActiveAbility.AbSprite != null)
        {
            UseSkill();
        }
        UseItem();
        if (Input.GetKeyDown(KeyCode.Space)) { Jump(); }
        if (Input.GetMouseButtonDown(0)) { atk(); }
        Move();
        ds();
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
        Hp_Max = DefaultHP + CharHP;
        DP = CharDP;
        AM.py = SelectChar;
    }
    //

    //이동
    public void Move()
    {
        h = Input.GetAxis("Horizontal");
        if (!IsCharging)
            transform.position += new Vector3(h * speed * Time.deltaTime, 0);
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
        }
    }
    //

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
        switch (ActiveAbility.AbCode)
        {
            case 0:
                active();
                break;
            case 1:
                active();
                break;
            case 2:
                active();
                break;
            case 3:
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
