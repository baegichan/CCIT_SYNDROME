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

    void anim_On()//지뢰 터지는 애니메이션 실행 예정
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
                //터지는 애니메이션
                collision.GetComponent<Character>().Damage(50);
                //Destroy(this.gameObject, 5);플레이어가 밟으면 터지는거 애니메이션 끝나면 없애줄꺼임// 5초는 그냥 설정
            }
        }
    }
}
