using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSwordAniEvent : MonoBehaviour
{
    public static int Attack_int = 0;
    public static List<GameObject> hit = new List<GameObject>();

    public void EvilSwordEvent()
    {
        GetComponent<Abduru>().E_Attack_State = false;
    }

    void AttackInitialization()
    {
        Attack_int = 0;
        Debug.Log("��Ź��: " + Attack_int);
    }

    void HitInitializtion()
    {
        hit.Clear();
        Debug.Log("�� ���� ���" + hit.Count);
    }

    public static void intup()
    {
        Attack_int++;
    }
}
