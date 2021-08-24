using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueShield : MonoBehaviour
{
    public GameObject user, body, face, Shield;
    public float speed, distance, jumpPower, autoJump, telDistance, Gage, Gage_max, gravity;
    public bool Open_Shield;
    public LayerMask groundLayer;
    Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 6);
    }

    void Update()
    {
        if (Open_Shield == false)
        {
            move();
            swap();
            invoke();
        }

        teleport();
        shield();
    }

    void move()
    {
        if (Mathf.Abs(transform.position.x - user.transform.position.x) > distance)
        {
            transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);
            body.transform.eulerAngles = new Vector3(0, 0, 1000000 * Time.deltaTime);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.5f, groundLayer);

            if (hit)
            {
                rig.velocity = Vector2.up * autoJump;
            }
        }
    }

    void invoke()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("jump", 0.2f);
        }
    }

    void jump()
    {
        rig.velocity = Vector2.up * jumpPower;
    }

    void teleport()
    {
        if (Vector2.Distance(user.transform.position, transform.position) > telDistance)
        {
            transform.position = user.transform.position;
        }
    }

    void swap()
    {
        if (transform.position.x - user.transform.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void shield()
    {
        if(Gage >= Gage_max)
        {
            Open_Shield = true;
            rig.gravityScale = 0;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else if(Gage <= 0)
        {
            Open_Shield = false;
            rig.gravityScale = gravity;
            GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if(Open_Shield == false && Gage < Gage_max)
        {
            Shield.SetActive(false);
            Gage += 30 * Time.deltaTime;
        }
        else if(Open_Shield == true)
        {
            Shield.SetActive(true);
            if(user.transform.eulerAngles.y == 0)
            {
                transform.position = new Vector2(user.transform.position.x + 0.9f, user.transform.position.y);
            }
            else if (user.transform.eulerAngles.y == 180)
            {
                transform.position = new Vector2(user.transform.position.x - 0.9f, user.transform.position.y);
            }
            Gage -= Time.deltaTime;
        }
        else if(Open_Shield == true && Gage <= 0)
        {
            Shield.SetActive(false);
            Gage += 30 * Time.deltaTime;
        }
    }
}
