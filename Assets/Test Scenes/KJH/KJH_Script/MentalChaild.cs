using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalChaild : MonoBehaviour
{
    //공격
    public float P_AttackForce;
    public int P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_AttackState = false;
    public float P_AttackResetTimer;
    //

    Animator Ani;

    void Start()
    {
        Ani = GetComponent<Animator>();
    }

    
    void Update()
    {
        Attack();
    }

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
}
