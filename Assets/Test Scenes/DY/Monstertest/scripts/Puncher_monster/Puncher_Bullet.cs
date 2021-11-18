using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher_Bullet : MonoBehaviour
{
    [Header("Prameter")]
    public float bullet_speed;
    public int bullet_Damage;

    [Header("Refernce")]
    GameObject Player;
    public Vector2 trans;
    public Vector3 dir;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        trans = new Vector2(Player.transform.position.x, Player.transform.position.y);
        dir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(Player.transform.position.y - transform.position.y, Player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        if (Player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        if (Player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        }
    }
    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * bullet_speed, dir.y + 1);

        if (GetComponent<Rigidbody2D>().velocity.x > 0.1f)
        {
            float Left_Dir = -0.7f;
            transform.localScale = new Vector3(Left_Dir, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            float Right_Dir = 0.7f;
            transform.localScale = new Vector3(Right_Dir, transform.localScale.y, transform.localScale.z);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.GetComponentInParent<Character>().Damage(bullet_Damage);
            BulletDestroy();
        }
        if (col.tag == "Ground")
        {
            Debug.Log("");
            BulletDestroy();
        }
        if (col.tag == "Wall")
        {
            BulletDestroy();
        }
    }

    void BulletDestroy()
    {
        Destroy(this.gameObject);
        //Instantiate(destroy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
