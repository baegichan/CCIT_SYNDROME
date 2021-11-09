using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssMonster : MonoBehaviour
{
    // Start is called before the first frame update
   
  
    AbyssManager AbyssManager;
    string names;

   
    private void Start()
    {
        AbyssManager = GameObject.Find("AbyssManager").transform.GetComponent<AbyssManager>();
          
        names = this.name;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ȭ���� ��ǥ�踦 ���� ��ǥ��� ��ȯ���ִ� �Լ�

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)

            {
               
                MonsterDie();

              

            }

        }
    }
    public  void MonsterDie()
    {

        AbyssManager.AbyssMonsterAdd(names, transform);
        Destroy(gameObject);

    }
}
