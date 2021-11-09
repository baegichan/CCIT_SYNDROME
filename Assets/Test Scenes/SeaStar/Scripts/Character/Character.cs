using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("공통 스테이터스")]
    [Tooltip("최대 체력")]
    public int Hp_Max;
    [Tooltip("현재 체력")]
    public int Hp_Current;
    [Tooltip("쉴드")]
    public int Shield;
    [Tooltip("공격력")]
    public int AP;
    [Tooltip("방어력")]
    public int DP;
    [Tooltip("기본 이동속도")]
    public float speed;

    void Update()
    {
        if(Hp_Current > Hp_Max) { Hp_Current = Hp_Max; }
        if(Shield < 0) { Shield = 0; }
    }

    public static void Damage(GameObject Defender, int DamageValue) //(맞는 애, 데미지값)
    {
        int firstDamge = DamageValue;

        //쉴드가 데미지보다 많은지 체크
        int secondDamge = firstDamge - Defender.GetComponent<Character>().Shield;

        //쉴드가 더 적을 시 HP에 데미지
        if (secondDamge > 0)
        {
            Defender.GetComponent<Character>().Hp_Current -= secondDamge - Defender.GetComponent<Character>().DP;
        }

        //쉴드에 데미지
        if(Defender.GetComponent<Character>().Shield > 0)
        Defender.GetComponent<Character>().Shield -= firstDamge - Defender.GetComponent<Character>().DP;

    }

    /* 데미지 예시 (몬스터랑 플레이어랑 닿은 상황)
    void OnCollisionEnter2D(Collision2D col)
    {
        Damage(col.gameObject, AP);
    }
    */
}
