using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    public GameObject parent;
    public GameObject BombEffect;
    public int AP;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject != parent && !coll.GetComponent<Collider2D>().isTrigger)
        {
            Instantiate(BombEffect, transform.position, Quaternion.identity);
            Collider2D[] col = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 3), 0, Physics2D.AllLayers);
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].tag == "Monster")
                {
                    col[i].GetComponent<Character>().Damage(AP, Char_Parent.ply.UseApPostion);
                }
            }
            Destroy(gameObject);
        }
    }
}
