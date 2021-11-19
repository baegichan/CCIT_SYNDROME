using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAxeAttack : MonoBehaviour
{
    public int D;
    public static int Attack_int = 0;
    public GameObject Current;
    public GameObject YourParent;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(Abduru.A_Attack_State == true)
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
                Debug.Log("1 :" + Attack_int);
                Current.GetComponent<Character>().Damage(D);
                break;
            case 2:
                Debug.Log("2 :" + Attack_int);
                Current.GetComponent<Character>().Damage(D);
                break;
            case 3:
                Debug.Log("3 :" + Attack_int);
                Current.GetComponent<Character>().Damage(D);
                break;
            case 4:
                Debug.Log("4 :" + Attack_int);
                Current.GetComponent<Character>().Damage(D + 20);//더미 플러스값 언제든 변경가능
                break;          
        }

        if(Attack_int >= 5)
        {
            Attack_int = 0;
        }
    }
}


