using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSwordAttack : MonoBehaviour
{
    public int D;
    public GameObject Event;
    public GameObject Current;
    public GameObject YourParent;

    //public GameObject YourParent;
    void OnTriggerEnter2D(Collider2D col)
    {
        EvilSwordAniEvent ese = Event.GetComponent<EvilSwordAniEvent>();
        if (YourParent.GetComponent<Abduru>().E_Attack_State == true)
        {
            if (EvilSwordAniEvent.Attack_int == 0)
            {
                if (col.tag == "Monster")
                {
                    Current = col.gameObject;
                    if (ese.hit.Count > 0)
                    {                       
                        bool tt = true;
                        for (int i = 0; i < ese.hit.Count; i++)
                        {
                            if (ese.hit[i] == Current)
                            {
                                tt = false;
                            }
                        }
                        if (tt)
                        {
                            First();
                            ese.hit.Add(Current);
                        }
                    }
                    else
                    {
                        First();
                        ese.hit.Add(Current);
                    }
                }
            }
            if (EvilSwordAniEvent.Attack_int == 1)
            {
                if (col.tag == "Monster")
                {
                    Current = col.gameObject;
                    Current.GetComponent<Character>().Damage(D);
                }
            }
            if (EvilSwordAniEvent.Attack_int == 2)
            {
                if (col.tag == "Monster")
                {
                    CameraShake.Shake(10000, 10);
                    Current = col.gameObject;
                    Current.GetComponent<Character>().Damage(D);
                    Current.GetComponent<Character>().Damage(D);
                    Current.GetComponent<Character>().Damage(D); 
                    Current.GetComponent<Character>().Damage(D);
                }
            }
        }
    }
    
    void First()
    {
        if (EvilSwordAniEvent.Attack_int == 0)
        {
            Current.GetComponent<Character>().Damage(20);
        }
    }
}
