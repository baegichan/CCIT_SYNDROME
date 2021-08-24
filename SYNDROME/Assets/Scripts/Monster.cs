using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float HP;
    public static float MonsterPower;
    void Start()
    {
        HP = 100000;
        MonsterPower = 20;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(HP);

        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "player")
        {
            Player.Hp -= 15;
        }
    }
    public void dameged(int dmg)
    {
        HP -= dmg;
    }
}
