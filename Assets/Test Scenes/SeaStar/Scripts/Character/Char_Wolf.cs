using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Wolf : MonoBehaviour
{
    public int[] HP;
    public int DP;
    public Char_Parent CP;
    public AbilityManager AM;
    public float WereWolf_Gauge = 0;
    float WereWolf_Max = 5;
    public int power;
    public Animator Ani;
    public bool P_Attack_State;
    public float P_AttackMoveInt;
    public GameObject Wolf_GageBar;

    void OnEnable()
    {
        CP.Hp_Current += HP[CP.ActiveAbility.Enhance];
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Char_Parent.ShopOn == false)
        {
            if (CP.Ani.GetBool("Jump") == false)
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
            AM.AS.PlayOneShot(SoundManager.instance.EFXs[7].Audio);
            Wolf_GageBar.SetActive(true);
            Ani.SetBool("Dash", true);
            Ani.SetBool("CanIThis", false);
            WereWolf_Gauge = Time.deltaTime;
            Wolf_GageBar.GetComponent<WolfGage>().WolfDashGage = WereWolf_Gauge;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Wolf_GageBar.SetActive(false);
            Ani.SetBool("Dash", false);
            Ani.SetBool("CanIThis", true);
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h * 4, 0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
            Wolf_GageBar.GetComponent<WolfGage>().WolfDashGage = WereWolf_Gauge;
        }
    }

    void AttackStart()
    {
        P_Attack_State = true;
    }
    void AttackEnd()
    {
        P_Attack_State = false;
        Ani.SetBool("CanIThis", true);
    }
}
