using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class new_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public Rigidbody2D rigid;
    public enum Target
    {
    Player,
    Monster
    }
    public Target Bullet_Target = Target.Monster;
    private void Start()
    {

        rigid = GetComponent<Rigidbody2D>();
        
    }
    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.right * Speed;
    }

  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Bullet_Target == Target.Player)
        {
            if (collision.tag == "Player")
            {
                // HP -
                Destroy(gameObject);
            }
            
        }
        else if(Bullet_Target==Target.Monster)
        {
            if (collision.tag == "Monster")
            {
                // HP -
                Destroy(gameObject);
            }
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
