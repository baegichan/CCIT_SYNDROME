using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublejump : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.tag == "Player")
        {
            PlayerMovement.JumpPoint = 2;
            Destroy(this.gameObject);
            
        }
    }
}
