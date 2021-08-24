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
            }//문제점 : 콜라이더가 파괴되어도 계속 접근하는것 쿨타임과 파괴되는 시간을 지연해서 해결
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.transform.tag == "enemy") //cols[2]가 맞는 경우 사라지게
        {
          col.GetComponent<Monster>().dameged(2);
          // 이름으로 받는거 다시 해야됨 아래에 있는거 아님
            Debug.Log("두번째");
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
//cols에 있는 cols[0]을 참조하려고 하는데 없으니깐 오류가 나는거 같다.
