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
                if(Attack_int <= 4)
                {
                    Current = col.gameObject;
                    Attack_int += 1;
                    Fourth();
                }
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
                Debug.Log("4 !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!:" + Attack_int);
                CameraShake.Shake(10000, 50);     
                Current.GetComponent<Character>().Damage(D + 20);//���� �÷����� ������ ���氡��
                break;          
        }

        Debug.Log("����:" + Attack_int);
        if (Abduru.A_Attack_State == false)
        {
            if(Attack_int == 5)
            {
                Debug.Log("�ʱ�ȭ ����:" + Attack_int);
                Attack_int = 0;
            }   
        }
        if (Abduru.A_Attack_State == true)
        {
            if (Attack_int == 5)
            {
                Debug.Log("���ʱ�ȭ:" + Attack_int);
                Attack_int = 0;
            }
        }
    }
}


