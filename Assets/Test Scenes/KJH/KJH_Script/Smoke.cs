using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public Vector2 Dir;
    public Vector2 PP;
    public Vector2 Die;

    void Update()
    {
        transform.Translate(Dir * 10 * Time.deltaTime);

        float Dis = Vector2.Distance(PP, transform.position);

        if (Dis >= 10)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 2);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, 2);
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag != "Player")
        {
            Explosion();
        }
    }
}
