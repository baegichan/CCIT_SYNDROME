using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secbullet : MonoBehaviour
{
    public float speed;
    public Vector2 dir;
    public Collider2D col;
   // private SpriteRenderer fadein;

    // Start is called before the first frame update
    void Start()
    {
        //fadein = GameObject.Find("secbullet(Clone)").GetComponent<SpriteRenderer>();
        Physics2D.IgnoreLayerCollision(7,9);
        Destroy(this.gameObject, 0.3f);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (petbullet.cols.Length != 2 || petbullet.cols.Length != 1)
        {
            if (petbullet.cols.Length >= 3)
            {
                dir = petbullet.cols[2].transform.position - transform.position;
                transform.Translate(dir * Time.deltaTime * speed);
            }//������ : �ݶ��̴��� �ı��Ǿ ��� �����ϴ°� ��Ÿ�Ӱ� �ı��Ǵ� �ð��� �����ؼ� �ذ�
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.transform.tag == "enemy") //cols[2]�� �´� ��� �������
        {
          col.GetComponent<Monster>().dameged(2);
          // �̸����� �޴°� �ٽ� �ؾߵ� �Ʒ��� �ִ°� �ƴ�
            Debug.Log("�ι�°");
            Debug.Log(col.name);
           // fade();


        }
    }
    /*
    void fade()
    {
        Color tmp = fadein.color;
        tmp.a = 0;
        fadein.color = tmp;
    }
    */
}
//cols�� �ִ� cols[0]�� �����Ϸ��� �ϴµ� �����ϱ� ������ ���°� ����.
