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

            //화면의 좌표계를 월드 좌표계로 전환해주는 함수

			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null)

            {
          
                thisMonster.MonsterDie();
                Debug.Log("d");


            }

        }



  
    }
    
}
