using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abycss_Explosion : MonoBehaviour
{
    public int Abycss_Bomb_Damage;
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
                collision.transform.parent.GetComponent<Character>().Damage(Abycss_Bomb_Damage);
                //Destroy(this.gameObject, 5);플레이어가 밟으면 터지는거 애니메이션 끝나면 없애줄꺼임// 5초는 그냥 설정
                Destroy(gameObject, 3);
            }
            if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject, 3);
            }

        }
    }
}
