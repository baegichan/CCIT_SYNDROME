using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eden_Foot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<Char_Eden>().Ani.SetBool("Jump", false);
            GetComponentInParent<Char_Parent>().P_JumpInt = GetComponentInParent<Char_Parent>().P_MaxJumpInt;
        }
    }
}
