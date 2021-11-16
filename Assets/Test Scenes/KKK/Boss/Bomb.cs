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

            //터지는 애니메이션



            Destroy(this.gameObject);
        }
    }
}
