using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAxe : MonoBehaviour
{
    //Battle Axe
    public int A_Int;
    float A_Damage;
    bool A_AttackState = false;
    public float A_ResetTimer;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void A_Attack()
    {
        
        float Reset = 10;
        switch (A_Int)
        {
            case 0:
                A_AttackState = false;
                A_Damage = 0;
                break;

            case 1:
                A_AttackState = true;
                A_Damage = 100;//임시
                A_ResetTimer -= Time.deltaTime;
                if(A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 2:
                A_AttackState = true;
                A_Damage = 100;
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 3:
                A_AttackState = true;
                A_Damage = 100;
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 4:A_AttackState = true;
                A_AttackState = true;
                A_Damage = 150;//임시
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;




        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("맞");
            if (col.gameObject.tag == "Monster")//임시
            {
                Debug.Log("았");
                if (A_Int <= 4)
                {
                    Debug.Log("다");
                    A_Int++;
                }
            }
        }
        
    }
}
