using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lDron : MonoBehaviour
{
    public GameObject player;
    public GameObject LDron;
    public GameObject L_bullet;
    public static GameObject target;

    Vector2 LDronPos;

    public float speed;
    public float distance;
    public float ReturnDis;
    public float MinDistance;
    public float AtkDistance;
    public static bool Ak = false;
    Animator anim;
    public char_state pet_state = char_state.idle;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }
    public enum char_state
    {
        idle = 0,
        move,
        attack,
    }
    void Update()
    {
        switch (pet_state)
        {
            case char_state.idle:
                Idle();
                break;
            case char_state.move:
                Move();
                break;
            case char_state.attack:
                Attack();
                break;
        }

        DronPos();
    }
    void FixedUpdate()
    {
        if (target == null)
        {
            if (Vector2.Distance(LDronPos, LDron.transform.position) > distance)
            {
                pet_state = char_state.move;
            }
            else
            {
                pet_state = char_state.idle;
            }
        }
        else if (target != null)
        {
            pet_state = char_state.attack;
        }
    }

    void Idle()
    {
        anim.SetBool("Idle", true);
    }

    void Move()
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Skill", false);
        if (Vector2.Distance(LDronPos, LDron.transform.position) > distance)
        {
            if (MinDistance < Vector2.Distance(LDronPos, LDron.transform.position))
            {
                LDron.transform.position = Vector2.MoveTowards(LDron.transform.position, LDronPos, Time.deltaTime * speed * 10);

            }
            else if (MinDistance > Vector2.Distance(LDronPos, LDron.transform.position))
            {
                LDron.transform.position = Vector2.MoveTowards(LDron.transform.position, LDronPos, Time.deltaTime * speed);
            }

            //LDron.transform.Rotate(0, 0, 20);               //이동 중이라는 것을 보여주기 위해 임시로 사용 애니메이션 받을 시 애니메이션으로 대체
            //RDron.transform.Rotate(0, 0, 20);

            Flip();

        }
    }

    void Attack()
    {
        anim.SetBool("Skill", true);
        if (Vector2.Distance(player.transform.position, LDron.transform.position) < ReturnDis)
        {
            if (target == null)
            {
                pet_state = char_state.move;
            }
            else if (target != null)
            {
                if (target.transform.position.x > LDron.transform.position.x)
                {
                    LDron.transform.eulerAngles = new Vector2(0, 180);
                }
                else if (target.transform.position.x < LDron.transform.position.x)
                {
                    LDron.transform.eulerAngles = new Vector2(0, 0);
                }

                if (Vector2.Distance(target.transform.position, LDron.transform.position) > AtkDistance)
                {

                    LDron.transform.position = Vector2.MoveTowards(LDron.transform.position, target.transform.position, Time.deltaTime * speed * 3);
                    
                }
                else if (Vector2.Distance(target.transform.position, LDron.transform.position) < AtkDistance)
                {

                    if (Ak == true)
                    {

                        Instantiate(L_bullet, LDron.transform.position, Quaternion.identity);

                        Ak = false;
                    }
                }
            }
        }
        else if (Vector2.Distance(player.transform.position, LDron.transform.position) > ReturnDis)
        {
            pet_state = char_state.move;

            target = null;
        }
    }

    void DronPos()
    {
        LDronPos = player.transform.position;
        LDronPos.x -= 1.5f;
        LDronPos.y += 3f;
    }

    void Flip()
    {
        if (LDron.transform.position.x > LDronPos.x)
        {
            LDron.transform.eulerAngles = new Vector2(0, 0);
        }
        else if (LDron.transform.position.x < LDronPos.x)
        {
            LDron.transform.eulerAngles = new Vector2(0, 180);
        }
    }
}
