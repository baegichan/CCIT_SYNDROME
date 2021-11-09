using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tt : MonoBehaviour
{
    // Start is called before the first frame update
    /// Test
    public int A;
    public int B;
    public int C;

    public void Test_Attack_Passive()
    {

    }

    /// Test
    /// 플레이어 스테이터스
    public float P_Hp;
    public int P_Money;
    ///
    /// 플레이어 이동
    public float P_M_Speed;
    public float P_JumpForce;
    public float P_MaxJumpInt = 1;
    public float P_JumpInt;
    public RaycastHit2D P_J_Ray;
    public float P_J_RayDistance;
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 2;
    ///
    /// 플레이어 특수능력 관련 함수
    public GameObject abilityManager;
    public int MulYakInt;
    public int AlYakInt;
    /// 
    /// 플레이어 공격
    public float P_AttackForce;
    public float P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_AttackState = false;
    public float P_AttackResetTimer = 0.8f;
    public Transform P_FrontAttack;

    public Transform P_TopAttack;
    public Vector2 P_UBox_Size;
    public Vector2 P_RBox_Size;
    /// <summary>
    /// 
    /// </summary>

    Animation ani;
    Rigidbody2D rigid;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();
    }
    void FixedUpdate()
    {
        Move();
        Attack();

    }
    void Update()
    {
        if (ActiveAbility.AbSprite != null)
        {
            UseSkill();
        }
        UseItem();
        Jump();
        JumpRay();
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
        //if (P_JumpInt > 1)
        //{
        //    P_JumpInt = 1;
        //}
        switch (P_JumpInt)
        {
            case 2:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpWorking();
                }
                break;
            case 1:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpWorking();
                }
                break;
            case 0:

                rigid.AddForce(Vector3.up * 0);
                break;
        }
    }
    void JumpWorking()
    {
        Debug.Log("작동");
        rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime);
        P_JumpInt -= 1;
    }
    void JumpRay()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player"));

        P_J_Ray = Physics2D.Raycast(transform.position + Vector3.down, Vector3.down, P_J_RayDistance, layerMask);//, LayerMask.NameToLayer("Ground"));

        Debug.DrawRay(transform.position, Vector3.down * P_J_RayDistance, new Color(1, 0, 0));



        if (P_J_Ray.collider.gameObject.layer == 31)
        {

            P_JumpInt = P_MaxJumpInt;
        }
    }

    public void Attack() //전해성 수정
    {
        if (Input.GetMouseButton(0))
        {
            P_AttackInt += 1;
            P_AttackResetTimer = 0.8f;
            Debug.Log("공격 작동");
        }
        switch (P_AttackInt)
        {
            case 0:
                P_AttackState = false;
                break;
            case 4:
                P_AttackInt = 0;
                break;
            default:
                if (P_AttackResetTimer <= 0)
                {
                    P_AttackInt = 0;
                }
                P_AttackState = true;
                break;
        }
        if (P_AttackResetTimer > 0)
            P_AttackResetTimer -= Time.deltaTime;
        if (P_AttackState) { /*AttackBoundary();*/ }
    }

    //public void AttackBoundary()
    //{
    //    Collider2D[] Uhit = Physics2D.OverlapBoxAll(P_TopAttack.position, P_UBox_Size, 0);
    //    foreach (Collider2D collider in Uhit)
    //    {
    //        if (collider.tag == "Monster")
    //        {
    //            if (P_AttackState == true)
    //            {
    //                TestMonster.M_Hp -= P_AttackForce;//몬스터 체력 스크립트 수정 필요
    //                Debug.Log(collider.tag + "위");
    //            }
    //        }
    //    }
    //    Collider2D[] Rhit = Physics2D.OverlapBoxAll(P_FrontAttack.position, P_RBox_Size, 0);
    //    foreach (Collider2D collider in Rhit)
    //    {
    //        if (collider.tag == "Monster")
    //        {
    //            if (P_AttackState == true)
    //            {
    //                TestMonster.M_Hp -= P_AttackForce;//몬스터 체력 스크립트 수정 필요
    //                Debug.Log(collider.tag + "우");
    //            }
    //        }
    //    }
    //}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(P_TopAttack.position, P_UBox_Size);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(P_FrontAttack.position, P_RBox_Size);
    }
    public void AttackSlow()//공격시 Player 이동속도 조절 //전해성 수정
    {
        switch (P_AttackInt)
        {
            case 0:
                P_M_Speed = 10;
                break;
            default:
                P_M_Speed = 3;
                break;
        }
    }
    public void AttackReset()
    {


    }


    void OnCollisionEnter2D(Collision2D col)
    {
        /*if (col.gameObject.tag == "Ground")
        {
            Debug.Log("점프 +1");
            P_JumpInt = P_MaxJumpInt;
        }*/
    }
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
}