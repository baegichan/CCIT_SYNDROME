using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petbullet : MonoBehaviour
{
    public GameObject secbullet;
    public Collider2D col;
    public float speed;
    public Transform enemy;
    public Vector2 dir;
    public static Collider2D[] cols;
    private SpriteRenderer fadein;
    
    void Start()
    {
        fadein = GameObject.Find("petbulle1(Clone)").GetComponent<SpriteRenderer>();
        Destroy(this.gameObject, 0.8f);
    }
    //문제점 : 콜라이더가 파괴되어도 계속 접근하는것
    void Update()
    {
        if (ninjapet.target == true) //null 레퍼런스 오류는 앞에 조건문을 써서 조건을 만족하면 실행되게 하면 해결된다
        {
            dir = ninjapet.target.transform.position - transform.position;
            transform.Translate(dir * Time.deltaTime * speed);
        }
        

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "enemy")
        {
            col.GetComponent<Monster>().dameged(1);
            secrange();
            secshoot();
            fade();
            //Destroy(this.gameObject);
            ninjapet.target = null;

        }
        else if (col.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    void secrange()
    {
        cols = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 10f, LayerMask.GetMask("enemy")); //마지막 부분에 레퍼런스에는 레이어 숫자를 쓰라고 했지만 해결방법은 직접 선언해 주어서 해결했다

        foreach (Collider2D collider in cols)
        {
            Debug.Log(collider);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    
    void secshoot()
    {
        if (cols.Length != 2 || cols.Length != 1)
        {
            Instantiate(secbullet, transform.position, Quaternion.identity);
        }
        else
        { System.Array.Clear(cols, 0, cols.Length); }
        Debug.Log("두번째 공격");
    }
 
    void fade()
    {
        Color tmp = fadein.color;
        tmp.a = 0;
        fadein.color = tmp;
    }
}

    


