using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_RockMan : MonoBehaviour
{
    public int HP;
    public int[] DP;
    public float P_DashForce;
    float P_DashInt = 10;
    float P_DashTimer = 8;
    public Animator Ani;

    public void Attack()
    {
        Debug.Log("???? 1??");
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 1) * P_DashForce * 2);
            P_DashInt = 0;
        }

        if (P_DashInt == 0)
        {
            P_DashForce = 0;
            P_DashTimer -= Time.deltaTime;
            Physics2D.IgnoreLayerCollision(10, 11);
        }
        if (P_DashTimer <= 0)
        {
            P_DashTimer = 2;
            P_DashInt = 1;
            P_DashForce = 100;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<Char_Parent>().P_JumpInt = GetComponentInParent<Char_Parent>().P_MaxJumpInt;
        }
    }
}
