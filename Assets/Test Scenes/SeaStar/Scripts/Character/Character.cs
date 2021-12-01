using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [Tooltip("보스 몬스터 여부")]
    public bool IsBoss;

    void Update()
    {
        if(Hp_Current > Hp_Max) { Hp_Current = Hp_Max; }
        if(Shield < 0) { Shield = 0; }
    }

    public void Damage(GameObject Defender, int DamageValue) //(맞는 애, 데미지값)
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
        Load_Damage_Text(Defender.GetComponent<Character>(),DamageValue);
    }

    /* 데미지 예시 (몬스터랑 플레이어랑 닿은 상황)
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
        GameObject Text = (GameObject)Instantiate(Resources.Load("HEALCANVAS"), target.transform.position + Vector3.up * 1+ new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
        Text.GetComponent<DamageOBJ>().HealText(Healint);


        float fontExtra = Mathf.Clamp(Healint / 3, 5.0f, 10.0f);
        float fontsize = Random.Range(0.8f * fontExtra, 1.0f * fontExtra);
        Text.GetComponent<Text>().fontSize = (int)fontsize;
    }
    public  void Load_Damage_Text(Character target,int Damage)
    { 
        if(transform.tag == "Player" && AbyssManager.abyss != null) { AbyssManager.abyss.GetAbyssGage(10); }
        GameObject Text = null;
      
            Text = (GameObject)Instantiate(Resources.Load("DMGCANVAS"), target.transform.position + Vector3.up * 1 + new Vector3(Random.Range(0.0f, 0.9f), Random.Range(0.0f, 0.3f), 0), Quaternion.identity);
       
   
        Text.GetComponent<DamageOBJ>().DamageText(Damage);
        float fontExtra = Mathf.Clamp(Damage / 3, 5.0f, 10.0f);
        float fontsize = Random.Range(0.8f * fontExtra, 1.0f * fontExtra);
        Text.GetComponent<Text>().fontSize = (int)fontsize;

    }
    public void Damage(int DamageValue)
    {
        int firstDamge = DamageValue;
        if (DamageValue > 20)
        {
            CameraShake.Cam_instance.Shake(0.1f, 0.4f);
        }
        int secondDamge = firstDamge - Shield;
        if (secondDamge > 0)
        {
            Hp_Current -= secondDamge - DP;

        }
        if (Shield > 0)
            Shield -= firstDamge - DP;

        if (transform.tag == "Player")
        {
            GetComponent<Char_Parent>().Special_Load_Damage_Text(DamageValue);
            StateManager.state.Hp = Hp_Current;
        }
        else
        {
            Load_Damage_Text(this, DamageValue);
        }
    }


    public void Damage(int DamageValue, bool IsBuffOn, GameObject HitEffect)
    {
        Instantiate(HitEffect, transform.position, Quaternion.identity);
        int firstDamage = IsBuffOn ? DamageValue + Mathf.RoundToInt(DamageValue * 0.2f) : DamageValue;
        if (DamageValue > 20)
        {
           //CameraShake.Cam_instance.Shake(70, 0.4f);
        }
        int secondDamge = firstDamage - Shield;
        if (secondDamge > 0)
        {
            Hp_Current -= secondDamge - DP;
        }
        if (Shield > 0)
            Shield -= firstDamage - DP;
        AbyssManager.abyss.abyssGage++;
        Load_Damage_Text(this, firstDamage);
    }

    public void Damage(int DamageValue, bool IsBuffOn)
    {
        int firstDamage = IsBuffOn ? DamageValue + Mathf.RoundToInt(DamageValue * 0.2f) : DamageValue;
        if (DamageValue > 20)
        {
            //CameraShake.Cam_instance.Shake(70, 0.4f);
        }
        int secondDamge = firstDamage - Shield;
        if (secondDamge > 0)
        {
            Hp_Current -= secondDamge - DP;
        }
        if (Shield > 0)
            Shield -= firstDamage - DP;
        AbyssManager.abyss.abyssGage++;
        Load_Damage_Text(this, firstDamage);
        if (tag == "Monster")
        {
            gameObject.GetComponent<Sound_P>().HitSound();
        }
    }

    public void KnuckBack(Transform Attacker, float Power, bool IsBoss)
    {
        if (!IsBoss)
        {
            if (Attacker.position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * Power, ForceMode2D.Impulse);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
            else if (Attacker.position.x > transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * Power, ForceMode2D.Impulse);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
        }
    }

    public void PlayerKnuckBack(Transform Hit, float Power, bool IsBoss)
    {
        if(!IsBoss)
        {
            if (Hit.position.x > transform.position.x)
            {
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Power, ForceMode2D.Impulse);
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
            else if (Hit.position.x < transform.position.x)
            {
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Power, ForceMode2D.Impulse);
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
        }
    }

    public void PlayerKnuckBack(Transform Attacker, Transform Hit, float Power, bool IsBoss)
    {
        if (!IsBoss)
        {
            if (Hit.position.x > Attacker.position.x)
            {
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.right * Power, ForceMode2D.Impulse);
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
            else if (Hit.position.x < Attacker.position.x)
            {
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.left * Power, ForceMode2D.Impulse);
                Hit.GetComponent<Rigidbody2D>().AddForce(Vector2.up * Power / 3, ForceMode2D.Impulse);
                //GetComponent<MonsterColorChanger>().Damaged();
            }
        }
    }
}
