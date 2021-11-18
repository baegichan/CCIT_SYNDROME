using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineAxe : MonoBehaviour
{
    public int D;
    public int Attack_int = 0;
    public GameObject Current;
    public GameObject YourParent;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (AbilityManager.A_Attack_State == true)
        {
            if (col.tag == "Monster")
            {
                Current = col.gameObject;
                Attack_int++;
                Fourth();
            }
        }
    }

    private void Fourth()
    {
        switch (Attack_int)
        {
            case 1:
                Current.GetComponent<Character>().Damage(D);
                break;
            case 2:
                Current.GetComponent<Character>().Damage(D);
                break;
            case 3:
                Current.GetComponent<Character>().Damage(D);
                break;
            case 4:
                Current.GetComponent<Character>().Damage(D + 20);//더미 플러스값 언제든 변경가능
                break;
            default:
                Current.GetComponent<Character>().Damage(D = 0);
                break;
        }

        if (Attack_int <= 5)
        {
            Attack_int = 0;
        }
    }
}
