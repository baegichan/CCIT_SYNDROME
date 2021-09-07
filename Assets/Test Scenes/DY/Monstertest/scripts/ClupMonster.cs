using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClupMonster : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Animator anim;
    public Vector2 first;

    public float atkCooltime = 4;
    public float atkDelay;

    public Transform boxpos;
    public Vector2 boxSize;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        first = transform.position;
    }

    void Update()
    {
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
    }

    public void DirectionClupmonster(float target, float baseobj)
    {
        if (target < baseobj)
            anim.SetFloat("Direction", -1);
        else
            anim.SetFloat("Direction", 1);
    }

    public void ClupAttack()
    {
        if(anim.GetFloat("Direction")==-1)
        {
            if(boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D col in collider2Ds)
        {
            if(col.tag =="Player")
            {
                Debug.Log("damage1");
            }
        }
    }
}
