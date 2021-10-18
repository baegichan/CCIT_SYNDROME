using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssMonster : MonoBehaviour
{
    // Start is called before the first frame update


    AbyssManager abyssManager;
   
    public int abyssMonsterId = 0;
    [Header("AbyssGage")]
    public int giveAbyssGage = 5;
    [Header("DarkFog")]
    public int darkFog = 5;
    int id;
    string names;
    AbyssManager.AbyssState state;
    private void Start()
    {
        abyssManager = GameObject.Find("AbyssManager").transform.GetComponent<AbyssManager>();

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
                MonsterDie();
        }

        //if(state != abyssManager.abyssState)
        //{
        //    Debug.Log("���ҽ���");
        //    state = abyssManager.abyssState;
        //    abyssManager.MonsterSwap(gameObject);
        //     Destroy(this.gameObject);
        //}

    }
    public void MonsterDie()
    {
        if (abyssManager.abyssState == AbyssManager.AbyssState.Reality)
        {
            abyssManager.AbyssMonsterAdd(names, transform.position);
            abyssManager.GetAbyssGage(giveAbyssGage);
            Destroy(this.gameObject);

        }
        else
        {
            abyssManager.GetDarkFog(darkFog);
            Destroy(this.gameObject);
        
        }

    }

  
}
