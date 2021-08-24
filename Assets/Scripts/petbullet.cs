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
    //������ : �ݶ��̴��� �ı��Ǿ ��� �����ϴ°�
    void Update()
    {
        if (ninjapet.target == true) //null ���۷��� ������ �տ� ���ǹ��� �Ἥ ������ �����ϸ� ����ǰ� �ϸ� �ذ�ȴ�
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
        cols = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 10f, LayerMask.GetMask("enemy")); //������ �κп� ���۷������� ���̾� ���ڸ� ����� ������ �ذ����� ���� ������ �־ �ذ��ߴ�

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
        Debug.Log("�ι�° ����");
    }
 
    void fade()
    {
        Color tmp = fadein.color;
        tmp.a = 0;
        fadein.color = tmp;
    }
}

    


