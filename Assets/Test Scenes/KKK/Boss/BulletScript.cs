using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //public GameObject Boss;//Component받아와야함

    Transform target;
    public float bullet_speed;

    //Transform Boss_Trasnform;
    Vector2 First_transform;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        First_transform = this.transform.position;
    }
    public void Update()
    {
        if (target.position.x > First_transform.x)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right, ForceMode2D.Impulse);
        }
        if (target.position.x < First_transform.x)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.GetComponent<Character>().Damage(10);
                
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
