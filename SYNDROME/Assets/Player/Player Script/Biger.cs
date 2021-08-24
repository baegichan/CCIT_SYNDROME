using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biger : MonoBehaviour
{
    
    
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if(col.tag == "Player")
        {
            if(PlayerMovement.PillInt == 1)
            {
                PlayerMovement.BigNum = 1;
                Destroy(this.gameObject);
            }
           
        }
    }
}
