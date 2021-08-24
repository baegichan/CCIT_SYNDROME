using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clup_Events : MonoBehaviour
{
    public GameObject Clup;
    public float speed;

    public void Event_0()
    {
        speed = this.gameObject.GetComponent<MeleeAttack>().speed;
        this.gameObject.GetComponent<MeleeAttack>().speed = 0;
        this.gameObject.GetComponent<Animator>().SetBool("Attack", false);
        Clup.SetActive(true);
    }
    public void Event_1()
    {
        Clup.SetActive(false);
        this.gameObject.GetComponent<MeleeAttack>().speed = speed;
    }
}
