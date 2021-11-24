using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public GameObject Boss;//Component받아와야함

    Transform target;

    [Header("Normal")]
    public float bullet_speed;
    public int Bullet_Damage;

    [Header("Abyss")]
    public float Abyss_bullet_speed;
    public int Abyss_bullet_Damage;

    public bool Abyss_Bullet_State = false;

    public bool left;
    public bool right;

    Vector3 aa;


    Boss boss;
    private void Start()
    {
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
        /*
        if (target.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * bullet_speed, ForceMode2D.Impulse);
        }
        if (target.position.x < transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * bullet_speed, ForceMode2D.Impulse);
        }
        */
        /*
        if (target.position.x > transform.position.x)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * bullet_speed, ForceMode2D.Impulse);
        }
        if (target.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        */
        if(left == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * bullet_speed, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (right == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * bullet_speed, ForceMode2D.Impulse);
            GetComponent<SpriteRenderer>().flipX = true;
        }





    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(Bullet_Damage);
                //collision.transform.parent.GetComponent<Character>().KnuckBack(transform, 5, collision.GetComponent<Character>().IsBoss);
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }


    /*
    void Start()
    {
        //bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = new Vector2(target.transform.position.x, target.transform.position.y);
        Vector3 dir = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(target.transform.position.y - this.transform.position.y, target.transform.position.x - this.transform.position.x) *
            Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        Destroy(this.gameObject, 5);

    }
    private void Update()
    {
        Vector2 moveDir = (target.transform.position - transform.position).normalized * (speed * Time.deltaTime);
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(targetPos.x, targetPos.y+0.8f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            Debug.Log("ㄴㅁㅇ");
        }
        Debug.Log("공격 받음");

    }
    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }*/
}
