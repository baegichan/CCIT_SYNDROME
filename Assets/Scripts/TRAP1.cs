using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TRAP1 : MonoBehaviour
{
    public Sprite[] Trap1_Sprite;
    public float cooltime;
    private float staticcooltime;
    public bool trapon=false;
    // Start is called before the first frame update
    void Start()
    {
        staticcooltime = cooltime;
    }

    // Update is called once per frame


    void Update()
    {
      if(trapon==true)
        {
           StartCoroutine(trapstart());

        }
    }
    
    public IEnumerator trapstart()
    {
        cooltime -= Time.deltaTime;
       
            if (cooltime >= staticcooltime * 6 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[0];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.0001f);

            }
            else if (cooltime >= staticcooltime * 5 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[1];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.3f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.45f);
            }
            else if (cooltime >= staticcooltime * 4 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[2];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.12f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.85f);
            }
            else if (cooltime >= staticcooltime * 3 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[3];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 1.13f);
            }
            else if (cooltime >= staticcooltime * 2 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[4];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.12f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.85f);
            }
            else if (cooltime >= staticcooltime * 1 / 7)
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[5];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, -0.3f);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.45f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().sprite = Trap1_Sprite[0];
                this.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                this.GetComponent<BoxCollider2D>().size = new Vector2(0.77f, 0.0001f);
            }
            if (cooltime <= 0)
            {
                cooltime = staticcooltime;
                 trapon = false;
            }
        
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D col)
    {


        if (col.tag == "Player")
        {

            //피격 무언가

        }

    }


}
