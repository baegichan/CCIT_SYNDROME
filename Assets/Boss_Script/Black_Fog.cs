using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Fog : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //�÷��̾� hp���� ����
            Debug.Log(22);
        }
    }
}
