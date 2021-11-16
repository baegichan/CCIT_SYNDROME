using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalChaild : MonoBehaviour
{
    //Test
    public int HP;
    public int DP;
    //

    //공격
    public float P_AttackForce;
    public int P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_Attack_State = false;
    public float P_AttackResetTimer;
    public float P_CombatTimer= 5;
    public float P_CombatInt;
    //
    //대쉬
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (P_AttackInt <= 2)
        //    {
        //        P_AttackInt++;
        //    }
        //}

        //float Reset = 1;

        //switch (P_AttackInt)
        //{
        //    case 0:

        //        P_AttackState = false;
        //        P_AttackResetTimer = Reset;
        //        break;

        //    case 1:

        //        P_AttackState = true;
        //        Ani.SetInteger("AttackInt", 1);
        //        Ani.SetBool("AttackState", true);

        //        P_AttackResetTimer -= Time.deltaTime;

        //        if (P_AttackResetTimer <= 0)//공격하다가 중간에 멈추면 다시 1타로 초기화.
        //        {
        //            P_AttackInt = 0;
        //            P_AttackResetTimer = Reset;
        //        }
        //        if (P_AttackState == true)
        //        {

        //        }
        //        break;

        //    case 2:

        //        P_AttackState = true;
        //        Ani.SetInteger("AttackInt", 2);
        //        Ani.SetBool("AttackState", true);

        //        P_AttackResetTimer -= Time.deltaTime;

        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            P_AttackResetTimer = Reset;
        //        }
        //        if (P_AttackResetTimer <= 0)
        //        {
        //            P_AttackInt = 0;
        //            P_AttackResetTimer = Reset;
        //        }
        //        P_AttackState = true;
        //        if (P_AttackState == true)
        //        {

        //        }
        //        break;

        //    case 3:

        //        Ani.SetInteger("AttackInt", 3);
        //        Ani.SetBool("AttackState", false);

        //        P_AttackResetTimer -= Time.deltaTime;

        //        if (P_AttackResetTimer <= 0)
        //        {
        //            P_AttackInt = 0;
        //        }
        //        P_AttackState = false;


        //        break;
        //}

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
}



