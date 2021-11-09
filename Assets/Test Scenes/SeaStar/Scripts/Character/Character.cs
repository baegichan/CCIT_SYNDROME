using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("���� �������ͽ�")]
    [Tooltip("�ִ� ü��")]
    public int Hp_Max;
    [Tooltip("���� ü��")]
    public int Hp_Current;
    [Tooltip("����")]
    public int Shield;
    [Tooltip("���ݷ�")]
    public int AP;
    [Tooltip("����")]
    public int DP;
    [Tooltip("�⺻ �̵��ӵ�")]
    public float speed;

    void Update()
    {
        if(Hp_Current > Hp_Max) { Hp_Current = Hp_Max; }
        if(Shield < 0) { Shield = 0; }
    }

    public static void Damage(GameObject Defender, int DamageValue) //(�´� ��, ��������)
    {
        int firstDamge = DamageValue;

        //���尡 ���������� ������ üũ
        int secondDamge = firstDamge - Defender.GetComponent<Character>().Shield;

        //���尡 �� ���� �� HP�� ������
        if (secondDamge > 0)
        {
            Defender.GetComponent<Character>().Hp_Current -= secondDamge - Defender.GetComponent<Character>().DP;
        }

        //���忡 ������
        if(Defender.GetComponent<Character>().Shield > 0)
        Defender.GetComponent<Character>().Shield -= firstDamge - Defender.GetComponent<Character>().DP;

    }

    /* ������ ���� (���Ͷ� �÷��̾�� ���� ��Ȳ)
    void OnCollisionEnter2D(Collision2D col)
    {
        Damage(col.gameObject, AP);
    }
    */
}
