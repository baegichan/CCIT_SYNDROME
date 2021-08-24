using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Stone_Script : MonoBehaviour
{
    public Transform Black_Stone;
    private void Start()
    {
        Black_Stone = GameObject.FindGameObjectWithTag("Black_Stone").transform;
    }
    public void ara()//검은돌 위로 올려줌
    {
        Black_Stone.position = Vector2.MoveTowards(this.transform.position, new Vector2(40, -219), Time.deltaTime);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//나중에 플레이어 공격 감지로 변경
        {
            //Destroy(this);
            Debug.Log("파괴됨");
        }
    }*/

    //검은 돌 플레이어랑 충돌구현 하다 말았츰
}
