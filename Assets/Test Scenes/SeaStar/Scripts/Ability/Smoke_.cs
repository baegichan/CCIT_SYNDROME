using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rigid.velocity = (Dir * Speed * Time.deltaTime);
        //transform.Translate(Dir * Speed * Time.deltaTime);
        Dis = Vector2.Distance(PP, transform.position);
        if (Dis >= Distance)
        {
            if(isHitMonster == false)
            {
                isEndOfEXPLOSION = true;
                if (isEndOfEXPLOSION == true)
                {
                    Instantiate(ES, transform.position, Quaternion.identity);
                    ES.GetComponent<ParticleSystem>().Play();
                    Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (Collider2D Current in Boom)
                    {
                        if (Current.GetComponent<Character>() != null)
                        {
                            Current.GetComponent<Character>().Damage(Char_Parent.ply.AM.DarkSmokeExplosionAP[Char_Parent.ply.ActiveAbility.Enhance]);
                        }
                        ES.GetComponent<ParticleSystem>().Clear(); 
                        gameObject.SetActive(false);
                        Destroy(gameObject, 0f);
                        //Destroy(ES, 0f);
                        isEndOfEXPLOSION = false;
                        AbilityManager.isShoot = false;
                    } 
                }
            }           
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, 1);
    }
    LayerMask Layer;
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Monster"||col.tag == "Wall"|| col.tag == "Ground")
        {
            if (col.gameObject.GetComponent<Character>() != null)
            {
                CT = col.gameObject.GetComponent<Character>();
                CT.Damage(Char_Parent.ply.AM.DarkSmokeAP[Char_Parent.ply.ActiveAbility.Enhance]);
            }
            isHitMonster = true; 
            if(isHitMonster == true)
            {
                Instantiate(ES, transform.position, Quaternion.identity);
                ES.GetComponent<ParticleSystem>().Play();
                Collider2D[] Boom = Physics2D.OverlapCircleAll(transform.position, 1);
                foreach (Collider2D Current in Boom)
                {
                   if (Current.GetComponent<Character>() != null)
                    {
                        Current.GetComponent<Character>().Damage(Char_Parent.ply.AM.DarkSmokeExplosionAP[Char_Parent.ply.ActiveAbility.Enhance]);
                    }
                    ES.GetComponent<ParticleSystem>().Clear();
                    gameObject.SetActive(false);
                    Destroy(gameObject, 0f);
                    //Destroy(ES, 0f);
                    isEndOfEXPLOSION = false;
                    AbilityManager.isShoot = false;
                }
            }
        }
    }
}