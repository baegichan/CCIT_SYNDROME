using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abduru : MonoBehaviour
{
    public Camera camera;
    public GameObject py;
    public GameObject Bomb;
    public float WereWolf_Gauge = 0;
    Rigidbody2D rg;
    public int RockAP;
    public int[] ParaoAP = { 20, 30, 40, 50 };
    public int[] BoomAP = { 10, 13, 16, 20 };
    public int[] WolfAP = { 2, 3, 4, 5 };

    //¸¶°Ë
    public bool E_Attack_State = false;
    //
    //ÀüÅõµµ³¢
    public int A_Int;
    public static bool A_Attack_State = false;  
    //
    public Animator EA;
    public GameObject PharaoEffect;

    public void Werewolf()
    {
        if (Input.GetMouseButtonDown(1) && !py.GetComponentInParent<TestPlayer>().Ani.GetBool("Jump") )
        {

            py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Ability");
            py.GetComponentInParent<TestPlayer>().Ani.SetBool("CanIThis", false);
            Debug.Log("¾ä¾ä");
            Vector2 pp = py.transform.position + new Vector3(0.5f, 0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "enemy")
                {
                    if (hit[i].GetComponent<Character>().Hp_Current < WolfAP[py.GetComponent<TestPlayer>().ActiveAbility.Enhance])
                    {
                        py.GetComponentInParent<Character>().Hp_Current++;
                        Debug.Log("ÂÁÂÁ");
                    }
                    Character.Damage(hit[i].gameObject, WolfAP[py.GetComponent<TestPlayer>().ActiveAbility.Enhance]);
                }
            }
        }
    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1) )
        {
            py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Ability");
            py.GetComponentInParent<TestPlayer>().Ani.SetBool("Combat", true);
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
            if (MonsterCol[i].tag == "enemy")
            {
                Vector2 enemy = new Vector2(MonsterCol[i].transform.position.x, MonsterCol[i].transform.position.y) - pp;
                //enemy = enemy.normalized;
                RaycastHit2D Hit = Physics2D.Raycast(pp, enemy);
                Debug.DrawRay(pp, enemy, Color.green);
                Debug.Log(Hit.transform.name);
                if (Hit.transform.tag == "enemy")
                {
                    Debug.Log("ÆÄ¶ó¿Ë!#@!!@!@$@#@");
                    Character.Damage(Hit.transform.gameObject, ParaoAP[py.GetComponentInParent<TestPlayer>().ActiveAbility.Enhance]);
                }
            }
        }
    }

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1) )
        {
            
            py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Ability");
            Debug.Log("ÆøÅºÆøÅº");
            Vector2 pp = py.transform.position;
            Vector2 CursorPos = Input.mousePosition;
            CursorPos = camera.ScreenToWorldPoint(CursorPos);
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
        Debug.Log("¹ÙÀ§Ã³·³ ´Ü´ÜÇÏ°Ô,,,,");
        if (ShieldCool > 0) { ShieldCool -= Time.deltaTime; }
        else if (ShieldCool <= 0) { py.GetComponentInParent<Character>().Shield = 0; }

        if (Input.GetMouseButtonDown(1) )
        {
            
            py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Ability");
            Debug.Log("ÅäÅ÷!! ´ëÁöµ¿ÇÙ!!!!!");
            Vector2 pp = py.transform.position + new Vector3(0.5f, 0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "enemy")
                {
                    ShieldCool = 4;
                    if (i < 4)
                    {
                        py.GetComponentInParent<Character>().Shield += 10;
                        Debug.Log("½¯µå È¹µæ");
                    }
                    Character.Damage(hit[i].gameObject, RockAP);
                }
            }
        }
    }

    public void BattleAxe()
    {
        py.GetComponentInParent<TestPlayer>().Ani.SetInteger("AbilityNum", 4);
        if (Input.GetMouseButtonDown(1))
        {
            A_Attack();
            py.GetComponent<MentalChaild>().P_CombatInt = 1;
        }
        if(Input.GetMouseButtonUp(1))
        {
            py.GetComponent<MentalChaild>().P_CombatInt = 0;
            py.GetComponent<MentalChaild>().P_CombatTimer = 5;
        }
    }
    public void A_Attack()
    {       
        py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Abililty");
        py.GetComponentInParent<TestPlayer>().Ani.SetBool("CanIThis", false);
        py.GetComponentInParent<TestPlayer>().Ani.SetBool("Combat", true);   
    }
       
    public void Ability_E()
    {
        if (Input.GetMouseButtonDown(1))
        {
            EvilSword_Attack();     
            py.GetComponent<MentalChaild>().P_CombatInt = 1;
        }  
        if(Input.GetMouseButtonUp(1))
        {
            py.GetComponent<MentalChaild>().P_CombatInt = 0;
            py.GetComponent<MentalChaild>().P_CombatTimer = 5;
        }     
    }

    public void EvilSword_Attack()
    {
        py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Abililty");
        py.GetComponentInParent<TestPlayer>().Ani.SetInteger("AbilityNum", 5);
        py.GetComponentInParent<TestPlayer>().Ani.SetBool("Combat", true);
        EA.SetTrigger("Attack");
    }

    public GameObject B_Ball;
    public float B_Damage;
    public float B_Speed;
    //°ËÀº¾È°³´É·Â
    public void BlackSmoke()
    {
        if (Input.GetMouseButtonDown(1) )
        { 
            py.GetComponentInParent<TestPlayer>().Ani.SetTrigger("Ability");
            Debug.Log("ºÎ¶óÄí ½º¸ðÄí!!!");
            Vector2 MouseP = Input.mousePosition;
            MouseP = camera.ScreenToWorldPoint(MouseP);
            Vector2 Point = py.transform.position;
            Vector2 Dir = MouseP - Point;
            Dir = Dir.normalized;

            GameObject BB = Instantiate(B_Ball, Point, Quaternion.identity);
            BB.GetComponent<Smoke_>().Dir = Dir;
            BB.GetComponent<Smoke_>().PP = Point;
        }
    }

    public void Ability_F()
    {
        Debug.Log("F");
    }

    public void Double_Jump()
    {
        Debug.Log("G");
    }
    public void Change_Jump_int()
    {
        //pp = py.GetComponent<PlayerM_>();
        //if (pp.PassiveAbility.AbCode == 6) { pp.P_MaxJumpInt = 2; }
        //else { pp.P_MaxJumpInt = 1; }
    }
}