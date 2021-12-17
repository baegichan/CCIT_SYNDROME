using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_ : MonoBehaviour
{
    public Character CT;
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
        Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
        if(CT != null)
            CT.Damage(Char_Parent.ply.AM.DarkSmokeAP[Char_Parent.ply.ActiveAbility.Enhance]);

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, 2);
    }
    LayerMask Layer;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag != "Player")
        {
            CT = col.GetComponent<Character>();
            Explosion();
        }
    }
}