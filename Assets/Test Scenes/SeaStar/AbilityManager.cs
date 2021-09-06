using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> AbList = new List<Ability>();
    public Camera camera;
    public GameObject py;
    public GameObject Bomber;
    float WereWolf_Gauge = 0;

    public void Werewolf()
    {
        Debug.Log("�����! ������!!");

        if (Input.GetMouseButton(1))
        {
            WereWolf_Gauge += 1 * Time.deltaTime;
            Debug.Log("������,,,,,");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            WereWolf_Gauge = 0;
            Debug.Log("�� �� �� �� !!!!!!@!@!@!!@!");
            Debug.Log("�� �� �� �� !!!@!@!@!!@!");
            Debug.Log("�� �� �� �� !!@!@@!@!!@!@!@!!@!");
            Debug.Log("�� �� �� �� @!@!!@!");
            Debug.Log("�� �� �� �� !@@@@@@@@!@!@!!@!");
        }

    }

    public void Parao()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 pp = py.transform.position;
            Collider2D[] MonsterCol = Physics2D.OverlapBoxAll(pp, new Vector2(5, 5), 0, Physics2D.AllLayers);
            for(int i = 0; i < MonsterCol.Length; i++)
            {
                if (MonsterCol[i].tag == "enemy")
                {
                    Vector2 enemy = new Vector2(MonsterCol[i].transform.position.x, MonsterCol[i].transform.position.y) - pp;
                    enemy = enemy.normalized;
                    RaycastHit2D Hit = Physics2D.Raycast(pp, enemy);
                    Debug.DrawRay(pp, enemy, Color.green);
                    Debug.Log(Hit.transform.name);
                    if (Hit.transform.tag == "enemy")
                    {
                        Debug.Log("�Ķ��_4");
                        Debug.Log(Hit.transform.name + "��(��) ���ظ� ����");
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
            Vector2 pp = py.transform.position;
            Vector2 cursor = Input.mousePosition;
            cursor = camera.ScreenToWorldPoint(cursor);
            Vector2 BombDirection = (cursor - pp).normalized;

            GameObject Boom = Instantiate(Bomber, pp, Quaternion.identity);
            Boom.gameObject.name = "Bomb";
            Rigidbody2D rg = Boom.GetComponent<Rigidbody2D>();
            rg.AddForce(BombDirection * 500);
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
        Debug.Log("���� ���� �ӱ�");
    }
    public void Change_Jump_int()
    {
        PlayerM_ pp = py.GetComponent<PlayerM_>();
        if(pp.PassiveAbility.AbCode==6) { pp.P_MaxJumpInt = 2; }
        else { pp.P_MaxJumpInt = 1; }
    }
}
