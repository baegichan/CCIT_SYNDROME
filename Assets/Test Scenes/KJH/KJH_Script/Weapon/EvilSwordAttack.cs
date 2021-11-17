using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSwordAttack : MonoBehaviour
{
    public int D;
    public GameObject Current;
    public GameObject YourParent;

    //public GameObject YourParent;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (YourParent.GetComponent<Abduru>().E_Attack_State == true)
        {
            Debug.Log(EvilSwordAniEvent.Attack_int);
            if (EvilSwordAniEvent.Attack_int == 0)
            {
                if (col.tag == "Monster")
                {
                    Current = col.gameObject;
                    if (EvilSwordAniEvent.hit.Count > 0)
                    {
                        for (int i = 0; i > EvilSwordAniEvent.hit.Count; i++)
                        {
                            if (EvilSwordAniEvent.hit[i] != Current)
                            {
                                First();
                                Debug.Log("내처음은아닌데얘는처음이야");
                                EvilSwordAniEvent.hit.Add(Current);
                            }
                        }
                    }
                    else
                    {
                        First();
                        Debug.Log("내처음이야");
                        EvilSwordAniEvent.hit.Add(Current);
                    }
                }
            }
            else if(EvilSwordAniEvent.Attack_int > 0)
            {
                if (col.tag == "Monster")
                {
                    Third();
                    Debug.Log("나비치야");
                }
            }
        }         
    }

    private void Third()
    {
        switch (EvilSwordAniEvent.Attack_int)
        {
            case 1:
                Current.GetComponent<Character>().Damage(D);
                break;
            case 2:
                Current.GetComponent<Character>().Damage(D);
                break;
        }
        if (EvilSwordAniEvent.Attack_int >= 4)
        {
            EvilSwordAniEvent.Attack_int = 0;
        }
    }
    void First()
    {
        if(EvilSwordAniEvent.Attack_int == 0)
        {
            Current.GetComponent<Character>().Damage(D);
        }
    }
}
