using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public GameObject XPosition;
    public GameObject TurnL;
    public GameObject TurnR;
    public GameObject Target;
    public float Speed;
    bool Turn = false;

    void Update()
    {
        MovingTile();
    }

    void MovingTile()
    {
        float S = Speed * Time.deltaTime;

        if (Turn == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, TurnR.transform.position, S);
            if (transform.position == TurnR.transform.position) { Turn = true; }
        }
        if (Turn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, TurnL.transform.position, S);
            if (transform.position == TurnL.transform.position) { Turn = false; }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Player")
        {   
            col.transform.parent.SetParent(Target.transform);
        }
       // else { col.gameObject.transform.SetParent(Target.transform); } 
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Target.transform.DetachChildren();
        }
    }
}


