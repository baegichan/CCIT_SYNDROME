using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forchecking : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cha;
    public GameObject Player;
    public  Vector2 check_position;
    public  Vector2 check_exit_position;
    private Transform player;//플레이어 위치\
    
   
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            check_position = cha.gameObject.transform.position;
            Debug.Log("감지 했을때 좌표 저장");

        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (collision.CompareTag("Player"))
        { 
            if (cha.GetComponent<Chamovement>().Attack_state() != true)
            {
                cha.GetComponent<Chamovement>().Turn_move();
            }

           
                if (Player.transform.position.x>cha.transform.position.x)
                {
                cha.transform.localScale = new Vector2(1, cha.transform.localScale.y);
                }
                else if (Player.transform.position.x < cha.transform.position.x)
                {
                cha.transform.localScale = new Vector2(-1, cha.transform.localScale.y);

                }
            

        }
        


    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            check_exit_position = cha.gameObject.transform.position;
            Debug.Log("나갔을 때 좌표 저장");

            cha.GetComponent<Chamovement>().Turn_back_start_position();          
            //다시 위로 가야함

        }
       
    }
    
}
