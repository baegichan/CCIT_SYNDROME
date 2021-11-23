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
                    CameraShake.Cam_instance.Shake(0.12f, 0.005f);
                    Current = col.gameObject;
                    Current.GetComponent<Character>().Damage(D);
                }
            }
            if (EvilSwordAniEvent.Attack_int == 2)
            {
                if (col.tag == "Monster")
                {              
                    CameraShake.Cam_instance.Shake(0.5f, 0.09f);
                    Current = col.gameObject;
                    Current.GetComponent<Character>().Damage(D);
                  
                }
            }
        }
    }
    
    void First()
    {
        if (EvilSwordAniEvent.Attack_int == 0)
        {
            CameraShake.Cam_instance.Shake(0.12f, 0.01f);
            Current.GetComponent<Character>().Damage(20);
        }
    }
}
