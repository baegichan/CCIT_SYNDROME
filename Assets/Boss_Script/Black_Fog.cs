using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Fog : MonoBehaviour
{
    public int Black_Fog_Damage = 15;
    public int Abycss_Black_Fog_Damage = 25;
    float Damage_Delay = 1.5f;

    public bool Abycss_Boss_Monster= false;


    public GameObject Boss;
    public GameObject Abyss_Boss;

    private void Start()
    {
        if(Abycss_Boss_Monster == true)
        {
            Black_Fog_Damage = Abycss_Black_Fog_Damage;
        }
        Destroy(this.gameObject, 1.5f);
    }

    private void Update()
    {
        if(Damage_Delay >= 0)
        {
            Damage_Delay = Mathf.Clamp(Damage_Delay + Time.deltaTime, 0, 0.5f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Damage_Delay == 0.5f)
        {
            collision.transform.parent.GetComponent<Character>().Damage(Black_Fog_Damage);
            Damage_Delay = 0;
        }
    }
}
