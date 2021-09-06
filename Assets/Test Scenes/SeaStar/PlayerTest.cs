using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float P_Hp;
    public float P_M_Speed;
    public float P_JumpForce;
    public float P_JumpInt = 1;

    Rigidbody2D rigid;

    public GameObject abilityManager;
    public Ability[] HaveAbility;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Update()
    {
        SwitchAbility();
        Attack();
    }

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");

        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);
    }
    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            switch (P_JumpInt)
            {
                case 1:
                    rigid.AddForce(Vector3.up * P_JumpForce);
                    P_JumpInt -= 1;
                    break;

                case 0:
                    rigid.AddForce(Vector3.up * 0);
                    break;
            }
            rigid.AddForce(Vector3.up * P_JumpForce);
        }
    }

    void SwitchAbility()
    {
        if (Input.GetMouseButtonDown(1))
        { 
            System.Array.Reverse(HaveAbility);
            SelectAbility();
        }
    }

    public delegate void useAbility();
    useAbility ability;

    void SelectAbility()
    {
        AbilityManager AM = abilityManager.GetComponent<AbilityManager>();

        switch(HaveAbility[0].AbCode)
        {
            case 0:
                ability = new useAbility(AM.Ability_A);
                Debug.Log("1");
                break;
            case 1:
                ability = new useAbility(AM.Ability_B);
                Debug.Log("1");
                break;
            case 2:
                ability = new useAbility(AM.Ability_C);
                Debug.Log("1");
                break;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ability();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            P_JumpInt += 1;
        }
    }
}