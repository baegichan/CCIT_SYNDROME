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

    //����
    public float E_Attack_Damage;
    public float E_Attack_Int = 0;
    public float E_Attack_Range = 100;
    public float E_ResetTimer = 0.8f;
    public static Transform Spawn;
    //
    //��������
    public int A_Int;
    public static bool A_Attack_State = false;
    //

    public GameObject PharaoEffect;
    public Animator EA;

    public void Werewolf()
    {
        if (Input.GetMouseButtonDown(1) && !py.GetComponentInParent<Char_Parent>().Ani.GetBool("Jump") && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
            py.GetComponentInParent<Char_Parent>().Ani.SetBool("CanIThis", false);
            Vector2 pp = py.transform.position + new Vector3(0.5f,0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Monster")
                {
                    if (hit[i].GetComponent<Character>().Hp_Current < WolfAP[py.GetComponent<Char_Parent>().ActiveAbility.Enhance])
                    {
                        py.GetComponentInParent<Character>().Hp_Current++;
                    }
                    py.GetComponentInParent<Character>().Damage(hit[i].gameObject, WolfAP[py.GetComponent<Char_Parent>().ActiveAbility.Enhance]);
                }
            }
        }
    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            py.GetComponentInParent<Char_Parent>().PharaoWandSwitch();
            py.GetComponent<Char_Eden>().active = Pharao;
            py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
            py.GetComponentInParent<Char_Parent>().Ani.SetBool("Combat", true);
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
                    py.GetComponentInParent<Character>().Damage(Hit.transform.gameObject, ParaoAP[py.GetComponentInParent<Char_Parent>().ActiveAbility.Enhance]);
                }
            }
        }
    }

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
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
        else if(ShieldCool <= 0) { py.GetComponentInParent<Character>().Shield = 0; }

        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
            Vector2 pp = py.transform.position + new Vector3(0.5f, 0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "Monster")
                {
                    ShieldCool = 4;
                    if (i < 4)
                    {
                        py.GetComponentInParent<Character>().Shield += 10;
                    }
                    py.GetComponentInParent<Character>().Damage(hit[i].gameObject, RockAP);
                }
            }
        }
    }

    public void BattleAxe()
    {
        py.GetComponentInParent<Char_Parent>().Ani.SetInteger("AbilityNum", 4);
        if (Input.GetMouseButtonDown(1))
        {
            A_Attack();
            py.GetComponent<Char_Eden>().P_CombatInt = 1;
        }
        if (Input.GetMouseButtonUp(1))
        {
            py.GetComponent<Char_Eden>().P_CombatInt = 0;
            py.GetComponent<Char_Eden>().P_CombatTimer = 5;
        }
    }
    public void A_Attack()
    {
        py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
        py.GetComponentInParent<Char_Parent>().Ani.SetBool("CanIThis", false);
        py.GetComponentInParent<Char_Parent>().Ani.SetBool("Combat", true);
    }

    public void Ability_E()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EvilSword_Attack();
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
        py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
        py.GetComponentInParent<Char_Parent>().Ani.SetInteger("AbilityNum", 5);
        py.GetComponentInParent<Char_Parent>().Ani.SetBool("Combat", true);
        EA.SetTrigger("Attack");
    }

    public GameObject B_Ball;
    public float B_Damage;
    public float B_Speed;
    //�����Ȱ��ɷ�
    public void BlackSmoke()
    {
        if (Input.GetMouseButtonDown(1) && Char_Parent.Active_Cool >= Char_Parent.Active_Cool_Max)
        {
            Char_Parent.Active_Cool = 0f;
            py.GetComponentInParent<Char_Parent>().Ani.SetTrigger("Ability");
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
