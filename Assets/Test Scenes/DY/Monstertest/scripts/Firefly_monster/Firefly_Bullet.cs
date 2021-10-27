using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly_Bullet : MonoBehaviour
{
    [Header("Prameter")]
    public float speed;
    public Rigidbody2D rg;
    
    //public ParticleSystem destroy;

    void Start()
    {
        rg.AddForce(new Vector3(15,0,0), ForceMode2D.Impulse);
        Invoke("BulletDestroy", 2f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player" || col.tag == "ground")
        {
            Debug.Log("원거리 적 공격 적중");
            BulletDestroy();

            Dron.target = null;
        }
    }

    void BulletDestroy()
    {
        Destroy(this.gameObject);
        //Instantiate(destroy, transform.position, Quaternion.identity);
    }
}
