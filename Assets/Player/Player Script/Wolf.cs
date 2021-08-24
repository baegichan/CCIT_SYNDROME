using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float WolfHp = PlayerMovement.Hp;//플레이어 피를 받아옴
    public float WolfShild = PlayerMovement.Shild;//플레이어 쉴드를 받아옴
    public float WolfSpeed = 10;//이동속도
    public bool WolfDash = false;//대쉬사용 유무
    public float WolfDashPower = 50;//도약힘
    public float WolfDashInt = 1;//도약 횟수
    public float WolfDashDelay = 2;//도약딜레이
    public float WolfDashTime = 1;//도약 무적시간
    public int WolfJumpForce = 200;//점프력
    public float WolfJumpInt = 1;//점프횟수
    public bool WolfAttack = false;//공격 상태 유무
    public int WolfAttackPower = 20;//공격력
    public int WolfAttackInt = 0;//공격 타수 판별하는 수
    public float WolfAttackDelay = 1.0f;//공격 딜레이
    public float WolfInvincibilityNum = 0;//무적 유무
    public float WolfInvincibilityDelay = 1;//피격시 무적 시간
    public float Chargeing;
    public float[] ChargeingGage;
    
    public Transform UattackPoint;//위 히트박스 기준
    public Transform RattackPoint;//우 히트박스 기준
  
    public Vector2 UboxSize;//위 히트박스
    public Vector2 RboxSize;//우 히트박스


    SpriteRenderer Sp;
    public Sprite Wolfface;
    Vector2 mouse;
    SpriteRenderer sp;
    Rigidbody2D rigid;
    Animator ani;

   

    public float Wolf_BlackSmoke = PlayerMovement.BlackSmoke;//검은안개 재화
    

    void Start()
    {
        
        Sp = gameObject.GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        Sp.sprite = Wolfface;
    }

    public void FixedUpdate()
    {
        
    }

    void Update()
    {
        Debug.Log("내 Hp는" + WolfHp + "남았습니다");

        mouse = Camera.main.ViewportToScreenPoint(Input.mousePosition);//마우스 포인터 좌표받기


        Move();
        FlashPosition();
        Mouseflip();
        Flash();
        Ground();
        LeftT();
        LeftFlashT();
        //Attacks();
        Rope();
        //AttackNum();
        Invincibility();
    }

    public void ShildPower()//바위인간용 쉴드
    {
        WolfShild += WolfHp;

    }
    public void Move()//이동
    {
        float Horizontal = Input.GetAxis("Horizontal");

        Vector3 Positon = transform.position;

        Positon.x += Horizontal * Time.deltaTime * WolfSpeed;

        transform.position = Positon;

        if (Input.GetKeyDown(KeyCode.A))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
           
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
           
        }

    }

    public void Ground()//점프
    {

        if (WolfJumpInt != 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * WolfJumpForce);

            WolfJumpInt--;

        }



    }
   
    public void Rope()//w를 누르면 로프타기 아직 미구현
    {
        if (tag == "Rope")
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.up);
            }
        }
    }
    public void Mouseflip()//마우스 방향으로 플립
    {
        if (mouse.x <= 1763650)// 1920x1080 기준 중간지점
        {
            sp.flipX = true;
        }
        else if (mouse.x >= 1763650)
        {
            sp.flipX = false;
        }
    }
    public void LeftT()//점멸 쿨타임
    {
        WolfDashDelay -= Time.deltaTime;

        if (WolfDashDelay <= 0)
        {
            WolfDashInt = 1;


        }
    }
    public void LeftFlashT()//점멸시 무적타임
    {

        if (WolfDashInt <= 0)
        {

            WolfInvincibilityDelay -= Time.deltaTime;
        }
        else if (WolfDashInt == 1)
        {

            WolfInvincibilityDelay = 0.1f;
        }


        if (WolfInvincibilityDelay <= 0.05f)
        {
            WolfDash = true;

        }
        else if (WolfInvincibilityDelay >= 0.05f)
        {
            WolfDash = false;

        }
    }
    public void FlashPosition()//점멸시 적 통과
    {

        if (WolfDash == true)
        {
            Physics2D.IgnoreLayerCollision(10, 11);

        }
        else if (WolfDash == false)
        {
            Physics2D.IgnoreLayerCollision(10, 11, false);

        }
    }
    public void Flash()//이동기  //이곳에 공격구현하는게 편할듯? 1대쉬 5공격 구현
    {
        
        

            if (Input.GetMouseButtonDown(0))
            { 
                    rigid.AddForce(Vector3.right * WolfDashPower * 75);
                    WolfAttack = true;
                    WolfDashDelay = 2f;
                    Collider2D[] Rhit = Physics2D.OverlapBoxAll(RattackPoint.position, RboxSize, 0);
                    foreach (Collider2D collider in Rhit)
                    {
                            rigid.AddForce(Vector3.right * WolfDashPower * 5);
                            if (collider.tag == "Monster")
                            {
                                if (WolfAttack == true)
                                {
                                    /*for (WolfAttackInt = 0; WolfAttackInt == 5; WolfAttackInt++)
                                    {
                                        Monster.MonsterHp -= WolfAttackPower;
                                        Debug.Log(collider.tag + "우");
                                    }*/
                                     //Monster.MonsterHp -= WolfAttackPower;
                                     Debug.Log(collider.tag + "우");
                        }
                            }        

                    }

                
                if (WolfDashInt == 0)
                {


                    LeftT();
                    LeftFlashT();



                }
                else if (Input.GetMouseButtonUp(0))
                {

                    WolfAttack = false;


                }

            }
        

    }
    /*public void Attacks()//공격
    {
        if (Input.GetMouseButtonDown(0))
        {

            WolfAttack = true;
            if (WolfAttackInt < 5)
            {
                if (WolfAttack == true)
                {
                    Collider2D[] Lhit = Physics2D.OverlapBoxAll(LattackPoint.position, LboxSize, 0);
                    foreach (Collider2D collider in Lhit)
                    {
                        if (mouse.x <= 1763650)
                        {
                            rigid.AddForce(Vector3.left * WolfDashPower * 5);
                            if (collider.tag == "Monster")
                            {
                                if (WolfAttack == true)
                                {
                                    for (WolfAttackInt = 0; WolfAttackInt == 5; WolfAttackInt++)
                                    {
                                        Monster.MonsterHp -= WolfAttackPower;
                                        Debug.Log(collider.tag + "좌");
                                    }
                                    
                                }
                            }
                        }



                    }
                    Collider2D[] Uhit = Physics2D.OverlapBoxAll(UattackPoint.position, UboxSize, 0);
                    foreach (Collider2D collider in Uhit)
                    {
                        if (collider.tag == "Monster")
                        {
                            if (WolfAttack == true)
                            {
                                for (WolfAttackInt = 0; WolfAttackInt == 5; WolfAttackInt++)
                                {
                                    Monster.MonsterHp -= WolfAttackPower;
                                    Debug.Log(collider.tag + "위");
                                }
                            }
                        }


                    }
                    Collider2D[] Rhit = Physics2D.OverlapBoxAll(RattackPoint.position, RboxSize, 0);
                    foreach (Collider2D collider in Rhit)
                    {
                        if (mouse.x >= 1763650)
                        {
                            rigid.AddForce(Vector3.right * WolfDashPower * 5);
                            if (collider.tag == "Monster")
                            {
                                if (WolfAttack == true)
                                {
                                    for (WolfAttackInt = 0; WolfAttackInt == 5; WolfAttackInt++)
                                    {
                                        Monster.MonsterHp -= WolfAttackPower;
                                        Debug.Log(collider.tag + "우");
                                    }
                                }
                            }
                        }

                    }
                }

            }

        }
        else if (Input.GetMouseButtonUp(0))
        {

            WolfAttack = false;


        }

    }*/
    /*public void AttackNum()//공격타수
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (WolfAttackInt < 5)
            {
                WolfAttackInt++;


            }
        }
        if (WolfAttackInt == 5)
        {
            WolfAttack = false;
            WolfAttackDelay -= Time.deltaTime;



            if (WolfAttackDelay <= 0)
            {
                WolfAttackInt = 0;
                if (WolfAttackInt == 0)
                {

                    WolfAttackDelay = 1.0f;
                }
            }
        }
    }*/
    
    public void Invincibility()//피격시 무적
    {

        if (WolfInvincibilityNum == 1)
        {
            Sp.color = new Color(1, 0, 0); 
            Physics2D.IgnoreLayerCollision(10, 11);
            WolfInvincibilityDelay -= Time.deltaTime;
            if (WolfInvincibilityDelay <= 0)
            {
                Sp.color = new Color(255, 255, 255, 255);
                WolfInvincibilityDelay = 1;
                WolfInvincibilityNum = 0;
                Physics2D.IgnoreLayerCollision(10, 11, false);
            }
        }
        if (WolfInvincibilityNum >= 1)
        {
            WolfInvincibilityNum = 1;
        }
    }
    

    private void OnDrawGizmos()//히트박스 그리기
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(UattackPoint.position, UboxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(RattackPoint.position, RboxSize);
    }
    public void ChargeAlgorithm()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Chargeing++;
            if(Chargeing == 5)
            {
                ChargeingGage[0] = 1;
            }
        }
    }
    public void ItemChanger()
    {
         if(Input.GetKey(KeyCode.E))
        {

        }
    }
    public void SkilChanger1()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {

        }
    }
    public void SkilChanger2()
    {
        if(Input.GetKey(KeyCode.Alpha2))
        {

        }
    }
    public void ObjectInteraction()
    {
        if(Input.GetKey(KeyCode.R))
        {

        }
    }
    void OnCollisionEnter2D(Collision2D col)//충돌체크
    {

        if (col.gameObject.name == "ground")
        {
            WolfJumpInt = 1;

        }
        if (col.gameObject.tag == "Monster")//플레이어 피격
        {
           // WolfShild -= Monster.MonsterPower;
            Debug.Log("내 쉴드는" + WolfShild + "남았습니다");
            if (WolfShild <= 0)
            {
                if (col.gameObject.tag == "Monster")
                {
                    //WolfHp -= Monster.MonsterPower;

                }
                if (WolfHp <= 0)
                {
                    Destroy(this.gameObject, 3f);
                }
            }
        }
        if (col.gameObject.tag == "Monster")
        {
            WolfInvincibilityNum += 1;
        }


    }


}
