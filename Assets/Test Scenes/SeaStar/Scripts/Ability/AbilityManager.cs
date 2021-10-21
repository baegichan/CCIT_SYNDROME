using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Camera camera;
    public GameObject py;
    public GameObject Bomb;
    public float WereWolf_Gauge = 0;
    Rigidbody2D rg;
    public int RockAP;
    public int[] ParaoAP = { 20, 30, 40, 50};
    public int[] BoomAP = { 10, 13, 16, 20 };
    public int[] WolfAP = { 2, 3, 4, 5 };

    void Start()
    {

    }

    public void Werewolf()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("¾ä¾ä");
            Vector2 pp = py.transform.position + new Vector3(0.5f,0);
            Collider2D[] hit = Physics2D.OverlapBoxAll(pp, new Vector2(1, 1), 0, Physics2D.AllLayers);
            for(int i = 0; i < hit.Length; i++)
            {
                if (hit[i].tag == "enemy")
                {
                    if (hit[i].GetComponent<Character>().Hp_Current < WolfAP[py.GetComponent<TestTest>().ActiveAbility.Enhance])
                    {
                        py.GetComponentInParent<Character>().Hp_Current++;
                        Debug.Log("ÂÁÂÁ");
                    }
                    Character.Damage(hit[i].gameObject, WolfAP[py.GetComponent<TestTest>().ActiveAbility.Enhance]);
                }
            }
        }
    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 pp = py.transform.position;
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
                        Debug.Log(Hit.transform.name + "ÀÌ(°¡) ÇÇÇØ¸¦ ¹ÞÀ½");
                        Character.Damage(Hit.transform.gameObject, ParaoAP[py.GetComponent<TestTest>().ActiveAbility.Enhance]);
                    }
                }
            }

        }
    }

    public void BomberMan()
    {
        if (Input.GetMouseButtonDown(1))
        {
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
        if(ShieldCool > 0) { ShieldCool -= Time.deltaTime; }
        else if(ShieldCool <= 0) { py.GetComponentInParent<Character>().Shield = 0; }

            if (Input.GetMouseButtonDown(1))
        {
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(py.transform.position + new Vector3(0.5f, 0), new Vector2(1, 1) * 0.5f);
    }

    public void Ability_E()
    {
        Debug.Log("E");
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
