using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puppy : MonoBehaviour

{
    public GameObject user;
    public int jump_count_max;
    public int[] jump_count;
    public float heal, time, cool, speed, distance, jumpPower, autoJump, telDistance;
    public GameObject[] puppy = new GameObject[3];
    public LayerMask groundLayer;
    Rigidbody2D rig_0, rig_1, rig_2;

    void Start()
    {
        rig_0 = puppy[0].GetComponent<Rigidbody2D>();
        rig_1 = puppy[1].GetComponent<Rigidbody2D>();
        rig_2 = puppy[2].GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(7, 8);
    }

    void Update()
    {
        move();
        swap();
        invoke();
        Heal();
        teleport();
    }

    public void Heal()
    {
        PlayerMovement player = user.GetComponent<PlayerMovement>();

        if (cool <= 0)
        {
            
            PlayerMovement.Hp += heal;
            if (PlayerMovement.Hp > PlayerMovement.Max_Hp)
            {
                PlayerMovement.Hp -= PlayerMovement.Hp - PlayerMovement.Max_Hp;
            }

            cool = time;
        }
        else
        {
            cool -= Time.deltaTime;
        }
    }

    void move()
    {
        if (Mathf.Abs(puppy[0].transform.position.x - user.transform.position.x) > distance)
        {
            puppy[0].transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);

            RaycastHit2D hit = Physics2D.Raycast(puppy[0].transform.position, puppy[0].transform.right, 0.5f, groundLayer);

            if (hit)
            {
                rig_0.velocity = Vector2.up * autoJump;
            }
        }

        if (Mathf.Abs(puppy[1].transform.position.x - user.transform.position.x) > distance)
        {
            puppy[1].transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);

            RaycastHit2D hit = Physics2D.Raycast(puppy[1].transform.position, puppy[1].transform.right, 0.5f, groundLayer);

            if (hit)
            {
                rig_1.velocity = Vector2.up * autoJump;
            }
        }

        if (Mathf.Abs(puppy[2].transform.position.x - user.transform.position.x) > distance)
        {
            puppy[2].transform.Translate(new Vector2(1, 0) * Time.deltaTime * speed);

            RaycastHit2D hit = Physics2D.Raycast(puppy[2].transform.position, puppy[2].transform.right, 0.5f, groundLayer);

            if (hit)
            {
                rig_2.velocity = Vector2.up * autoJump;
            }
        }
    }

    void invoke()
    {
        RaycastHit2D[] hit = new RaycastHit2D[3];

        hit[0] = Physics2D.Raycast(puppy[0].transform.position, puppy[0].transform.up * -1, 0.97f, groundLayer);
        if (hit[0] == true)
        {
            jump_count[0] = jump_count_max;
        }
        hit[1] = Physics2D.Raycast(puppy[1].transform.position, puppy[1].transform.up * -1, 0.85f, groundLayer);
        if (hit[1] == true)
        {
            jump_count[1] = jump_count_max;
        }
        hit[2] = Physics2D.Raycast(puppy[2].transform.position, puppy[2].transform.up * -1, 0.9f, groundLayer);
        if (hit[2] == true)
        {
            jump_count[2] = jump_count_max;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jump_count[0] > 0)
            {
                jump_count[0]--;
                Invoke("jump_0", 0.2f);
            }

            if (jump_count[1] > 0)
            {
                jump_count[1]--;
                Invoke("jump_1", 0.3f);
            }

            if (jump_count[2] > 0)
            {
                jump_count[2]--;
                Invoke("jump_2", 0.4f);
            }
        }
    }

    void jump_0()
    {
        rig_0.velocity = Vector2.up * jumpPower;
    }
    void jump_1()
    {
        rig_1.velocity = Vector2.up * jumpPower;
    }
    void jump_2()
    {
        rig_2.velocity = Vector2.up * jumpPower;
    }

    void teleport()
    {
        if (Vector2.Distance(user.transform.position, puppy[0].transform.position) > telDistance)
        {
            puppy[0].transform.position = user.transform.position;
        }

        if (Vector2.Distance(user.transform.position, puppy[1].transform.position) > telDistance)
        {
            puppy[1].transform.position = user.transform.position;
        }

        if (Vector2.Distance(user.transform.position, puppy[2].transform.position) > telDistance)
        {
            puppy[2].transform.position = user.transform.position;
        }
    }

    void swap()
    {
        if (puppy[0].transform.position.x - user.transform.position.x < 0)
        {
            puppy[0].transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            puppy[0].transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (puppy[1].transform.position.x - user.transform.position.x < 0)
        {
            puppy[1].transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            puppy[1].transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (puppy[2].transform.position.x - user.transform.position.x < 0)
        {
            puppy[2].transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            puppy[2].transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
