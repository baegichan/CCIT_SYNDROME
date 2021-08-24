using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMan : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Player.Shild += 50;
            Destroy(this.gameObject);
        }
    }
}
