using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void Start()
    {
        //anim_On();
        //Invoke("anim_On",4);
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
            if (collision.CompareTag("Player"))
            {
                //������ �ִϸ��̼�
                anim_On();
                collision.GetComponent<Character>().Damage(20);
                Destroy(gameObject);
                //Destroy(this.gameObject, 5);�÷��̾ ������ �����°� �ִϸ��̼� ������ �����ٲ���// 5�ʴ� �׳� ����
            }
            if (collision.CompareTag("Ground"))
            {
                anim_On();
                Destroy(gameObject);
            }
        
        }
    }
}
