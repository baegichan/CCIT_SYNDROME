using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerM : MonoBehaviour
{
    public float P_Hp;
    public float P_M_Speed;
    public float P_JumpForce;


    Rigidbody2D rigid;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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

    public void Move()
    {
        float h = Input.GetAxis("Horizontal");

        transform.position += new Vector3(h * P_M_Speed * Time.deltaTime, 0);
    }
    public void Jump()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigid.AddForce(Vector3.up * P_JumpForce);
        }
    }
}
