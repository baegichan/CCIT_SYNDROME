using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gammanager;

    //가챠 관련
    public static bool gacha = false;
    public static int PillInt = 0;//알약먹는 변수
    public static bool NinjapetPillsEatPill = false;
    public static bool KitpetEatPill = false;
    public static bool DronpetEatPill = false;
    public int YaminoDarkEnergy;
    //
    ///상태
    ///
    public float TestHp = Hp;
    public static float Max_Hp;// 힐링 스크립트에 이 값이 존재하길래 임시로 만들어둠
    public static float Hp = 100;//= PlayerPrefs.GetInt("PlayerHP");//캐릭터 체력
    public static float Shild = 0;//바위인간용 쉴드
    public static bool flash = false;//점멸 사용 유무
    public static float BigNum = 0;//거대화 알약용
    public static float BlackSmoke = 0;//검은안개 재화
    /// 이동
     public int speed;//움직이는 속도
    public int jumpforce;//점프 강도
    public int flashp = 1000;//이동기 속도
    public static int JumpInt = 1;//점프 가능 횟수
    public int testJumpInt;
    public static int JumpPoint = 1;
    public float AniJump;//애니메이션 더블점프를 위한 변수
    public static int FlashInt = 1;//점멸 가능 횟수
    public float LeftTime; // 점멸 쿨타임
    public float FlashTime; //무적타임
    public static float RedBullInt = 0;//성난황소애니메이션용
    ///
    //공격                     
    public bool Attack;//공격상태 유무
    public static float AttackPower = 40;//공격력
    public int AttackInt = 0;//공격 타수 판별하는 수
    public float AttackDelay = 1.0f;//공격 딜레이
    public float ReAttack = 0.8f;//공격중 정지했을때 초기화
    public float InvincibilityNum = 0;//무적 유무
    public float InvincibilityDelay = 1;//피격시 무적 시간
   // public Transform LattackPoint;//좌 히트박스 기준
    public Transform UattackPoint;//위 히트박스 기준
    public Transform RattackPoint;//우 히트박스 기준
   // public Vector2 LboxSize;//좌 히트박스
    public Vector2 UboxSize;//위 히트박스
    public Vector2 RboxSize;//우 히트박스
    public void damage(int i)
    {
        
    }

    SpriteRenderer Sp;
    Vector2 mouse;
    SpriteRenderer sp;
    Rigidbody2D rigid;
    Animator ani;
    MeshRenderer Mesh;

    ///Player공격속도 조절을 위한 
    public float ClickTimer = 1f;
    public bool ClickAttack = false;
    public bool Indigo = false;
    /// <TEST>
    public void pickupweapon()
    {
        ani.SetBool("weapon", true);
       
    }
    public IEnumerator animation_getitem()
    {
        yield return new WaitForSeconds(1.5f);
        pickupweapon();
    }
    public enum currentactiveskill
    {
        none,
        wolf
        
    }
    public enum currentpassiveskill
    {
        none,
        doublejump,
        rushattack,
        
    }
    public enum currentpet
    {
        none,
        ninja,
        Drone,
        meditkit,
        drug,
        GMD,
        
    }
    public currentactiveskill activeskill;
    public currentpassiveskill passiveskill;
    public currentpet pet;
    void Start()
    {
        
        Sp = gameObject.GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
      
    }

    public void FixedUpdate()
    {
        Indigo = Input.GetMouseButtonDown(0);
        
    }

    void Update()
    {      Debug.Log("내 Hp는" + Hp + "남았습니다");
        
        mouse = Camera.main.ViewportToScreenPoint(Input.mousePosition);//마우스 포인터 좌표받기
        Move();
        FlashPosition();
        Mouseflip();
        Flash();
        Ground();
        LeftT();
        LeftFlashT();
        Attacks();
        Rope();
        AttackNum();
        ReAttackTimer();
        Invincibility();
        TestAttackSlow();
        NomalAttackDelay();
        Dead(); 
    }
   
    public void ShildPower()//바위인간용 쉴드
    {
        Shild += Hp;

    }
    public void Move()//이동
    {
        
        float Horizontal = Input.GetAxis("Horizontal");
        //이동 수정필요.
        Vector3 Positon = transform.position;

        Positon.x += Horizontal * Time.deltaTime * speed;

        transform.position = Positon;
       
        if (Input.GetKeyDown((KeyCode)settingmanager.GM.left))
        {
            /*if(rigid.velocity == new Vector2(speed,0))
            {
                ani.SetBool("Run", true);
            }*/
            ani.SetBool("Run", true);

        }
        if (Input.GetKeyDown((KeyCode)settingmanager.GM.right))
        {
            ani.SetBool("Run", true);
        }
        if (Input.GetKeyUp((KeyCode)settingmanager.GM.left))
        {
            ani.SetBool("Run", false);
        }
        if (Input.GetKeyUp((KeyCode)settingmanager.GM.right))
        {
            ani.SetBool("Run", false);
        }

    }

    public void Ground()//점프
    {
        if (JumpInt != 0 && Input.GetKeyDown((KeyCode)settingmanager.GM.jump))
        {
            rigid.AddForce(Vector3.up * jumpforce * 50);

            JumpInt--;

            ani.SetBool("Jump", true);

            AniJump++;
            if(Input.GetMouseButtonDown(0))
            {
                AttackInt = 1;
                ani.SetBool("JumpATK", true);
                if(Input.GetMouseButtonDown(0))
                {
                    AttackInt = 0;
                    ani.SetBool("JumpATK", false);
                }
            }
        }
        if (Input.GetKeyDown((KeyCode)settingmanager.GM.jump))
        {
            if (AniJump == 2)
            {
                ani.SetBool("Double Jump", true);
            }
            else
            {
                ani.SetBool("Double Jump", false);
            }
        }
        if (Input.GetKeyUp((KeyCode)settingmanager.GM.jump))
        {
            ani.SetBool("Jump", false);
            
        }
        
    }
   
    public void RedBullOFF()
    {
        rigid.velocity= new Vector2(0,0);
    }
    public void Flash()//이동기 
    {
        if (Input.GetKey((KeyCode)settingmanager.GM.dash) && Input.GetKey((KeyCode)settingmanager.GM.left))
        {
            if (FlashInt != 0)
            {
                ani.SetTrigger("Dash");
                rigid.AddForce(Vector3.left * flashp * 10);

                FlashInt--;
                rigid.angularVelocity = 0;
                rigid.velocity = Vector2.zero;
                LeftTime = 2f;
                if (RedBullInt == 1)
                {
                   
                    ani.SetTrigger("Dash");
                    Invoke("RedBullOFF()", 2f);
                }
            }
            if (FlashInt == 0)
            {

                
                LeftT();
                LeftFlashT();



            }


        }
        else if (Input.GetKey((KeyCode)settingmanager.GM.dash) && Input.GetKey((KeyCode)settingmanager.GM.right))
        {


            if (FlashInt != 0)
            {
                ani.SetBool("Dash", true);
                rigid.AddForce(Vector3.right * flashp * 10);
                FlashInt--;
                LeftTime = 2f;
                Invoke("DashOFF()", 0.3f);
                RedBullOFF();
                if (RedBullInt == 1)
                {
                    
                    ani.SetBool("RedBullDash", true);
                    Invoke("RedBullOFF()", 2f);
                }

            }
            if (FlashInt == 0)
            {
                
                LeftT();
                LeftFlashT();



            }


        }
     
    }
    public void Rope()//w를 누르면 로프타기 아직 미구현
    {
        if(tag == "Rope")
        {
            if(Input.GetKey(KeyCode.W))
            {
                transform.position = Vector2.up;
                transform.Translate(0, speed, 0);
                transform.up = Vector2.up;
                this.transform.position = Vector2.up;
                this.gameObject.transform.position = Vector2.up;
                
            }
            if(Input.GetKey(KeyCode.S))
            {
                transform.Translate(0, -speed * Time.deltaTime, 0);
            }
        }
    }
    public void Mouseflip()//마우스 방향으로 플립
    {
        if (mouse.x <= 1763650)// 1920x1080 기준 중간지점
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (mouse.x >= 1763650)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void LeftT()//점멸 쿨타임
    {
        LeftTime -= Time.deltaTime;

        if (LeftTime <= 0)
        {
            FlashInt = 1;
            

        }
    }
    public void LeftFlashT()//점멸시 무적타임
    {
       
        if(FlashInt <= 0)
        {
            
            FlashTime -= Time.deltaTime;
        }
        else if(FlashInt == 1)
        {
           
            FlashTime = 0.1f;
        }


        if (FlashTime <= 0.05f)
        {
            flash = true;
             
        }
        else if (FlashTime >= 0.05f)
        {
            flash = false;
           
        }
    }
    public void FlashPosition()//점멸시 적 통과
    {
        
        if (flash == true)
        {
            Physics2D.IgnoreLayerCollision(10, 11);
           
        }
        else if(flash == false)
        {
            Physics2D.IgnoreLayerCollision(10, 11,false);
           
        }
    }
    public void TestAttackSlow()//공격시 Player 이동속도 조절
    {
        switch (AttackInt){

            case 0 :
                
                    speed = 10;
                    break;

            case 1 :

                speed = 3;
                break;

            case 2 :

                speed = 3;
                break;

            case 3 :

                speed = 3;
                break;




        }

    }
    public void NomalAttackDelay()//기본공격의 속도조절
    {
        
        if (ClickTimer >= 0)
        {
            
            ClickTimer -= Time.deltaTime;
            
            if(Input.GetMouseButtonDown(0))
            {
                
                ClickAttack = false;
            }
            else if(Input.GetMouseButtonDown(0))
            {
                ClickAttack = true;
            }
            if(ClickTimer <= 0)
            {
                ClickAttack = true;
                ClickTimer = 1f;
                
                
            }
            if(ClickAttack == true)
            {
                Indigo = true;
            }
            else if (ClickAttack == false)
            {
                Indigo = false;       
            }
        }

    }
    public void Attacks()//공격
    {
        if(Input.GetKeyDown((KeyCode)settingmanager.GM.nomalattack))
        {
         
            Attack = true;
           if(AttackInt < 3)
            {
               if(Attack == true)
                {
                    /*Collider2D[] Lhit = Physics2D.OverlapBoxAll(LattackPoint.position, LboxSize, 0);
                    foreach (Collider2D collider in Lhit)
                    {
                        if (mouse.x <= 1763650)
                        {
                            if (collider.tag == "Monster")
                            {
                                if (Attack == true)
                                {
                                    TestMonster.M_Hp -= AttackPower;
                                    Debug.Log(collider.tag + "좌");
                                }
                            }
                        }



                    }*/
               
                    Collider2D[] Rhit = Physics2D.OverlapBoxAll(RattackPoint.position, RboxSize, 0);
                    foreach (Collider2D collider in Rhit)
                    {
                        
                            if (collider.tag == "enemy")
                            {

                            collider.GetComponent<AddForce_>().check();
                            collider.GetComponent<MeleeAttack>().damaged((int)AttackPower);
                            gammanager.GetComponent<Damagetextspawn>().damagespawner(collider.gameObject, (int)AttackPower);


                            if (Attack == true)
                                {
                                
                                    TestMonster.M_Hp -= AttackPower;
                                    Debug.Log(collider.tag + "우");
                                }
                            }
                    }
                }
               
            }
          
        }
        else if(Input.GetKeyUp((KeyCode)settingmanager.GM.nomalattack))
        {
           
            Attack = false;
            
            
        }

    }
    
    public void AttackNum()//공격타수
    {
        if(Input.GetMouseButtonDown(0))
        {
           
            if (AttackInt < 3)
            {
               
                AttackInt++;
                ani.SetInteger("Attack", AttackInt);

            }
        }
        if (AttackInt == 3)
        {
                Attack = false;
                AttackDelay -= Time.deltaTime;
            
            

            if (AttackDelay <= 0)
            {
                AttackInt = 0;
                if (AttackInt == 0)
                {
                    
                    AttackDelay = 1.0f;
                }
            }
        }
    }
    public void ReAttackTimer()//공격시 멈추면 2초후 공격 모션 초기화
    {
        if (AttackInt == 0)
        {
            ReAttack = 0.8f;
        }
        if (AttackInt == 1)
        {
            ReAttack -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                ReAttack = 0.8f;
            }
            if (ReAttack <= 0)
            {
                ReAttack = 0.8f;
                AttackInt = 0;
                ani.SetInteger("Attack", AttackInt);
            }

        }
        if (AttackInt == 2)
        {
            ReAttack -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                ReAttack = 0.8f;
            }

            if (ReAttack <= 0)
            {
                ReAttack = 0.8f;
                AttackInt = 0;
                ani.SetInteger("Attack", AttackInt);
            }
        }

        if (AttackInt == 3)
        {
            ReAttack -= Time.deltaTime;

            ani.SetInteger("Attack", AttackInt);

        }

        
    }
    public void Invincibility()//피격시 무적
    {
        /*
        if(InvincibilityNum == 1)
        {
            Mesh.material.color = new Color(1, 0, 0); 
            Physics2D.IgnoreLayerCollision(10, 11);
            InvincibilityDelay -= Time.deltaTime;
            if (InvincibilityDelay <= 0)
            {
                Sp.color = new Color(255, 255, 255,255);
                InvincibilityDelay = 1;
                InvincibilityNum = 0;
                Physics2D.IgnoreLayerCollision(10, 11,false);
            }
        }
        if (InvincibilityNum >= 1)
        {
            InvincibilityNum = 1;
        }*/
    }
    public void Bigman()//거대화 알약전용 아직 미구현
    {
        if(BigNum == 1)
        {
            transform.localScale = new Vector2(200, 200);
        }
    }

    private void OnDrawGizmos()//히트박스 그리기
    {
        /*Gizmos.color = Color.red;
       Gizmos.DrawWireCube(LattackPoint.position, LboxSize);*/

   
    }
    public void ItemChanger()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //1번 아이템 변경
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //2번 아이템 변경
        }
    }
    public void SkilChanger()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            //스킬 변경
        }
    }
    public void ObjectInteraction()//오브젝트 상호작용
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //오브젝트 상호작용
            //Use Energe도 여기에? 
            ani.SetBool("Use Energe", true);
            ani.SetBool("Get Items", true);
        }
    }
   public void Dead()//사망모션
    {
        if(Hp <= 0)
        {
            ani.SetBool("Dead", true);//플레이어 사망 모션     
            Destroy(this.gameObject, 6f);
            gameObject.GetComponent<PlayerMovement>().enabled = false;
        }

       
    }
    void OnCollisionEnter2D(Collision2D col)//충돌체크
    {

        if (col.gameObject.tag == "Ground")
        {
           
            JumpInt = JumpPoint;
            ani.SetBool("Ground", true);
            AniJump = 0;
            Debug.Log("Ground True");
        }
       if(col.gameObject.tag == "Monster")//플레이어 피격
        {
            Shild -= TestMonster.M_Power;
            Debug.Log("내 쉴드는" + Shild + "남았습니다");
            if(Shild <= 0)
            {
                if (col.gameObject.tag == "Monster")
                {
                    Hp -= TestMonster.M_Power;
                   
                }     
                if (Hp <= 0)
                {
                    Dead();
                }
            }
        }
       if(col.gameObject.tag == "Monster")
        {
            InvincibilityNum += 1;
        }
       

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Rope")
        {
            Rope();           
        }
        if (col.tag == "gacha")
        {
            if (Input.GetKey(KeyCode.T))
            {
                
            }
        }
        if(col.tag == "Pill")
        {
            PillInt = 1;
            if(Input.GetKeyDown(KeyCode.T))
            {

                PillInt = 0;
            }
        }
    }
    // OnTriggerStay2D가 없다는 가정하에 맨 아래에 추가해 주면 됨 같이 주는 프리펩들이 tag 번호를 가지고 있으니깐 이거는 상관없이 가능할듯

    void OnTriggerStay2D(Collider2D col)
    {
        
        if (col.tag == "gacha")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gacha = true;
                col.transform.GetChild(0).GetComponent<singlegacha>().Gacha();
            }

        }
        else if (Input.GetKeyDown(KeyCode.T)) //태그를 필로 하면 모든 필이 다 먹어짐 따라서 재헌이랑 이야기를 해보고 변경이 필요
        {

            if (col.gameObject.tag == "1")
            {
                NinjapetPillsEatPill = true;
            }
            else if (col.gameObject.tag == "2")
            {
                KitpetEatPill = true;
            }
            else if (col.gameObject.tag == "3")
            {
                DronpetEatPill = true;
            }
        }
        
    }
}



