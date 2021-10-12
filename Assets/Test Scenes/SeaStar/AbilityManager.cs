using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public Camera camera;
    public GameObject py;
    public GameObject Bomber;
    public float WereWolf_Gauge = 0;
    IEnumerator wolf;
    Rigidbody2D rg;
    public int power;
    public void Werewolf()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rg = py.GetComponent<Rigidbody2D>();
            wolf = WolfGauge();
            StartCoroutine(wolf);
            Debug.Log("Ä«¸ÞÇÏ¸Þ,,,,,,,,");
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopAllCoroutines();
            rg.AddForce(new Vector2(1,0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
            Debug.Log("ÇÏ¤¿¤¿¤¿¤¿¤¿¤¿¤¿");
        }
    }

    IEnumerator WolfGauge()
    {
        yield return new WaitForSeconds(0.5f);
        if(WereWolf_Gauge < 5) { WereWolf_Gauge += 1; }
        StartCoroutine(WolfGauge());
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
            Debug.Log("Canvas: " + CursorPos);
            CursorPos = camera.ScreenToWorldPoint(CursorPos);
            Debug.Log("WorldPosition " + CursorPos);
            Vector2 Dir = CursorPos - pp;
            Dir = Dir.normalized;
            GameObject Boom = Instantiate(Bomber, pp, Quaternion.identity);
            Boom.gameObject.name = "Bomb";
            Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
            rg.AddForce(Dir * 100000 * Time.deltaTime);
        }
    }

    public void Ability_D()
    {
        Debug.Log("D");
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
