using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVILSWORD_ANI : MonoBehaviour
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
        ply.GetComponent<AbilityManager>().E_Attack_State = false;
    }

    void AI()
    {
        Attack_int = 0;
        Char_Parent.Active_Cool = 0f;
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
        ply.GetComponent<AbilityManager>().E_Attack_State = true;
    }
    void IcantDo()
    {
        ply.GetComponent<AbilityManager>().Evilst();
    }

    void IcanDoit()
    {
        ply.GetComponent<AbilityManager>().EvilRe();
    }

    void Movefalse()
    {
        ply.GetComponent<Char_Parent>().Ani.SetBool("Move", false);
    }
}