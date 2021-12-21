using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eden_Foot : MonoBehaviour
{
    public Char_Eden CE;
    public Char_Parent CP;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            CE.Ani.SetBool("Jump", false);
            CP.P_JumpInt = CP.P_MaxJumpInt;
        }
    }
}
