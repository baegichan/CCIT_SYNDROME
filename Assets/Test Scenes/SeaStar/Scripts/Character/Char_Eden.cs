using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Eden : MonoBehaviour
{
    public int HP;
    public int DP;
    public float P_DashForce;
    float P_DashInt = 10;
    float P_DashTimer = 2;

    public int P_AttackInt = 0;
    public bool P_Attack_State = false;
    public float P_CombatTimer = 5;
    public float P_CombatInt;

    public Animator Ani;

    public void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Char_Parent.rigid.AddForce(new Vector2(TestPlayer.h, 0) * 2500 * Time.deltaTime);
            Ani.SetTrigger("Attack");
            Ani.SetBool("Combat", true);
            Ani.SetBool("CanIThis", false);
            P_CombatTimer = 5;
            P_CombatInt = 1;
        }
        if (Input.GetMouseButtonUp(0))
        {
            P_CombatInt = 0;
        }
        if (P_CombatInt <= 0)
        {
            P_CombatTimer -= Time.deltaTime;
            if (P_CombatTimer <= 0)
            {
                Ani.SetBool("Combat", false);
            }
        }
    }
    public void Event_Eden()
    {
        Ani.SetBool("CanIThis", true);
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Ani.SetBool("Dash", true);
            Physics2D.IgnoreLayerCollision(10, 11);
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 1) * P_DashForce * 2);
            P_DashInt = 0;
            if (Char_Parent.RedBullDash == true)
            {
                Physics2D.IgnoreLayerCollision(10, 11);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { Ani.SetBool("Dash", false); }

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
            Ani.SetBool("Jump", false);
        }
    }

    public void PharaoWandSwitch()
    {
        if (GetComponentInParent<Char_Parent>().PharaoWand_Senaka.activeSelf) { GetComponentInParent<Char_Parent>().PharaoWand_Senaka.SetActive(false); }
        else { GetComponentInParent<Char_Parent>().PharaoWand_Senaka.SetActive(true); }
    }

    public delegate void Active();
    public Active active;
    
    public void UseActive()
    {
        active();
    }

    void BattleAxeEvent()
    {
        AbilityManager.A_Attack_State = false;
        GetComponentInParent<Char_Parent>().Ani.SetBool("CanIThis", true);
    }
    void BattleAxeAttackEvent()
    {
        AbilityManager.A_Attack_State = true;
        GetComponentInParent<Char_Parent>().Ani.SetBool("CanIThis", false);
    }
    void OnBattleAxeSwith()
    {
        GetComponentInParent<Char_Parent>().OnBattleAxe();
    }
    void OffBattleAxeSwith()
    {
        GetComponentInParent<Char_Parent>().OffBattleAxe();
    }
    void BattleAxeSwithMove()
    {
        if (Ani.GetBool("CanIThis") && !GetComponentInParent<Char_Parent>().BattleAxe_Senaka.activeSelf && Ani.GetBool("Move"))
        {
            GetComponentInParent<Char_Parent>().OffBattleAxe();
        }
    }
    void BattleAxeintInitalization()
    {
        BattleAxeAttack.Attack_int = 0;
    }
    void TestBa()
    {
        if (BattleAxeAttack.Attack_int > 4)
        {
            BattleAxeAttack.Attack_int = 1;
        }
    }
    void BAI()//Battle Axe Attack Int <-- 기계도끼 애니메이션
    {
        GetComponentInParent<Char_Parent>().Ani.SetInteger("BAI", BattleAxeAttack.Attack_int);
    }
}
