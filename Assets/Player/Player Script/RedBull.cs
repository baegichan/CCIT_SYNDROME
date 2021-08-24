using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBull : MonoBehaviour
{
   public GameObject RedBullCollider;

   

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "Player")
        {

            RedBullCollider.SetActive(true);
            PlayerMovement.RedBullInt = 1;
            Destroy(this.gameObject);

           

        }
        
    }
   

}
