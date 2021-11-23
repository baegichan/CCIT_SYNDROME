using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXE : MonoBehaviour
{
    public int Damage;
    public static int Attack_int;
    public GameObject Current;
    public GameObject YourParent;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (AbilityManager.A_Attack_State == true)
        {
            if (col.tag == "Monster")
            {
                if (Attack_int <= 4)
                {
                    Current = col.gameObject;
                    Attack_int++;
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
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                Current.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                break;
            case 2:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                Current.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                break;
            case 3:
                CameraShake.Cam_instance.Shake(0.09f, 0.02f);
                Current.GetComponent<Character>().Damage(Damage, YourParent.GetComponent<Char_Parent>().UseApPostion);
                break;
            case 4:
                Debug.Log("aaaa");
                CameraShake.Cam_instance.Shake(0.12f, 0.8f);
                Current.GetComponent<Character>().Damage(Damage + 60, YourParent.GetComponent<Char_Parent>().UseApPostion);//더미 플러스값 언제든 변경가능
                break;
        }


        if (AbilityManager.A_Attack_State == false)
        {
            if (Attack_int == 5)
            {
                Debug.Log("초기화 현재:" + Attack_int);
                Attack_int = 0;
            }
        }
        if (AbilityManager.A_Attack_State == true)
        {
            if (Attack_int == 5)
            {
                Debug.Log("안초기화:" + Attack_int);
                Attack_int = 0;
            }
        }
    }
}
