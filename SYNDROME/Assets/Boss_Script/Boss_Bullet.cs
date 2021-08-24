using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    GameObject Player;

    public float bullet_speed;

    Vector2 a;
    Vector2 b;
    Vector3 dir;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        b = new Vector2(Player.transform.position.x, Player.transform.position.y);
        //sp = GetComponent<SpriteRenderer>();
      dir = Player.transform.position - this.transform.position;
        float angle = Mathf.Atan2(Player.transform.position.y - this.transform.position.y, Player.transform.position.x - this.transform.position.x) *
            Mathf.Rad2Deg;
        if (Player.transform.position.x < transform.position.x)
        {

            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Player.transform.position.x > transform.position.x)
        {

            this.transform.rotation = Quaternion.AngleAxis(angle-180, Vector3.forward);
        }
    }
    private void Update()
    {
        //a = (Player.transform.position - transform.position).normalized * (bullet_speed * Time.deltaTime);

        this.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x, dir.y + 1);

        if (this.GetComponent<Rigidbody2D>().velocity.x > 0.1f) // 속도 늦춰야 할듯?
        {
            float Left_Dir = -0.5f;
            transform.localScale = new Vector3(Left_Dir, transform.localScale.y, transform.localScale.z);

        }//방향 전환
        else
        {
            float Right_Dir = 0.5f;
            transform.localScale = new Vector3(Right_Dir, transform.localScale.y, transform.localScale.z);

        }//방향전환


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            Debug.Log(22222);
            Destroy(this.gameObject);
            //플레이어 hp감소 ~~~
        }
    }
}