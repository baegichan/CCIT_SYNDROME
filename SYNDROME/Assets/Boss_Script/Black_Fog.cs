using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Fog : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //플레이어 hp감소 구현
            Debug.Log(22);
        }
    }
}
