using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgbMonster : MonoBehaviour
{
    public AbyssMonster thisMonster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))

        {

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //ȭ���� ��ǥ�踦 ���� ��ǥ��� ��ȯ���ִ� �Լ�

			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)

            {
          
                thisMonster.MonsterDie();
                Debug.Log("d");


            }

        }



  
    }
    
}
