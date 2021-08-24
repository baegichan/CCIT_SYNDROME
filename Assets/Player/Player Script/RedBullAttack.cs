using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullAttack : MonoBehaviour
{
    public int RedbullPower = 1000;
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster" && PlayerMovement.FlashInt == 0) 
        {
            Debug.Log("닿음");
            TestMonster.M_Hp -= RedbullPower;
        }
    }
   
     
}
