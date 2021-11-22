using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAxeAttack : MonoBehaviour
{
    public int D;
    public static int Attack_int;
    public GameObject Current;
    public GameObject YourParent;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(Abduru.A_Attack_State == true)
        {
            if (col.tag == "Monster")
            {
                Debug.Log("현재:" + Attack_int);
                if (Attack_int <= 4)
                {
                    Current = col.gameObject;
                    Attack_int ++;
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
                //CameraShake.Cam_instance.Shake(1000, 500);     
                Current.GetComponent<Character>().Damage(D + 20);//더미 플러스값 언제든 변경가능
                break;          
        }

       
        if (Abduru.A_Attack_State == false)
        {
            if(Attack_int == 5)
            {
                Debug.Log("초기화 현재:" + Attack_int);
                Attack_int = 0;
            }   
        }
        if (Abduru.A_Attack_State == true)
        {
            if (Attack_int == 5)
            {
                Debug.Log("안초기화:" + Attack_int);
                Attack_int = 0;
            }
        }
    }
}


