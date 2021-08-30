using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    //플레이어 스텟
    public float P_Hp;

    //
    //움직임 관련
    public float P_M_Speed;
    public float P_JumpForce;
    public float P_JumpInt = 1;
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 2;
    //
    //공격 관련
    public float P_AttackForce;
    public float P_AttackInt;
    public float P_AttackTimer = 1;
    public Transform P_FrontAttack;
    public Transform P_TopAttack;
    //

    Animation ani;
    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animation>();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    public void Move()//Move안에 대쉬까지 만듦
    {

        float h = Input.GetAxis("Horizontal");
        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);

        switch (h)
        {
            case -1:
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    rigid.AddForce(Vector3.left * P_DashForce * 2);
                    P_DashInt = 0;
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rigid.AddForce(Vector3.right * P_DashForce * 2);
                    P_DashInt = 0;
                }
                break;
        }

        if(P_DashInt == 0)
        {
            P_DashForce = 0; 
            P_DashTimer -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(10, 11);
        }
        if(P_DashTimer <= 0)
        {
            P_DashTimer = 2;
            P_DashInt = 1;
            P_DashForce = 150;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
        
    }
    public void Jump()
    {
        if(P_JumpInt > 1)
        {
            P_JumpInt = 1;
        }

        switch (P_JumpInt)
            {
                case 1:

                    if(Input.GetKey(KeyCode.Space))
                {
                    Debug.Log("작동");
                    rigid.AddForce(Vector3.up * P_JumpForce * 100 * Time.deltaTime);
                    P_JumpInt -= 1;
                }

                    break;

               case 0:
                    Debug.Log("작동x");
                    rigid.AddForce(Vector3.up * 0);
                    break;

            }
            

            
        
    }

    public void Attack()
    {
        if (Input.GetKeyDown((KeyCode)settingmanager.GM.nomalattack))
        {

        }
    }

    public void AttackSlow()//공격시 Player 이동속도 조절
    {
        switch (P_AttackInt)
        {
            case 0:
                P_M_Speed = 10;
                break;
            case 1:
                P_M_Speed = 3;
                break;
            case 2:
                P_M_Speed = 3;
                break;
            case 3:
                P_M_Speed = 3;
                break;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
     {
       if (col.gameObject.tag == "Ground")
       {
        Debug.Log("점프 +1");
        P_JumpInt += 1;
       }
     }
}


