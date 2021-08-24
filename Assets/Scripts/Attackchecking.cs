using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackchecking : MonoBehaviour
{
    public GameObject cha;
    private Transform player;//플레이어 위치
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
         float distanceFromPlayer = Vector2.Distance(cha.transform.position, transform.position);


        if (collision.CompareTag("Player"))
        {
         
            cha.GetComponent<Chamovement>().check();
            
            distanceFromPlayer = 9f;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {

            cha.GetComponent<Chamovement>().Turn_move();
        }
    }
}
