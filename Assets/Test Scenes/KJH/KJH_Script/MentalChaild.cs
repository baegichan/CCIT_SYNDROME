using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalChaild : MonoBehaviour
{
    //Test
    public int HP;
    public int DP;
    //
    public GameObject BAX;
    [Header("공격")]
    public float P_AttackForce;
    public int P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_Attack_State = false;
    public float P_AttackResetTimer;
    public float P_CombatTimer= 5;
    public float P_CombatInt;
    //
    [Header("대쉬")]
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 5;
    int AnimeInt = 1;
    //

    Animator Ani;
    Rigidbody2D rigid;

    void Start()
    {
        Ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Attack();   
        Dash();
        BattleAxeSwithMove();
        BAI();
    }

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rigid.AddForce(new Vector2(TestPlayer.h, 0) * 2500 * Time.deltaTime);
            Ani.SetTrigger("Attack");
            Ani.SetBool("Combat", true);
            Ani.SetBool("CanIThis", false);
            P_CombatTimer = 5;
            P_CombatInt = 1;
            P_Attack_State = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            P_CombatInt = 0;
           
        }
        if(P_CombatInt == 0)
        {
            P_CombatTimer -= Time.deltaTime;
            if (P_CombatTimer <= 0)
            {
                P_Attack_State = false;
                Ani.SetBool("Combat", false);
            }
        }
    }
    public void Event_Eden()
    {
        Ani.SetBool("CanIThis", true);
    }
   
    public void Dash()
    {              
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (P_DashInt == 0)
            {
                if(AnimeInt == 1)
                {
                    Ani.SetBool("Dash", true);
                    Ani.SetBool("CanIThis", false);
                    AnimeInt = 0;
                }                
            }
            // Ani.SetBool("Dash", true);
            // Ani.SetBool("CanIThis", false);
            Physics2D.IgnoreLayerCollision(10, 11);
                TestPlayer.rigid.AddForce(new Vector2(TestPlayer.h * 4, 1.6f) * P_DashForce);
            TestPlayer.rigid.velocity = new Vector2(0, 0);
                P_DashInt = 0;                             
                if (TestPlayer.RedBullDash == true)
                {
                    Physics2D.IgnoreLayerCollision(10, 11);
                    //Damage
                }            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Ani.SetBool("Dash", false);
        }

        if (P_DashInt == 0)
        {         
            P_DashForce = 0;
            P_DashTimer -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(10, 11);
        }
        if (P_DashTimer <= 0)
        {
            P_DashTimer = 5;
            P_DashInt = 1;
            AnimeInt = 1;
            P_DashForce = 100;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Ground")
        {
            Ani.SetBool("Jump", false);
            GetComponentInParent<TestPlayer>().P_JumpInt = GetComponentInParent<TestPlayer>().P_MaxJumpInt;     
        }
        
    }

    void BattleAxeEvent()
    {
        Abduru.A_Attack_State = false;
        GetComponentInParent<TestPlayer>().Ani.SetBool("CanIThis", true);
    }
    void BattleAxeAttackEvent()
    {
        Abduru.A_Attack_State = true;
        GetComponentInParent<TestPlayer>().Ani.SetBool("CanIThis", false);
    }
    void OnBattleAxeSwith()
    {
        GetComponentInParent<TestPlayer>().OnBattleAxe();
    }
    void OffBattleAxeSwith()
    {
        GetComponentInParent<TestPlayer>().OffBattleAxe();
    }
    void BattleAxeSwithMove()
    {
        if (Ani.GetBool("CanIThis") && !GetComponentInParent<TestPlayer>().BattleAxe_Senaka.activeSelf && Ani.GetBool("Move"))
        {
            GetComponentInParent<TestPlayer>().OffBattleAxe();
        }
    }
    void BattleAxeintInitalization()
    {
       //BAX.GetComponent<BattleAxeAttack>().Attack_int = 0;
       BattleAxeAttack.Attack_int = 0;
    }
    void TestBa()
    {
        if(BattleAxeAttack.Attack_int > 4)
        {
            BattleAxeAttack.Attack_int = 1;
        }
    }
    void BAI()//Battle Axe Attack Int <-- 기계도끼 애니메이션
    {
        GetComponentInParent<TestPlayer>().Ani.SetInteger("BAI", BattleAxeAttack.Attack_int);
    }
    
}