using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitpetpill : MonoBehaviour
{
    public GameObject kitpetpill;
    public GameObject kitpet;
    public GameObject petpos;


    private void Update()
    {
        if (Player.KitpetEatPill == true)
        {
            GameObject kit = Instantiate(kitpet, petpos.transform.position, petpos.transform.rotation);
            Destroy(this.gameObject);
        }
    }
  
        
}
