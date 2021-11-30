using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Camera cam;
    public GameObject py;
    public GameObject Bomb;
    public float WereWolf_Gauge = 0;
    Rigidbody2D rg;
    public LayerMask TargetLayer;
    public int RockAP;
    public float[] DoubleJumpP = { 5, 10, 15, 20};
    public int[] ParaoAP = { 20, 25, 30, 35};
    public int[] BoomAP = { 10, 13, 16, 20 };
    public int[] WolfAP = { 3, 4, 5, 6 };
    public int[] AxeAP = { 1, 3, 5, 7 };
    public int[] EvilAP = { 6, 9, 12, 15 };
    public GameObject PharaoHitEffect;

    //마검
    public bool E_Attack_State = false;
    //
    //전투도끼
    public int A_Int;
    public static bool A_Attack_State = false;
    //

    public GameObject PharaoEffect;
    public Animator EA;
    public Char_Parent CP;

    public void Werewolf()
    {
        if (Input.GetKeyDown(settingmanager.GM.skillattack) && !CP.Ani.GetBool("Jump") && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            PlayerSkillUI.skill.CoolTime = Char_Parent.Active_Cool_Max;
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            CP.Ani.SetBool("CanIThis", false);
        }
    }

    public void Parao()
    {
        if (Input.GetKeyDown(settingmanager.GM.skillattack) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            PlayerSkillUI.skill.CoolTime = Char_Parent.Active_Cool_Max;
            Char_Parent.Active_Cool = 0f;
            CP.PharaoWandSwitch();
            py.GetComponent<Char_Eden>().active = Pharao;
            CP.Ani.SetTrigger("Ability");
            CP.Ani.SetBool("Combat", true);
        }
    }

    public void Pharao()
    {
        Vector2 pp = py.transform.position;
        Vector2 ppp = new Vector2(py.transform.position.x, py.transform.position.y + 2f);

        Instantiate(PharaoEffect, ppp, Quaternion.identity);
        Physics2D.queriesStartInColliders = false;
        Collider2D[] MonsterCol = Physics2D.OverlapBoxAll(pp, new Vector2(5, 5), 0, TargetLayer);
        for (int i = 0; i < MonsterCol.Length; i++)
        {
            if (MonsterCol[i].tag == "Monster")
            {
                Vector2 enemy = new Vector2(MonsterCol[i].transform.position.x, MonsterCol[i].transform.position.y) - pp;
                RaycastHit2D Hit = Physics2D.Raycast(pp, enemy, TargetLayer);
                if (Hit.transform.tag == "Monster")
                {
                    Hit.transform.GetComponent<Character>().Damage(ParaoAP[CP.ActiveAbility.Enhance], CP.UseApPostion, PharaoHitEffect);
                    GetComponent<Character>().KnuckBack(transform, 0.8f, Hit.transform.GetComponent<Character>().IsBoss);
                }
            }
        }
    }

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            Vector2 pp = py.transform.position;
            Vector2 CursorPos = Input.mousePosition;
            CursorPos = cam.ScreenToWorldPoint(CursorPos);
            Vector2 Dir = CursorPos - pp;
            Dir = Dir.normalized;
            GameObject Boom = Instantiate(Bomb, pp, Quaternion.identity);
            Boom.gameObject.name = "Bomb";
            Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
            rg.AddForce(Dir * 100000 * Time.deltaTime);
        }
    }

    public float ShieldCool;
    public void Ability_D()
    {
        if(ShieldCool > 0) { ShieldCool -= Time.deltaTime; }
        else if(ShieldCool <= 0) { CP.Shield = 0; }

        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            Vector2 pp = py.transform.position + new Vector3(0.5f, 0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Monster")
                {
                    ShieldCool = 4;
                    if (i < 4)
                    {
                        CP.Shield += 10;
                    }
                    hit[i].GetComponent<Character>().Damage(RockAP, CP.UseApPostion);
                }
            }
        }
    }

    public void BattleAxe()
    {
        CP.Ani.SetInteger("AbilityNum", 4);
        if (Input.GetKeyDown(settingmanager.GM.skillattack) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            A_Attack();
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetKeyUp(settingmanager.GM.skillattack) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            py.GetComponent<Char_Eden>().P_CombatInt = 0;
            py.GetComponent<Char_Eden>().P_CombatTimer = 5;
        }
    }
    public void A_Attack()
    {
        CP.Ani.SetTrigger("Ability");
        CP.Ani.SetBool("CanIThis", false);
        CP.Ani.SetBool("Combat", true);
    }

    public void Ability_E()
    {
        if (Input.GetKeyDown(settingmanager.GM.skillattack))
        {
            EvilSword_Attack();
            if(Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
            {
                py.GetComponent<Char_Eden>().P_CombatInt = 0;
                EA.SetTrigger("First");
                EA.SetTrigger("Attack");
            }
            else if(Char_Parent.Active_Cool < Char_Parent.Active_Cool_Max)
            {
                EA.SetTrigger("Attack");
            }
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetKeyUp(settingmanager.GM.skillattack))
        {
            py.GetComponent<Char_Eden>().P_CombatInt = 0;
            py.GetComponent<Char_Eden>().P_CombatTimer = 5;
        }
    }

    public void EvilSword_Attack()
    {
        CP.Ani.SetTrigger("Ability");
        CP.Ani.SetInteger("AbilityNum", 5);
        CP.Ani.SetBool("Combat", true);
    }
    public void Evilst()
    {
        CP.Ani.SetBool("CanIThis", false);
    }

    public void EvilRe()
    {
        CP.Ani.SetBool("CanIThis", true);
    }

    public GameObject B_Ball;
    public float B_Damage;
    public float B_Speed;
    //검은안개능력
    public void BlackSmoke()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            Vector2 MouseP = Input.mousePosition;
            MouseP = cam.ScreenToWorldPoint(MouseP);
            Vector2 Point = py.transform.position;
            Vector2 Dir = MouseP - Point;
            Dir = Dir.normalized;

            GameObject BB = Instantiate(B_Ball, Point, Quaternion.identity);
            BB.GetComponent<Smoke_>().Dir = Dir;
            BB.GetComponent<Smoke_>().PP = Point;
        }
    }

    public void Double_Jump()
    {

    }

    public void Change_Jump_int()
    {
        //pp = py.GetComponent<PlayerM_>();
        //if (pp.PassiveAbility.AbCode == 6) { pp.P_MaxJumpInt = 2; }
        //else { pp.P_MaxJumpInt = 1; }
    }
}
