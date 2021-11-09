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

    public void Attack()
    {
        Ani.SetTrigger("Attack");
        Ani.SetBool("CanIThis", false);
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Ani.SetBool("Dash", true);
            Ani.SetBool("CanIThis", false);
            wolf = WolfGauge();
            StartCoroutine(wolf);
            Debug.Log("ī���ϸ�,,,,,,,,");
            Ani.SetBool("CanIThis", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("dasfsdfdsfsdf");
            Ani.SetBool("Dash", false);
            StopAllCoroutines();
            Debug.Log(Char_Parent.h);
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h * 4, 0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
            Debug.Log("�Ϥ�������������");
        }
    }

    IEnumerator WolfGauge()
    {
        yield return new WaitForSeconds(0.5f);
        if (WereWolf_Gauge < 5) { WereWolf_Gauge += 1; }
        StartCoroutine(WolfGauge());
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            GetComponentInParent<Char_Parent>().P_JumpInt = GetComponentInParent<Char_Parent>().P_MaxJumpInt;
            Ani.SetBool("Jump", false);
        }
    }
}
