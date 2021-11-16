using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int D;
    public GameObject YourWeapon;

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (YourWeapon.GetComponent<MentalChaild>().P_Attack_State == true)
        //{
            if (col.tag == "Monster")
            {
                col.GetComponent<Character>().Damage(D);
            }
        //}
    } 
}
