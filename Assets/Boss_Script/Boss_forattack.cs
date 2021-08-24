using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_forattack : MonoBehaviour
{
    BossScript bs;
    Ball ball;


    private void Start()
    {
        bs = GameObject.Find("Boss").GetComponent<BossScript>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bs.bb >= 0)
            {
                Debug.Log("13");
                bs.Turn_attack();
            }
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bs.bb >= 0)
            {
                bs.Turn_attack();

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bs.Turn_move();
        }
    }
}
