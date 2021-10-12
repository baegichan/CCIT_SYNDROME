using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_False : MonoBehaviour
{
    public float speed;

    public void Event()
    {
        GetComponent<Animator>().SetBool("Attack", false);
    }

    public void Event_()
    {
        speed = this.gameObject.GetComponent<MeleeAttack>().speed;
        this.gameObject.GetComponent<MeleeAttack>().speed = 0;
    }

    public void Event__()
    {
        this.gameObject.GetComponent<MeleeAttack>().speed = speed;
    }
}
