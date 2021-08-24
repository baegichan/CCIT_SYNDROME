using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rDronBullet : MonoBehaviour
{
    public Vector2 targetPOS;
    public float speed;
    public static float damage = 0.5f;
    //public ParticleSystem destroy;

    void Start()
    {
        targetPOS = rDron.target.transform.position - transform.position;

        Invoke("LaserDestroy", 2f);
    }

    void Update()
    {
        transform.Translate(targetPOS * Time.deltaTime * speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "enemy" || col.tag == "ground")
        {
            col.GetComponent<Monster>().dameged(2);
            LaserDestroy();

            rDron.target = null;
        }
    }

    void LaserDestroy()
    {
        Destroy(this.gameObject);
        //Instantiate(destroy, transform.position, Quaternion.identity);
    }
}
