using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon2 : MonoBehaviour
{
    public int Damage;
    public GameObject YourParent;
    public GameObject Current;

    void OnTriggerEnter2D(Collider2D col)
    {
        Current = col.gameObject;

        if (YourParent.GetComponent<Char_Eden>().P_Attack_State == true)
        {
            if (col.tag == "Monster")
            {
                CameraShake.Cam_instance.Shake(0.1f, 0.005f);
                col.GetComponent<Character>().Damage(Damage);
            }
        }
    }
}
