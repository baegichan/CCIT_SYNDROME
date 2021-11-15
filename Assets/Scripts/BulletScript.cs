using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Transform target;
    public float bullet_speed;
    //Rigidbody2D bulletRB;
    Vector2 targetPos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), Time.deltaTime * bullet_speed);

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
