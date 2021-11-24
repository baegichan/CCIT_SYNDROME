using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_Foot : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<Char_Wolf>().Ani.SetBool("Jump", false);
            GetComponentInParent<Char_Parent>().P_JumpInt = GetComponentInParent<Char_Parent>().P_MaxJumpInt;
        }
    }
}
