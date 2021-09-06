using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSword : MonoBehaviour
{
    //마검 = Evil Sword
    public float E_Attack_Damage;
    public float E_Attack_Int = 0;
    public float E_Attack_Range = 100;
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
    }

    public void Attack()
    {
        if(Input.GetMouseButton(0))
        {
            E_Attack_Int++;
            switch (E_Attack_Int)
            {
                case 0:
                    AttackZero();
                    E_Attack_Damage = 0;
                    break;

                case 1:
                    AttackOne();
                    E_Attack_Damage = 15;
                    break;

                case 2:
                    AttackTwo();
                    E_Attack_Damage = 15;
                    break;

                case 3:
                    AttackThree();
                    E_Attack_Damage = 30;
                    break;



            }
           if(E_Attack_Int > 3)
            {
                E_Attack_Int = 0;
            }
        }
    }
    public void AttackZero()
    {
        this.gameObject.transform.Translate(Vector3.back * 100 * Time.deltaTime, Spawn);
    }

    public void AttackOne()
    {
        this.gameObject.transform.Translate(Vector3.forward * E_Move_Speed * 500 * Time.deltaTime, E_Attack_Throw);
    }

    public void AttackTwo()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180 * E_Move_SpinSpeed);
    }

    public void AttackThree()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, 0, -180 * E_Move_SpinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Monster")
        {
            //몬스터피 -= E_AttackDamage;
        }
    }
}
