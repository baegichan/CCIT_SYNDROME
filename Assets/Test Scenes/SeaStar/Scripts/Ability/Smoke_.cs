using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke_ : MonoBehaviour
{
    public Character CT;
    public GameObject ES;
    public float Speed;
    public Vector2 Dir;
    public Vector2 PP;
    public float Dis;
    public float Distance;
    bool isEndOfEXPLOSION = false;
    bool isHitMonster = false;
    void Update()
    {
        transform.Translate(Dir * Speed * Time.deltaTime);
        Dis = Vector2.Distance(PP, transform.position);
        if (Dis >= Distance)
        {
            if(isHitMonster == false)
            {
                isEndOfEXPLOSION = true;
                if (isEndOfEXPLOSION == true)
                {
                    Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (Collider2D Current in Boom)
                    {
                        Instantiate(ES, transform.position, Quaternion.identity);
                        ES.GetComponent<ParticleSystem>().Play();
                        Current.GetComponent<Character>().Damage(Char_Parent.ply.AM.DarkSmokeExplosionAP[Char_Parent.ply.ActiveAbility.Enhance]);
                        ES.GetComponent<ParticleSystem>().Clear();
                        isEndOfEXPLOSION = false;
                        Delete();
                    } 
                }
            }           
        }
    }

    //public void Explosion()
    //{
    //    Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
    //    foreach (Collider2D Current in Boom)
    //    {
    //        Instantiate(ES, transform.position, Quaternion.identity);
    //        ES.GetComponent<ParticleSystem>().Play();
    //        Current.GetComponent<Character>().Damage(Char_Parent.ply.AM.DarkSmokeExplosionAP[Char_Parent.ply.ActiveAbility.Enhance]);
    //        Delete();
    //    }
    //}
    void Delete()
    {
        isEndOfEXPLOSION = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
        Destroy(ES);
        isEndOfEXPLOSION = false;
        AbilityManager.isShoot = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, 1);
    }
    LayerMask Layer;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            CT = col.gameObject.GetComponent<Character>();
            CT.Damage(Char_Parent.ply.AM.DarkSmokeAP[Char_Parent.ply.ActiveAbility.Enhance]);
            isHitMonster = true; 
            if(isHitMonster == true)
            {
                Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
                foreach (Collider2D Current in Boom)
                {
                    Instantiate(ES, transform.position, Quaternion.identity);
                    ES.GetComponent<ParticleSystem>().Play();
                    Current.GetComponent<Character>().Damage(Char_Parent.ply.AM.DarkSmokeExplosionAP[Char_Parent.ply.ActiveAbility.Enhance]);
                    ES.GetComponent<ParticleSystem>().Clear();
                    Delete();
                }
            }
        }
    }
}