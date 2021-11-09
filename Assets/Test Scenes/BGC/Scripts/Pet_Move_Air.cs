using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Move_Air : MoveModule
{
    
   
    GameObject Player;
    Rigidbody2D Rigid;
    public Vector2 target_distace;
 
    public float Speed;
    private float Point_Move;
    private float Points_Move;
    public void Start()
    {
        Set_Module();
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigid = GetComponent<Rigidbody2D>();
       
        Move();


    }
    public void Update()
    {
        Move_To_Target(); 
       
    }
    public override void Move()
    {
        if (Active)
        {
            Move_To_Target();

        }
    }

    /// <summary>
    /// target = Player.transform.position + target_distace
    /// </summary>
    /// <returns></returns>
    public void Move_To_Target()
    {
        Point_Move += Time.deltaTime*Speed;
        transform.position=Vector2.MoveTowards(transform.position, (Vector2)Player.transform.position + target_distace, Point_Move);
        if (Point_Move >= 1)
        {
            Point_Move = 0;
            
        }
 
        
    }

    public void Patroll_To_Target()
    {
        Points_Move += Time.deltaTime * Speed;
      
        
        if (Points_Move >= 1)
        {
            Points_Move = 0;

        }


    }
    /*
    public IEnumerator Patroll_2_Points(Vector2 P1, Vector2 P2)
    {
        Points_Move += Time.deltaTime * Speed;
        transform.position = Vector2.MoveTowards(transform.position, P2, Points_Move);
        if (Points_Move >= 1)
        {
            Points_Move = 0;
            
            StartCoroutine(Patroll_2_Points(P2, P1));
        }
        else
        {
            yield return new WaitForFixedUpdate();
            StartCoroutine(Patroll_2_Points(P1, P2));
        }
      
    }*/
    public override void Jump()
    {
        //No jump ^^
        throw new System.NotImplementedException();
    }
}
