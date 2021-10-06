using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSword : MonoBehaviour
{
   
    public float E_Attack_Damage;
    public float E_Attack_Int = 0;
    public float E_Attack_Range = 100;
    public float E_ResetTimer = 0.8f;
    public Transform E_Attack_Throw;
    public float E_Move_Speed = 500;
    public float E_Move_SpinSpeed = 500;
    public static Transform Spawn;


    void Start()
    {
        Spawn = GameObject.FindWithTag("Evil Sword Spawn").GetComponent<Transform>();
        //Instantiate(GameObject.FindWithTag("Evil Sword"),Player);
    }

    void Update()
    {
        this.transform.position = Spawn.transform.position;

        Attack();
        ResetAttack();


    }

    public void Attack()
    {
        AttackPlus();
            switch (E_Attack_Int)
            {
                case 0:
                    AttackZero();
                E_ResetTimer = 0.8f;
                E_Attack_Damage = 0;
                    break;

                case 1:
                    AttackOne();
                ResetAttack();
                E_ResetTimer -= Time.deltaTime;

                E_Attack_Damage = 15;
                    break;

                case 2:
                    AttackTwo();
                ResetAttack();
                E_ResetTimer -= Time.deltaTime;

                E_Attack_Damage = 15;
                    break;

                case 3:
                    AttackThree();
                ResetAttack();
                E_ResetTimer -= Time.deltaTime;

                E_Attack_Damage = 30;
                    break;



            }

    }
    public void AttackPlus()
    {
        if(Input.GetMouseButtonDown(0))
        {
            E_Attack_Int++;
        }
       
        if (E_Attack_Int > 3)
        {
            E_Attack_Int = 0;
        }
    }
    public void AttackZero()
    {
        Debug.Log("제로");
        
        this.gameObject.transform.Translate(Vector3.back * 100 * Time.deltaTime, Spawn);
    }

    public void AttackOne()
    {
        Debug.Log("원");
      
        this.gameObject.transform.Translate(Vector3.forward * E_Move_Speed * 500 * Time.deltaTime, E_Attack_Throw);
    }

    public void AttackTwo()
    {
        Debug.Log("투");
        
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180 * E_Move_SpinSpeed);
    }

    public void AttackThree()
    {
        Debug.Log("쓰리");
        
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -180 * E_Move_SpinSpeed);
    }

    public void ResetAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            E_ResetTimer = 0.8f;
        }
            switch (E_Attack_Int)
        {

            case 1:

                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;

            case 2:

                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;

            case 3:

                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;
        }
            
        

        
        

            
     }
        
 

    private void OnTriggerEnter2D(Collider2D col)
    {
      
        if (col.tag == "Monster")
        {
          
        }
    }
}