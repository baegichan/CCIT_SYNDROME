using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int Normal_Boss_Bomb_Damage;
    private void Start()
    {

        Destroy(gameObject, 1.5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(Normal_Boss_Bomb_Damage);
                //Destroy(this.gameObject, 5);�÷��̾ ������ �����°� �ִϸ��̼� ������ �����ٲ���// 5�ʴ� �׳� ����
                //                col.GetComponent<Character>().KnuckBack(transform, KnuckBackForce, col.GetComponent<Character>().IsBoss);
                //collision.transform.parent.GetComponent<Character>().KnuckBack(transform, 5, collision.GetComponent<Character>().IsBoss);   
                Destroy(gameObject,1f);
            }
            if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject,3);
            }

        }
    }
}
