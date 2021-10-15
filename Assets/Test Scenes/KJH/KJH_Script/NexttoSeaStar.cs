using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexttoSeaStar : MonoBehaviour
{
    public List<Ability> AbList = new List<Ability>();
    public Camera camera;
    public GameObject py;
    public GameObject Bomber;
    public float WereWolf_Gauge = 0;
    IEnumerator wolf;
    Rigidbody2D rg;
    public int power = 10;
    //¸¶°Ë
    public float E_Attack_Damage;
    public float E_Attack_Int = 0;
    public float E_Attack_Range = 100;
    public float E_ResetTimer = 0.8f;   
    public static Transform Spawn;
    //
    //ÀüÅõµµ³¢
    public int A_Int;
    float A_Damage;
    bool A_AttackState = false;
    public float A_ResetTimer;
    //

    public void Werewolf()
    {
        Debug.Log("´Á´ë´Ù! ¹«¼·Âî!!");

        if (Input.GetMouseButtonDown(1))
        {
            rg = py.GetComponent<Rigidbody2D>();
            wolf = WolfGauge();
            StartCoroutine(wolf);
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopAllCoroutines();
            rg.AddForce(new Vector2(1, 0.6f) * WereWolf_Gauge * power);
            WereWolf_Gauge = 0;
        }
    }

    IEnumerator WolfGauge()
    {
        yield return new WaitForSeconds(0.5f);
        if (WereWolf_Gauge < 5) { WereWolf_Gauge += 1; }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(py.transform.position, new Vector2(5, 5));
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
            GameObject Boom = Instantiate(Bomber, pp, Quaternion.identity);
            Boom.gameObject.name = "Bomb";
            Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
            rg.AddForce(Dir * 100000 * Time.deltaTime);
        }
    }

    public void BattleAxe()
    {
        A_Attack();
        Debug.Log("¹ÙÅä·ç-¾ÆÄí½º");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("¸Â");
        //    if (col.gameObject.tag == "Monster")//ÀÓ½Ã
        //    {
        //        Debug.Log("¾Ò");
        //        if (A_Int <= 4)
        //        {
        //            Debug.Log("´Ù");
        //            A_Int++;
        //        }
        //    }
        //}
    }
    public void A_Attack()
    {

        float Reset = 10;
        switch (A_Int)
        {
            case 0:
                A_AttackState = false;
                A_Damage = 0;
                break;

            case 1:
                A_AttackState = true;
                A_Damage = 100;//ÀÓ½Ã
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 2:
                A_AttackState = true;
                A_Damage = 100;
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 3:
                A_AttackState = true;
                A_Damage = 100;
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;

            case 4:
                A_AttackState = true;
                A_AttackState = true;
                A_Damage = 150;//ÀÓ½Ã
                A_ResetTimer -= Time.deltaTime;
                if (A_ResetTimer <= 0)
                {
                    A_Int = 0;
                    A_ResetTimer = Reset;
                }
                break;
        }
    }

    public void RedBull()
    {

    }

    public void Ability_E()
    {
        EvilSword_Attack();
        EvilSwordResetAttack();
        Debug.Log("ÀÌ-ºÎ¸£ ¼Ò-µµ");
    }

    public void EvilSword_Attack()
    {
        AttackPlus();
        switch (E_Attack_Int)
        {
            case 0:
                E_ResetTimer = 0.8f;
                E_Attack_Damage = 0;
                break;
            case 1:
                EvilSwordResetAttack();
                E_ResetTimer -= Time.deltaTime;
                E_Attack_Damage = 15;
                break;
            case 2:
                EvilSwordResetAttack();
                E_ResetTimer -= Time.deltaTime;
                E_Attack_Damage = 15;
                break;
            case 3:
                EvilSwordResetAttack();
                E_ResetTimer -= Time.deltaTime;
                E_Attack_Damage = 30;
                break;
        }

    }

    public void AttackPlus()
    {
        if (Input.GetMouseButtonDown(0))
        {
            E_Attack_Int++;
        }

        if (E_Attack_Int > 3)
        {
            E_Attack_Int = 0;
        }
    }

    public void EvilSwordResetAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            E_ResetTimer = 0.8f;
        }
        switch (E_Attack_Int)
        {
            case 1:
                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;
            case 2:
                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;
            case 3:
                if (E_ResetTimer <= 0)
                {
                    E_ResetTimer = 0.8f;
                    E_Attack_Int = 0;
                }
                break;
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
