using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [Header("게임 오브젝트")]
    [Tooltip("변신할 캐릭터 리스트")]
    public GameObject[] Char;
    public GameObject SelectChar;
    [Tooltip("능력 정보 받아올 매니저")]
    public GameObject abilityManager;
    //Animation ani;
    Rigidbody2D rigid;
    AbilityManager AM;
    Camera Cam;
    
    [Header("스테이터스")]
    ///플레이어 
    //스테이터스
    public int P_hp;
    public int AbilityHP;
    public int DefualtHP;
    public int P_Maxhp { get { return AbilityHP + DefualtHP; } }
    public bool P_OtherWorld = false;//2021.10.12 김재헌
    //
    //이동
    public int P_M_Speed;
    Vector2 Mouse;//2021.10.12 김재헌
    Vector2 PlayerPosition;//2021.10.12 김재헌
    //
    //점프
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
    float P_JumpInt = 10;
    //
    //대쉬
    public float P_DashForce;
    float P_DashInt = 1;
    float P_DashTimer = 2;
    //
    [Header("소지 물약&능력")]
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    Animator Ani;

    void Awake()
    {
        Cam = Camera.main;//2021.10.12 김재헌
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
        PlayerPosition = Cam.WorldToScreenPoint(transform.position);//마우스 포인터 좌표받기//2021.10.12 김재헌
        Mouse = Input.mousePosition;//2021.10.12 김재헌
        MouseFilp();//2021.10.12 김재헌

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
        //ani = GetComponent<Animation>();

        switch (SelectChar.transform.name)
        {
            case "Defualt":
                atk = SelectChar.GetComponent<child1>().Attack;
                break;
            case "MainCharacter_Eden":
                atk = SelectChar.GetComponent<MentalChaild>().Attack;
                break;
        }
    }
    //

    //이동
    public void Move()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);
        if (h == 1)
        {
            Ani.SetInteger("Move", 1);
        }
        

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
    public delegate void Attack();
    Attack atk;
    //

    //점프
    public void Jump()
    {
        if (P_JumpInt == 0) { rigid.AddForce(Vector3.up * 0); }
        else if (P_JumpInt > 0)
        {
            rigid.AddForce(Vector3.up * P_JumpForce * 150 * Time.deltaTime);
            P_JumpInt -= 1;
        }
    }
    //마우스 플립
    public void MouseFilp()//2021.10.12 김재헌
    {

        if (Mouse.x <= PlayerPosition.x)// 1920x1080 기준 중간지점
        {
            SelectChar.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Mouse.x > PlayerPosition.x)
        {
            SelectChar.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    //
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
