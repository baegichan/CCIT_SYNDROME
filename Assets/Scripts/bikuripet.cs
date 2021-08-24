using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bikuripet : MonoBehaviour
{
    public float speed, playerdistance, jumpPw, teldistance;
    public static float bikuridistance = 3;
    public Transform playerTF;
    public LayerMask GroundLy;
    public float cooltime;
    public Transform monsterTF;
    Rigidbody2D rb;
    public float curtime;
    //Animator anim;
    public void Start()
    {
        monsterTF = GameObject.Find("monster").transform;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        playerTF = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(3, 6); //레이캐스트 충돌무시 (레이어 번호 적기)
        Physics2D.IgnoreLayerCollision(6, 8);
    }


    void Update()
    {
        Move();
        bikuring();
    }
    private void Move()
    {
        if (Mathf.Abs(transform.position.x - playerTF.position.x) > playerdistance)//플레이어와의 거리가 양수, 음수 가될 수 있으니 절대값으로 구한다
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);


            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, GroundLy); //직선 레이캐스트

            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(1 * FlipPet(), 1), 2f, GroundLy); //사선 레이캐스트

            if (playerTF.position.y - transform.position.y <= 0) //플레이어가 아래있을떄는 레이캐스트 반환값을 비워줘야 올라가는걸 방지 할 수 있다 
            {
                hit2 = new RaycastHit2D();
            }
            if (hit1 || hit2)
            {
                rb.velocity = Vector2.up * jumpPw;
            }

            if (Vector2.Distance(playerTF.position, transform.position) > teldistance)
            {
                transform.position = playerTF.position;
            }
        }
    }

    float FlipPet()
    {
        if (transform.position.x - playerTF.position.x < 0)
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

    void bikuring()
    {
      //  if()

    }

    void bikuri()
    {
        if (curtime <= 0)
        {
            
            Debug.Log("비쿠리시타");
            curtime = cooltime;
        }
        else
        {
            curtime -= Time.deltaTime;
        }
    }
}