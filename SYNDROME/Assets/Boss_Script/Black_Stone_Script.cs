using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Stone_Script : MonoBehaviour
{
    public Transform Black_Stone;
    private void Start()
    {
        Black_Stone = GameObject.FindGameObjectWithTag("Black_Stone").transform;
    }
    public void ara()//������ ���� �÷���
    {
        Black_Stone.position = Vector2.MoveTowards(this.transform.position, new Vector2(40, -219), Time.deltaTime);
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))//���߿� �÷��̾� ���� ������ ����
        {
            //Destroy(this);
            Debug.Log("�ı���");
        }
    }*/

    //���� �� �÷��̾�� �浹���� �ϴ� ������
}
