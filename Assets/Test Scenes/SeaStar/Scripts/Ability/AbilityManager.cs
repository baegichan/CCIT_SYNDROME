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
    public int RockAP;
    public int[] ParaoAP = { 20, 30, 40, 50};
    public int[] BoomAP = { 10, 13, 16, 20 };
    public int[] WolfAP = { 2, 3, 4, 5 };

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
        if (Input.GetMouseButtonDown(1) && !CP.Ani.GetBool("Jump") && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            CP.Ani.SetTrigger("Ability");
            CP.Ani.SetBool("CanIThis", false);
            Vector2 pp = py.transform.position + new Vector3(0.5f,0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Monster")
                {
                    if (hit[i].GetComponent<Character>().Hp_Current < WolfAP[CP.ActiveAbility.Enhance])
                    {
                        CP.Hp_Current++;
                    }
                    hit[i].GetComponent<Character>().Damage(WolfAP[CP.ActiveAbility.Enhance], CP.UseApPostion);
                }
            }
        }
    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
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
        Collider2D[] MonsterCol = Physics2D.OverlapBoxAll(pp, new Vector2(5, 5), 0, Physics2D.AllLayers);
        for (int i = 0; i < MonsterCol.Length; i++)
        {
            if (MonsterCol[i].tag == "Monster")
            {
                Vector2 enemy = new Vector2(MonsterCol[i].transform.position.x, MonsterCol[i].transform.position.y) - pp;
                RaycastHit2D Hit = Physics2D.Raycast(pp, enemy);
                if (Hit.transform.tag == "Monster")
                {
                    Hit.transform.GetComponent<Character>().Damage(ParaoAP[CP.ActiveAbility.Enhance], CP.UseApPostion);
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
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            A_Attack();
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetMouseButtonUp(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
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
        if (Input.GetMouseButtonDown(1))
        {
            EvilSword_Attack();
            if(Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
            {
                py.GetComponent<Char_Eden>().P_CombatInt = 0;
                EA.SetTrigger("First");
            }
            else if(Char_Parent.Active_Cool < Char_Parent.Active_Cool_Max)
            {
                EA.SetTrigger("Attack");
            }
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetMouseButtonUp(1))
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
