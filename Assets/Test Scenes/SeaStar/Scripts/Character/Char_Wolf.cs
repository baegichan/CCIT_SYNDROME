using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Wolf : MonoBehaviour
{
    public int[] HP;
    public int DP;
    public float WereWolf_Gauge = 0;
    public int power;
    IEnumerator wolf;
    public Animator Ani;
    public bool P_Attack_State;
    public float P_AttackMoveInt;

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Char_Parent.ShopOn == false)
        {
            if (GetComponentInParent<Char_Parent>().Ani.GetBool("Jump") == false)
            {
                Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 0) * (P_AttackMoveInt * 5), ForceMode2D.Impulse);
            }
            Ani.SetTrigger("Attack");
            Ani.SetBool("CanIThis", false);
        }
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Ani.SetBool("Dash", true);
            Ani.SetBool("CanIThis", false);
            wolf = WolfGauge();
            StartCoroutine(wolf);
            Ani.SetBool("CanIThis", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Ani.SetBool("Dash", false);
            StopAllCoroutines();
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h * 4, 0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
        }
    }

    IEnumerator WolfGauge()
    {
        yield return new WaitForSeconds(0.5f);
        if (WereWolf_Gauge < 5) { WereWolf_Gauge += 1; }
        StartCoroutine(WolfGauge());
    }

    void AttackStart()
    {
        P_Attack_State = true;
    }
    void AttackEnd()
    {
        P_Attack_State = false;
    }
}
