using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerindigo : MonoBehaviour
{ 
    /// 플레이어 스테이터스
    public float P_Hp;
    public int P_Money;
    public bool P_OtherWorld = false;//2021.10.07 김재헌
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
    Vector2 Mouse;
    Vector2 PlayerPosition;
    ///
    /// 플레이어 특수능력 관련 함수
    public GameObject abilityManager;
    public int MulYakInt;
    public int AlYakInt;
    /// 
    /// 플레이어 공격
    public float P_AttackForce;
    public int P_AttackInt = 0;
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
    Camera Cam;
    Animation ani;
    Rigidbody2D rigid;
    public Ability ActiveAbility;
    public Ability PassiveAbility;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();
        Cam = Camera.main;
    }
    void FixedUpdate()
    {
        Move();

    }
    void Update()
    {
        PlayerPosition = Cam.WorldToScreenPoint(transform.position);//마우스 포인터 좌표받기
        Mouse = Input.mousePosition;
        UseItem();
        Jump();
        //JumpRay();
        MouseFilp();
        Attack();
        if (ActiveAbility.AbSprite != null)
        {
            UseSkill();
        }
       
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

        P_J_Ray = Physics2D.Raycast(transform.position + Vector3.down ,Vector3.down, P_J_RayDistance, layerMask);//, LayerMask.NameToLayer("Ground"));
        
        Debug.DrawRay(transform.position, Vector3.down * P_J_RayDistance, new Color(1,0,0));

        
       
        if (P_J_Ray.collider.gameObject.layer == 31)
        {
            
            P_JumpInt = P_MaxJumpInt;
        }
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (P_AttackInt <= 3)
            {
                P_AttackInt++;
            }

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

                if (Input.GetMouseButtonDown(0))
                {
                    P_AttackResetTimer = 0.8f;
                }
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

                if (Input.GetMouseButtonDown(0))
                {
                    P_AttackResetTimer = 0.8f;
                }
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

            case 4:
                if(P_AttackInt == 4) {P_AttackResetTimer = 0.8f;}
                P_AttackResetTimer -= Time.deltaTime;
                if (P_AttackResetTimer == 0)
                {
                  P_AttackInt = 0;
                }
                P_AttackState = false;
               
                
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

    void OnDrawGizmos()
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
    

    public void WorldChange()//이면세계 전환 2021.10.07 김재헌
    {
        GameObject A = GameObject.FindGameObjectWithTag("Player");
        GameObject B = GameObject.FindGameObjectWithTag("OtherPlayer");//임시
        if (P_OtherWorld == false)
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = true;//이면세계 진입 구현 필요            
                Instantiate(B,A.transform);
                Destroy(A,0f);
            }
        }
        if(P_OtherWorld == true)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                P_OtherWorld = false;//기본세계 진입 구현 필요
                Instantiate(A, B.transform);
                Destroy(B, 0f);
            }
        }
    }
    
    public void MouseFilp()
    {
        
        if (Mouse.x <= PlayerPosition.x)// 1920x1080 기준 중간지점
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Mouse.x > PlayerPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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



