using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEVILSWORD_ANI : MonoBehaviour
{
    public static int Attack_int = 0;
    public Char_Parent ply;
    public AbilityManager am;
    public DEVILSWORD Single;
    public DEVILSWORD[] Sjard;
    public DEVILSWORD circle;

    void Update()
    {
        if (Attack_int >= 3)
        {
            Attack_int = 0;
        }
    }

    public void OffAttack()
    {
        am.E_Attack_State = false;
    }

    void AI()
    {
        Attack_int = 0;
        Char_Parent.Active_Cool = 0f;
    }

    public void int1()
    {
        Attack_int = 1;
    }
    public void int2()
    {
        Attack_int = 2;
    }

    void OnAttack()
    {
        am.E_Attack_State = true;
    }
    void IcantDo()
    {
        am.Evilst();
    }

    void IcanDoit()
    {
        am.EvilRe();
    }

    void Movefalse()
    {
        ply.Ani.SetBool("Move", false);
    }
    void ESAttack()
    {
        if(Attack_int == 0) { Single.EvilSwordAttack(); }
        else if(Attack_int == 1)
        {
            Sjard[0].EvilSwordAttack();
            Sjard[1].EvilSwordAttack();
        }
        else if(Attack_int == 2) { circle.EvilSwordAttack(); }
    }
}