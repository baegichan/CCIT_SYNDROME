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

    public void Attack()
    {
        Debug.Log("因維 2腰");
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TestTest.IsCharging = true;
            wolf = WolfGauge();
            StartCoroutine(wolf);
            Debug.Log("朝五馬五,,,,,,,,");
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            TestTest.IsCharging = false;
            StopAllCoroutines();
            Debug.Log(TestTest.h);
            TestTest.rigid.AddForce(new Vector2(TestTest.h * 4, 0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
            Debug.Log("馬たたたたたたた");
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
            GetComponentInParent<TestTest>().P_JumpInt = GetComponentInParent<TestTest>().P_MaxJumpInt;
        }
    }
}
