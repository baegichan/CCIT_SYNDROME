using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetreturner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] objects;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="enemy")
        {
         
            objects[0] = collision.gameObject;
           
        }
        else if (collision.tag == "Boss")
        {

            objects[0] = collision.gameObject;

        }
    }
}
