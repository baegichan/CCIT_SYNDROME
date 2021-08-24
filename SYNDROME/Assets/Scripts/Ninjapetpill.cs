using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninjapetpill : MonoBehaviour
{
    public GameObject NinjapetPills;
    public GameObject ninja;

    public GameObject pos;
    void Update()
    {
        if (Player.NinjapetPillsEatPill == true)
        {
            GameObject Ninjapet = Instantiate(ninja, pos.transform.position , pos.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
