using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjapet : MonoBehaviour
{
    public float speed, playerdistance, jumpPw, teldistance, attackdistance;
    public GameObject Petbullet;
    public Transform player;
    public LayerMask GroundLy;
    Rigidbody2D rb;
    

    public static GameObject target;

    public static bool AK;
    //Animator anim;
    public void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(3, 6); //레이캐스트 충돌무시 (레이어 번호 적기)
        Physics2D.IgnoreLayerCollision(6, 8);
    }


    void Update()
    {
        Move();
        Attack();
    }
    private void Move()
    {
        if (Mathf.Abs(transform.position.x - player.position.x) > playerdistance)//플레이어와의 거리가 양수, 음수 가될 수 있으니 절대값으로 구한다
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);


            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, GroundLy); //직선 레이캐스트

            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(1 * FlipPet(), 1), 2f, GroundLy); //사선 레이캐스트

            if (player.position.y - transform.position.y <= 0) //플레이어가 아래있을떄는 레이캐스트 반환값을 비워줘야 올라가는걸 방지 할 수 있다 
            {
                hit2 = new RaycastHit2D();
            }
            if (hit1 || hit2)
            {
                rb.velocity = Vector2.up * jumpPw;
            }

            if (Vector2.Distance(player.position, transform.position) > teldistance)
            {
                transform.position = player.position;
                target = null;
                AK = false;
            }
        }
    }

    float FlipPet()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return 1;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }

    }

    void Attack()
    {
        if(Vector2.Distance(player.transform.position, transform.position) < teldistance)
        {
            if(target == null)
            {
                Move();
            }
            else if(target != null)
            {
                if (target.transform.position.x < transform.position.x)
                {
                    transform.eulerAngles = new Vector2(0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector2(0, 180);
                }
                if (target.transform.position.x > transform.position.x)
                {
                    transform.eulerAngles = new Vector2(0, 180);
                }
                else
                {
                    transform.eulerAngles = new Vector2(0, 0);
                }
                if (Vector2.Distance(target.transform.position, transform.position) > attackdistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed * 3);
                }
                else if (Vector2.Distance(target.transform.position, transform.position) < attackdistance)
                    if (AK == true)
                    {
                        Instantiate(Petbullet,transform.position, Quaternion.identity);
                        AK = false;
                    }
            }
            else if(Vector2.Distance(player.transform.position, transform.position) > teldistance)
            {
                Move();
            }
        }
    }
}
