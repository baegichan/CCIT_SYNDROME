using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaePoAl : MonoBehaviour
{
    Rigidbody2D rig;
    public float power;
    Player p;
    public float Damage;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(9, 10);
        rig = GetComponent<Rigidbody2D>();

        //p = GameObject.Find("Player").GetComponent<Player>();

        rig.AddForce(Vector2.left * power);

        destory();
    }

    void Update()
    {
 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("shield"))
        {
            Debug.Log("막음");
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Debug.Log("데미지 입힘");
            Destroy(this.gameObject);
            PlayerMovement.Hp -= Damage;
        }
    }

    void destory()
    {
        Destroy(this.gameObject, 5);
    }
}
