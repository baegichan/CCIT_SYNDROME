using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public GameObject XPosition;
    public GameObject TurnL;
    public GameObject TurnR;
    public float Speed;
    bool Turn = false;
   
    void Update()
    {
        MovingTile();
    }

    void MovingTile()
    {
        float S = Speed * Time.deltaTime;

        if(Turn == false)
        {          
            transform.position = Vector3.MoveTowards(transform.position, TurnR.transform.position, S);
            if(transform.position == TurnR.transform.position){Turn = true;}
        }
        if (Turn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, TurnL.transform.position, S);
            if (transform.position == TurnL.transform.position){ Turn = false; }
        }
    }
}
