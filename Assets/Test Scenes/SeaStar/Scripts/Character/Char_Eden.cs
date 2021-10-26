using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Eden : MonoBehaviour
{
    public int HP;
    public int DP;
    public float P_DashForce;
    float P_DashInt = 10;
    float P_DashTimer = 2;

        public float P_AttackForce;
    public int P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_AttackState = false;
    public float P_AttackResetTimer;

    Animator Ani;

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (P_AttackInt <= 2)
            {
                P_AttackInt++;
            }
        }

        float Reset = 1;

        switch (P_AttackInt)
        {
            case 0:

                P_AttackState = false;
                P_AttackResetTimer = Reset;
                break;

            case 1:

                P_AttackState = true;
                Ani.SetInteger("AttackInt", 1);
                Ani.SetBool("AttackState", true);

                P_AttackResetTimer -= Time.deltaTime;

                if (P_AttackResetTimer <= 0)//공격하다가 중간에 멈추면 다시 1타로 초기화.
                {
                    P_AttackInt = 0;
                    P_AttackResetTimer = Reset;
                }
                if (P_AttackState == true)
                {

                }
                break;

            case 2:

                P_AttackState = true;
                Ani.SetInteger("AttackInt", 2);
                Ani.SetBool("AttackState", true);

                P_AttackResetTimer -= Time.deltaTime;

                if (Input.GetMouseButtonDown(0))
                {
                    P_AttackResetTimer = Reset;
                }
                if (P_AttackResetTimer <= 0)
                {
                    P_AttackInt = 0;
                    P_AttackResetTimer = Reset;
                }
                P_AttackState = true;
                if (P_AttackState == true)
                {

                }
                break;

            case 3:

                Ani.SetInteger("AttackInt", 3);
                Ani.SetBool("AttackState", false);

                P_AttackResetTimer -= Time.deltaTime;

                if (P_AttackResetTimer <= 0)
                {
                    P_AttackInt = 0;
                }
                P_AttackState = false;


                break;
        }

    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TestTest.rigid.AddForce(new Vector2(TestTest.h, 1) * P_DashForce * 2);
            P_DashInt = 0;
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
            P_DashForce = 100;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<TestTest>().P_JumpInt = GetComponentInParent<TestTest>().P_MaxJumpInt;
        }
    }
}
