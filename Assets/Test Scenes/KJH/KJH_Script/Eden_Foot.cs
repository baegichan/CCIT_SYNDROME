using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eden_Foot : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<MentalChaild>().Ani.SetBool("Jump", false);
            GetComponentInParent<TestPlayer>().P_JumpInt = GetComponentInParent<TestPlayer>().P_MaxJumpInt;
        }
    }
}
