using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_range_in : MonoBehaviour
{
    // Start is called before the first frame update
    public CircleCollider2D range;
    public GameObject monster;
    
    void Start()
    {
        range = this.gameObject.GetComponent<CircleCollider2D>();
        
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(collision.transform.position.x>monster.transform.position.x)
            {
                monster.GetComponent<SpriteRenderer>().flipX = true;

            }
            else if(collision.transform.position.x < monster.transform.position.x)
            {
                monster.GetComponent<SpriteRenderer>().flipX = false;
            }
            
           // ¿·Ω√∫¿¿Œ monster.GetComponent<Chamovement>().Attack_mode();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           //¿·Ω√∫¿¿Œ monster.GetComponent<Chamovement>().Patroll_mode();
        }
    }

}
