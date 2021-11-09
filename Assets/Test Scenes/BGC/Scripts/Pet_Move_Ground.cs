using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Move_Ground : MoveModule
{
    // Start is called before the first frame update
    
    GameObject Player;
    Rigidbody2D Rigid;
    public float jumpforce;
    public float jumpcooltime;
    private float jumpcool;
    private float MoveToP=0;
    public float MoveSpeed;
    public float distace;

    public override void Jump()
    {
    if(jumpcool <= 0)
        {
            Rigid.AddForce(new Vector2(0,jumpforce));
            jumpcool = jumpcooltime;
        }
    }
    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigid = GetComponent<Rigidbody2D>();
        Set_Module();
    }
    public override void Move()
    {
        if (Active)
        {
            if (Mathf.Abs(Player.transform.position.x - transform.position.x) > distace)
            {
                MoveToP += Time.deltaTime * MoveSpeed;
                transform.position = new Vector2(Vector2.Lerp(transform.position, Player.transform.position, MoveToP).x, transform.position.y);
                if (MoveToP >= 1)
                {
                    MoveToP = 0;
                }
            }
            if (Player.transform.position.y - transform.position.y > distace)
            {
                Jump();
            }
            else
            {

            }
            jumpcool = Mathf.Clamp(jumpcool - Time.deltaTime, 0, jumpcool);
        }
    }
}
