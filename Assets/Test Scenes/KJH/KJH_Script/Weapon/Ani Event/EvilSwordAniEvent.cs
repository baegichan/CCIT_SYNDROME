using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSwordAniEvent : MonoBehaviour
{
    public static int Attack_int = 0;
    public List<GameObject> hit = new List<GameObject>();
    public GameObject ply;

    void Update()
    {
        if (Attack_int >= 3)
        {
            Attack_int = 0;
        }
    }

    public void Statefalse()
    {
        ply.GetComponent<Abduru>().E_Attack_State = false;
    }

    void AI()
    {
        Attack_int = 0;
    }

    public void HI()
    { 
        hit = new List<GameObject>();
    }

    public void int1()
    {
        Attack_int = 1;
    }
    public void int2()
    {
        Attack_int = 2;
    }

    void OnAttakcState()
    {
        ply.GetComponent<Abduru>().E_Attack_State = true;
    }
}
