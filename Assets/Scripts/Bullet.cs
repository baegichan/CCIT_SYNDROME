using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Destroy(this.gameObject, 10f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "enemy" ||col.tag == "Ground")
        {
            if(col.tag == "enemy")
            {
                Dron.target = col.gameObject;
                col.GetComponent<Monster>().dameged(1);
                ninjapet.AK = true;
                Dron.Ak = true;
                ninjapet.target = col.gameObject;
            }
            Destroy(this.gameObject);

            
        }
    }
}
