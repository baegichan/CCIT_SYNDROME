using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClupMonster : MonoBehaviour
{
    //prameter
    public float speed;
    public float patrolSpeed;
    public float atkCooltime = 4;
    public float atkDelay;

    //refernce
    public Transform player;
    public Animator anim;
    public Vector2 first;
    public Vector2 boxSize;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Transform playerCheck;
    public Transform boxpos;
    public Vector2 direction;
    public float distance;

    //turn state
    public bool filp;
    public bool patroll;
    public bool trace;

    //Debug

    void Start()
    {
        filp = true;
        patroll = true;
        trace = false;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (patroll == true)
        {
            Patroll();
        }
        //PlayerCheck();
        first = transform.position;
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
        if (anim.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
        }
        else
        {
            if (boxpos.localPosition.x < 0)
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x * 1), boxpos.localPosition.y);
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D col in collider2Ds)
        {
            if (col.tag == "Player")
            {
                Debug.Log("damage1");
            }
        }
    }

    public void Patroll()
    {
        transform.Translate(Vector2.right * patrolSpeed * Time.deltaTime);
        Filp();
    }

    public void Filp()
    {
        RaycastHit2D groundcheck = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);
        if (groundcheck.collider == false)
        {
            if (filp == true)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                filp = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                filp = true;
            }
        }
    }
}
