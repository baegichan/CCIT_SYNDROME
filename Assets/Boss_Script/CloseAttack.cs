using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour
{
    public GameObject Fog_zone;
    public GameObject Black_Fog;
    BossScript BS;
    private void Start()
    {
        BS = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BS.Stomping();
            BS.Boss_speed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BS.Sef_false_Stomping();
        }
    }
    public void Make_Fog()
    {
        Instantiate(Black_Fog, Fog_zone.transform.position, Quaternion.identity);
    }
}
