using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    [Header("게임 오브젝트")]
    [Tooltip("변신할 캐릭터 리스트")]
    public GameObject[] Char;
    public GameObject SelectChar;
    [Tooltip("능력 정보 받아올 매니저")]
    public GameObject abilityManager;
    //Animation ani;
    Rigidbody2D rigid;

    [Header("스테이터스")]
    public int P_hp;
    public int AbilityHP;
    public int DefualtHP;
    public int P_Maxhp { get { return AbilityHP + DefualtHP; } }
    public int P_M_Speed;
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
    float P_JumpInt = 10;
    public float P_DashForce;
    float P_DashInt = 1;
    float P_DashTimer = 2;

    [Header("소지 물약&능력")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        SelectChar = Char[0];
        ChangeCahr();
    }

    void FixedUpdate()
    {
        Move();
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
        if (Input.GetKeyDown(KeyCode.Tab)) { ChangeCahr(); }
    }

    //캐릭터 변경
    void ChangeCahr()
    {
        //임시 캐릭터 교체
        if(SelectChar == Char[0])
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
        //ani = GetComponent<Animation>();

        switch (SelectChar.transform.name)
        {
            case "Defualt":
                atk = SelectChar.GetComponent<child1>().Attack;
                break;
            case "Wolf":
                atk = SelectChar.GetComponent<child2>().Attack;
                break;
        }
    }
    //

    //이동
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);

        switch (h)
        {
            case -1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rigid.AddForce(Vector3.left * P_DashForce * 2);
                    P_DashInt = 0;
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rigid.AddForce(Vector3.right * P_DashForce * 2);
                    P_DashInt = 0;
                }
                break;
        }

        if (P_DashInt == 0)
        {
            P_DashForce = 0;
            P_DashTimer -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(10, 11);
        }
        if (P_DashTimer <= 0)
        {
            P_DashTimer = 2;
            P_DashInt = 1;
            P_DashForce = 300;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
    }
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            P_JumpInt = P_MaxJumpInt;
        }
    }
    //

    //능력
    public delegate void useAbility();
    useAbility ability;

    public void SelectAbility()
    {
        AbilityManager AM = abilityManager.GetComponent<AbilityManager>();

        switch (ActiveAbility.AbCode)
        {
            case 0:
                ability = new useAbility(AM.Werewolf);
                break;
            case 1:
                ability = new useAbility(AM.Parao);
                break;
            case 2:
                ability = new useAbility(AM.BomberMan);
                break;
            case 3:
                ability = new useAbility(AM.Ability_D);
                break;
            case 4:
                ability = new useAbility(AM.Ability_E);
                break;
            case 5:
                ability = new useAbility(AM.Ability_F);
                break;
            case 6:
                ability = new useAbility(AM.Double_Jump);
                break;
        }
    }

    void UseSkill()
    {
        switch (ActiveAbility.AbName)
        {
            case "Werewolf":
                ability();
                break;
            case "Parao":
                ability();
                break;
            case "BomberMan":
                ability();
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
