using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public GameObject Boss;//Component받아와야함

    Transform target;

    [Header("Normal")]
    public float bullet_speed;
    int Bullet_Damage;

    [Header("Abyss")]
    public float Abyss_bullet_speed;
    int Abyss_bullet_Damage;

    public bool Abyss_Bullet_State = false;

    public bool left;
    public bool right;

    Vector3 aa;

    public GameObject Boss;
    private void Start()
    {
        Bullet_Damage = Random.Range(2, 6);
        Abyss_bullet_Damage = Random.Range(4, 8);
        if(Abyss_Bullet_State == true)
        {
            bullet_speed = Abyss_bullet_speed;
            Bullet_Damage = Abyss_bullet_Damage;
        }
        target = GameObject.FindGameObjectWithTag("Player").transform;




        aa = target.position - transform.position;

        if (target.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //right = true;
        }
        if (target.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //left = true;
        }

        

    }
    public void Update()
    {
        if(left == true)
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = false;
            //this.transform.position = new Vector3(this.transform.position.x - 0.1f,transform.position.y,transform.position.z);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
        }
        if (right == true)
        {
            //GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = true;
            //this.transform.position = new Vector3(this.transform.position.x + 0.1f, transform.position.y, transform.position.z);
            GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground") || collision.CompareTag("Wall"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(Bullet_Damage);
                
                collision.GetComponentInParent<Character>().PlayerKnuckBack(transform, collision.transform, 5, false);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }


}
