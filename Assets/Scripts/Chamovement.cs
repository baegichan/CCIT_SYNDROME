using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chamovement : MonoBehaviour
{
    public enum char_state
    {

        idle,
        patroll,
        move,
        attack,
        dead,
        back_start_position


    }
    Rigidbody2D rigid;
    //SpriteRenderer spriterenderer;
    Animator anim;
    public char_state monster_state = char_state.idle;
    /*
    public int char_hp
    {
        get
        {
            return char_hp;
        }
        set
        {
            if(value<=0)
            {
                char_hp = 0;
            }
            else 
            {
                char_hp = value;
            }
        }
    }
    */
    //원하는대로 초기화 필요
    public int attack_point = 10;
    public int speed = 5;
    public int char_hp = 100;
    public int nextMove = 0;
   
    private Vector2 cha_start_position;

    public GameObject bullet;
    public GameObject bulletParent;

    private Transform player;//플레이어 위치
    public forchecking forcheck;



    public void Set_position(Vector3 new_position)
    {

        //굳이 쓸필요 없음.
        this.gameObject.transform.position = new_position;




    }
    public void Attack()
    {
        anim.SetBool("isAttack", true);
        rigid.velocity = new Vector2(0, 0);

        speed = 0;

        Debug.Log("공격");

    }
    public void Shotbullet()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
        
        Invoke("Reback_Speed", 1);
    }
    public void Reback_Speed()
    {
        speed = 5;
    }
    public void Dead()
    {

        Debug.Log("사망");
    }
    public void check()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= 9f)
        {
            Idle();

            monster_state = char_state.attack;

        }
    }
  
    public void Move()
    {
        speed = 5;
        anim.SetBool("isTrace", true);
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * 0.05f);
        //속도는 물어보고 설정
        check();
        Debug.Log("움직이는중");


    }
    public void Patroll()
    {
        anim.SetBool("isPatroll", true);
        rigid.velocity = new Vector2(nextMove * 3, rigid.velocity.y);
      
        if (rigid.velocity.x > 0.1f)
        {
            this.transform.localScale = new Vector2(1,this.transform.localScale.y);
            //원거리 몬스터 크키가 1이여서 1로 해줌
        }
        else
        {
            this.transform.localScale = new Vector2(-1, this.transform.localScale.y);
        }
        Debug.Log("정찰중");

    }
    public void Idle()
    {
        //StopCoroutine("MonsterAI");
        nextMove = 0;
        //nextMove가 0일 때 
        Debug.Log("대기중");


    }
    public void Back_Start_Position()
    {
        //rigid.velocity = new Vector2(0, 0);
        anim.SetBool("isTrace", false);
        anim.SetBool("isPatroll", true);
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2 (this.transform.position.x,cha_start_position.y), 0.05f);
            if (this.transform.position.y == cha_start_position.y)
            {
                Turn_patroll();
            }
        
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //spriterenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine("MonsterAI");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        //몬스터
        cha_start_position = this.gameObject.transform.position;


    }
    private void Update()
    {
       
        check();

    }
    private void FixedUpdate()
    {


        if (char_hp > 0)
        {
            switch (monster_state)
            {
                case char_state.idle:
                    Idle();
                    break;
                case char_state.patroll:
                    Patroll();
                    break;
                case char_state.move:
                    Move();
                    break;
                case char_state.attack:
                    Attack();
                    break;
                case char_state.back_start_position:
                    Back_Start_Position();
                    break;


            }

        }
        if(char_hp <= 0)
        {
            monster_state = char_state.dead;
            anim.SetBool("isDead", true);
        }
        if (monster_state != char_state.attack)
        {
            anim.SetBool("isAttack", false);
        }


    }
    IEnumerator MonsterAI()
    {
        nextMove = Random.Range(-1, 2);
        //float time = Random.Range(2f, 5f);
        yield return new WaitForSeconds(3f);
        StartCoroutine("MonsterAI");

    }


    public void Turn_move()
    {
        anim.SetBool("isPatroll", false);
       
        this.monster_state = char_state.move;
    }
    public void Turn_patroll()
    {

        this.monster_state = char_state.patroll;
    }
    public bool Attack_state()
    {
        if (monster_state == char_state.attack)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Turn_back_start_position()
    {

          this.monster_state = char_state.back_start_position;
    }

}

