using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM_ : MonoBehaviour
{
    public GameObject player;
    public float P_Hp;
    public float P_M_Speed;
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

        //{chp + php}
    }
    public float P_JumpInt;
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 2;
    public int MulYakInt;
    public int AlYakInt;
    public int P_Money;

    public float P_DefaultAttack = 10;
    public float P_AttackForce
    {
        get { return P_DefaultAttack; }
        set { P_DefaultAttack = value; }
    }
    public float P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_AttackState = false;
    public float P_AttackResetTimer = 0.8f;
    public Transform P_FrontAttack;
    public Transform P_TopAttack;
    public Vector2 P_UBox_Size;
    public Vector2 P_RBox_Size;

    Animation ani;
    Rigidbody2D rigid;
    AbilityManager AM;

    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        AM = GetComponent<AbilityManager>();
        rigid = player.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();
    }

    void FixedUpdate()
    {
        Move();
        //Attack();
    }

    void Update()
    {
        if (ActiveAbility.AbSprite != null)
        {
            UseSkill();
        }
        UseItem();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public void Move()//Move안에 대쉬까지 만듦
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
    public void Jump()
    {
        if(P_JumpInt == 0) { rigid.AddForce(Vector3.up * 0); }
        else if (P_JumpInt > 0) { jump(); }
    }

    void jump()
    {
        rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime);
        P_JumpInt -= 1;
    }

    public void Attack()
    {
        if (/*Input.GetKeyDown((KeyCode)settingmanager.GM.nomalattack)*/Input.GetMouseButtonDown(0))
        {
            P_AttackInt++;
            Debug.Log("공격 작동");
        }
        else if (P_AttackInt > 3)
        {
            P_AttackState = false;
            P_AttackInt = 0;
        }
        switch (P_AttackInt)
        {
            case 0:
                P_AttackState = false;
                P_AttackResetTimer = 0.8f;
                break;

            case 1:
                P_AttackState = true;
                P_AttackResetTimer -= Time.deltaTime;
                if (P_AttackResetTimer <= 0)//공격하다가 중간에 멈추면 다시 1타로 초기화.
                {
                    P_AttackInt = 0;
                    P_AttackState = false;
                    P_AttackResetTimer = 0.8f;
                }
                if (P_AttackState == true)
                {
                    AttackBoundary();
                }
                break;

            case 2:
                P_AttackResetTimer = 0.8f;
                P_AttackResetTimer -= Time.deltaTime;
                if (P_AttackResetTimer <= 0)
                {
                    P_AttackInt = 0;
                    P_AttackState = false;
                    P_AttackResetTimer = 0.8f;
                }
                P_AttackState = true;
                if (P_AttackState == true)
                {
                    AttackBoundary();
                }
                break;

            case 3:
                P_AttackResetTimer = 0.8f;
                P_AttackResetTimer -= Time.deltaTime;
                if (P_AttackResetTimer <= 0)
                {
                    P_AttackInt = 0;
                    P_AttackState = false;
                    P_AttackResetTimer = 0.8f;
                }
                P_AttackState = true;
                if (P_AttackState == true)
                {
                    AttackBoundary();
                }
                break;
        }
    }
    public void AttackBoundary()
    {
        Collider2D[] Uhit = Physics2D.OverlapBoxAll(P_TopAttack.position, P_UBox_Size, 0);
        foreach (Collider2D collider in Uhit)
        {
            if (collider.tag == "Monster")
            {
                if (P_AttackState == true)
                {
                    TestMonster.M_Hp -= P_AttackForce;//몬스터 체력 스크립트 수정 필요
                    Debug.Log(collider.tag + "위");
                }
            }
        }
        Collider2D[] Rhit = Physics2D.OverlapBoxAll(P_FrontAttack.position, P_RBox_Size, 0);
        foreach (Collider2D collider in Rhit)
        {

            if (collider.tag == "Monster")
            {
                if (P_AttackState == true)
                {
                    TestMonster.M_Hp -= P_AttackForce;//몬스터 체력 스크립트 수정 필요
                    Debug.Log(collider.tag + "우");
                }
            }
        }
    }

    public void OnDrawGizumos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(P_TopAttack.position, P_UBox_Size);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(P_FrontAttack.position, P_RBox_Size);
    }
    public void AttackSlow()//공격시 Player 이동속도 조절
    {
        switch (P_AttackInt)
        {
            case 0:
                P_M_Speed = 10;
                break;
            case 1:
                P_M_Speed = 3;
                break;
            case 2:
                P_M_Speed = 3;
                break;
            case 3:
                P_M_Speed = 3;
                break;
        }
    }

    public void AttackReset()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            P_JumpInt = P_MaxJumpInt;
        }
    }

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
}