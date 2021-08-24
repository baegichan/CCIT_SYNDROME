using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : MonoBehaviour
{
    public static int MonsterHp;
    public static float MonsterPower;
    void Start()
    {
        MonsterHp = 100000;
        MonsterPower = 20;
    }

   
    void Update()
    {
        Debug.Log(MonsterHp);

        if(MonsterHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            PlayerMovement.Hp -= 15;
        }
    }
}
