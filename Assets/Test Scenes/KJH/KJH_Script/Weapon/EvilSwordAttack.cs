using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSwordAttack : MonoBehaviour
{
    public int D;
    int Attack_int = 0;
    public GameObject Current;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Monster")
        {
            Current = col.gameObject;
            Attack_int++;
            Third();
        }
    }

    private void Third()
    {
        switch (Attack_int)
        {
            case 1:
                Current.GetComponent<Character>().Damage(D);
                break;
            case 2:
                Current.GetComponent<Character>().Damage(D += 20);//���� �÷����� ������ ���氡��
                break;
            case 3:
                Current.GetComponent<Character>().Damage(D += 40);//���� �÷����� ������ ���氡��
                break;
            default:
                Current.GetComponent<Character>().Damage(D = 0);
                break;
        }

        if (Attack_int <= 4)
        {
            Attack_int = 0;
        }
    }
}
