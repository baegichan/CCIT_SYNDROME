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
        //Load_Damage_Text(Defender.GetComponent<Character>(),DamageValue);
    }

    /* ������ ���� (���Ͷ� �÷��̾�� ���� ��Ȳ)
    void OnCollisionEnter2D(Collision2D col)
    {
        Damage(col.gameObject, AP);
    }
    */
    public  void Heal(Character target , int Healint)
    {
        if(target.Hp_Current>0 && target.Hp_Max>target.Hp_Current)
        {
            target.Hp_Current = Mathf.Clamp(target.Hp_Current+Healint, 0, target.Hp_Max);
            Load_Heal_Text(target,Healint);
        }
    }
    public  void Load_Heal_Text(Character target,int Healint)
    {
        GameObject Text = (GameObject)Instantiate(Resources.Load("DamageObj"), target.transform.position + Vector3.up * 3+ new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
        Text.GetComponent<DamageOBJ>().HealText(Healint);
    }
    public  void Load_Damage_Text(Character target,int Damage)
    {
        GameObject Text = (GameObject)Instantiate(Resources.Load("DamageObj"), target.transform.position + Vector3.up * 3+ new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
        Text.GetComponent<DamageOBJ>().DamageText(Damage);
    }
    public void Damage( int DamageValue) 
    {
        int firstDamge = DamageValue;
        int secondDamge = firstDamge - Shield;
        if (secondDamge > 0)
        {
            Hp_Current -= secondDamge - DP;
        }       
        if (Shield > 0)
           Shield -= firstDamge - DP;
        Load_Damage_Text(this,DamageValue);
    }
}
