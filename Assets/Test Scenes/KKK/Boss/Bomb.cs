using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void Start()
    {
        //anim_On();
        Destroy(this, 5f);
    }

    void anim_On()//���� ������ �ִϸ��̼� ���� ����
    {

    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {

            //������ �ִϸ��̼�



            Destroy(this.gameObject);
        }
    }
}
