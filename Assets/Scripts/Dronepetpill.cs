using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dronepetpill : MonoBehaviour
{
    public GameObject DroenpetPills;
    public GameObject droen;

    public GameObject petpos;
    void Update()
    {
        if (Player.DronpetEatPill == true)
        {
            GameObject dron = Instantiate(droen, petpos.transform.position, petpos.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
