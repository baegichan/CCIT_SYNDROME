using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour
{
    public static float M_Hp = 100;
    public static float M_Power = 20;

    void Start()
    {
        
    }

   
    void Update()
    {
        Debug.Log(M_Hp);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Attack")
        {
            M_Hp -= PlayerMovement.AttackPower;
           
        }
        

        
    }
}
