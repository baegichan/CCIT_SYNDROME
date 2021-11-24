using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Eden : MonoBehaviour
{
    //Test
    public int HP;
    public int DP;
    //
    [Header("공격")]
    public float P_AttackForce;
    public int P_AttackInt = 0;
    public float P_AttackTimer = 1;
    public bool P_Attack_State = false;
    public float P_AttackResetTimer;
    public float P_CombatTimer = 5;
    public float P_CombatInt;
    [Tooltip("플레이어 공격시 앞으로 움직여지는 변수")]
    public float P_AttackMoveInt;
    //
    [Header("대쉬")]
    public float P_DashForce;
    public float P_DashInt = 1;
    public float P_DashTimer = 5;
    int AnimeInt = 1;
    //

    public Animator Ani;
    Rigidbody2D rigid;

    void Start()
    {
        Ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        BAI();
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && Char_Parent.ShopOn == false)
        {
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 0) * (P_AttackMoveInt * 5) , ForceMode2D.Impulse);
            Ani.SetTrigger("Attack");
            Ani.SetBool("Combat", true);
            Ani.SetBool("CanIThis", false);
            P_CombatTimer = 5;
            P_CombatInt = 1;
            P_Attack_State = true;
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
                P_Attack_State = false;
                Ani.SetBool("Combat", false);
            }
        }
    }
    public Transform Sword;

    void SwordAttack()
    {
        Collider2D[] hit = Physics2D.OverlapBoxAll(Sword.position, new Vector2(1.8f, 1), 0);

        if (P_Attack_State == true)
        {
            foreach (Collider2D Current in hit)
            {
                if (Current.tag == "Monster")
                {
                    CameraShake.Cam_instance.Shake(0.1f, 0.05f);
                    Current.GetComponent<Character>().Damage(GetComponentInParent<Char_Parent>().AP, GetComponentInParent<Char_Parent>().UseApPostion);
                    Current.GetComponent<Character>().KnuckBack(transform, 5);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Sword.position, new Vector2(1.8f, 1));
    }
    public void Event_Eden()
    {
        Ani.SetBool("CanIThis", true);
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (P_DashInt == 0)
            {
                if (AnimeInt == 1)
                {
                    Ani.SetBool("Dash", true);
                    Ani.SetBool("CanIThis", false);
                    AnimeInt = 0;
                }
            }
            Physics2D.IgnoreLayerCollision(10, 11);
            Char_Parent.rigid.AddForce(new Vector2(Char_Parent.h, 0.1f) * P_DashForce * 2);
            Char_Parent.rigid.velocity = new Vector2(0, 0);
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
            P_DashTimer = 5;
            P_DashInt = 1;
            AnimeInt = 1;
            P_DashForce = 300;
            Physics2D.IgnoreLayerCollision(10, 11, false);
        }
    }
    void DashS()
    {
        Ani.SetBool("CanIThis", false);
    }
    void DashE()
    {
        Ani.SetBool("Dash", false);
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

    void AttackStart()
    {
        P_Attack_State = true;
    }
    void AttackEnd()
    {
        P_Attack_State = false;
    }
    ///

    /// <전투도끼>
    void A_int1()
    {
        AXE.Attack_int = 0;
    }
    void A_int2()
    {
        AXE.Attack_int = 1;

    }
    void A_int3()
    {
        AXE.Attack_int = 2;
    }
    void A_int4()
    {
        AXE.Attack_int = 3;
    }
    void BattleAxeEvent()
    {
        AbilityManager.A_Attack_State = false;
    }

    void CanMoving()
    {
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

    void BattleAxeintInitalization()
    {
       //BAX.GetComponent<BattleAxeAttack>().Attack_int = 0;
        BattleAxeAttack.Attack_int = 0;
        Char_Parent.Active_Cool = 0f;
    }

    void TestBa()
    {
        if(BattleAxeAttack.Attack_int >= 5)
        {
            BattleAxeAttack.Attack_int = 1;
        }
    }

    void BAI()//Battle Axe Attack Int <-- 기계도끼 애니메이션
    {
       Ani.SetInteger("BAI", BattleAxeAttack.Attack_int);
    }
    
    void AxeAttack()
    {
        GetComponentInParent<Char_Parent>().BattleAxe.GetComponent<AXE>().AxeAttack();
    }
    void Transformation_Wolf()
    {
        GetComponentInParent<Char_Parent>().DecideChar();
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;
    }

    void NormalTime()
    {
        Time.timeScale = 1f;
    }
}