using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponpickup : MonoBehaviour
{
    public GameObject gamemaster;
    // Start is called before the first frame update
    private void Start()
    {
       gamemaster = GameObject.Find("GameManager");
    }
    private void OnTriggerStay2D(Collider2D col)
    {


        if (col.tag == "Player")
        {
            
            if (gamemaster != null && Input.GetKey(settingmanager.GM.comunication))
            {

                gamemaster.GetComponent<weaponpickler>().pickup();
                // col.GetComponent<PlayerMovement>().pickupweapon();
                col.GetComponent<PlayerMovement>().pickupweapon();
                Destroy(this.gameObject);
            }

        }
    }
 
}
