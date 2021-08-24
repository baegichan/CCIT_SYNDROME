using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Animator OhDuck;
    public Transform target;
    public Transform shootPoint;
    public GameObject black;
    bool droped = false;
    Vector2 MonFront;

    public Vector2 SpawnPos;
    public Vector2 Patroll_L;
    public Vector2 Patroll_R;
    public float Pat_Dis;
    public int nextMove;

    public RaycastHit2D PlayerCheck;
    public RaycastHit2D PlatformCheck;
    public LayerMask isLayer;
    public float distance;
    public float atkDistance;
    public float Close_Atk;
    public bool PlayerFollow;
    public GameObject player;
    Vector2 direction;

    public Vector2 BoxSize;

    public float CoolTime;
    public float CurrentTIme;

    public enum char_state
    {
        idle = 0,
        patroll,
        MoveDirection,
        attack,
        dead
    }


    public char_state monster_state;
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
    public float speed;
    public int char_hp = 100;
    private Vector2 cha_start_position;

    public void Set_position(Vector2 new_position)
    {

        //굳이 쓸필요 없음.
        this.gameObject.transform.position = new_position;

    }
    public void Attack()
    {
        if (Close_Atk >= Vector2.Distance(transform.position, PlayerCheck.collider.transform.position))
        {
            nextMove = 1;
            OhDuck.SetFloat("Speed", 0);
            if (CurrentTIme <= 0)
            {
                OhDuck.SetBool("Attack", true);
                CurrentTIme = CoolTime;
            }
        }
    }

    public void Dead()
    {
        speed = 0;
        OhDuck.SetBool("Dead", true);
        Destroy(this.gameObject, 2f);
    }
    public void MoveDirection()
    {
        OhDuck.SetFloat("Speed", speed);
        CancelInvoke("NextMove");

        if (PlatformCheck.collider == null)
        {
            nextMove = 0;
            if (distance < Vector2.Distance(this.gameObject.transform.position, player.transform.position))
            {
                PlayerFollow = false;
            }
        }
        else if (PlatformCheck.collider != null)
        {
            if (transform.position.x < PlayerCheck.collider.transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);

                transform.Translate(Vector2.right * Time.deltaTime * speed);
            }
            else if (transform.position.x > PlayerCheck.collider.transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 180);

                transform.Translate(Vector2.right * 1 * Time.deltaTime * speed);
            }
        }
        Debug.Log("움직이는중");
    }

    public void Patroll()
    {
        OhDuck.SetFloat("Speed", speed);
        OhDuck.SetBool("Attack", false);

        if (PlatformCheck.collider == null)
        {
            if (transform.position.x < SpawnPos.x)
            {
                CancelInvoke("NextMove");
                nextMove = 0;
                Invoke("NextMove", 2);

                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (transform.position.x > SpawnPos.x)
            {
                CancelInvoke("NextMove");
                nextMove = 0;
                Invoke("NextMove", 2);

                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else if (PlatformCheck.collider != null)
        {
            if (nextMove == 1)
            {
                if (transform.position.x > Patroll_L.x && transform.position.x < Patroll_R.x)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else if (transform.position.x < Patroll_L.x)
                {
                    CancelInvoke("NextMove");
                    nextMove = 0;
                    Invoke("NextMove", 2);

                    transform.eulerAngles = new Vector2(0, 0);
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else if (transform.position.x > Patroll_R.x)
                {
                    CancelInvoke("NextMove");
                    nextMove = 0;
                    Invoke("NextMove", 2);

                    transform.eulerAngles = new Vector2(0, 180);
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
            }
            else if (nextMove == 0)
            {
                monster_state = char_state.idle;
                Invoke("NextMove", 2);
            }
        }
        Debug.Log("정찰중");

    }

    void NextMove()
    {
        if (transform.position.x > Patroll_L.x && transform.position.x < Patroll_R.x)
        {
            nextMove = 1;
        }
        else if (nextMove == 0)
        {
            Invoke("NextMove", 2);
        }
    }

    void RaycastManager()
    {
        direction = player.gameObject.transform.position - transform.position;
        direction.Normalize();
        PlayerCheck = Physics2D.Raycast(transform.position, direction, distance);
        Debug.DrawRay(transform.position, direction, Color.green);

        if (transform.eulerAngles.y == 180)
        {
            MonFront = new Vector2(transform.position.x * 1 - 0.8f, transform.position.y);

            PlatformCheck = Physics2D.Raycast(MonFront, Vector2.down, 3, LayerMask.GetMask("GroundMakLayer"));
        }
        else if (transform.eulerAngles.y == 0)
        {
            MonFront = new Vector2(transform.position.x + 0.8f, transform.position.y);

            PlatformCheck = Physics2D.Raycast(MonFront, Vector2.down, 3, LayerMask.GetMask("GroundMakLayer"));
        }
    }
    public void damaged(int damage)
    {
        Mathf.Clamp(char_hp -= damage, 0,100);
    }
    public void Idle()
    {
        OhDuck.SetFloat("Speed", 0);
        Debug.Log("대기중");
    }
    private void Start()
    {
        BulletPOS.MonPos = transform.position;
        SpawnPos = BulletPOS.MonPos;
        Patroll_Distance();
        NextMove();
        //몬스터
        // cha_start_position = this.gameObject.transform.position;
    }

    void FixedUpdate()
    {
        RaycastManager();
        Debug.DrawRay(MonFront, Vector2.down, Color.blue);
        CurrentTIme -= Time.deltaTime;
        if (PlatformCheck.collider != null)
        {
            if (PlayerCheck.collider != null)
            {
                if (PlayerCheck.collider.gameObject.CompareTag("Player"))
                {
                    PlayerFollow = true;
                }
                else
                {
                    PlayerFollow = false;
                }
            }
        }

        if (PlayerFollow == false)
        {
            monster_state = char_state.patroll;
            if (Vector2.Distance(transform.position, player.gameObject.transform.position) < atkDistance)
            {
                monster_state = char_state.attack;
            }
        }
        else if (PlayerFollow == true)
        {
            monster_state = char_state.MoveDirection;

            if (Vector2.Distance(transform.position, player.gameObject.transform.position) < atkDistance)
            {
                monster_state = char_state.attack;
            }
        }
    }
    void SpawnDarkEnergy()
    {
        
        int j = Random.Range(1, 4);
   
     
            for (int x = 0; x < j; x++)
            {
            

                Vector2 DarkPosition = new Vector2(transform.position.x - Random.Range(1.0f, 2.0f),  transform.position.y - Random.Range(0.0f, 0.3f));
                GameObject YamiNoDark = Instantiate(black, DarkPosition, Quaternion.identity);
                YamiNoDark.gameObject.name = "YamiNoDark[" + x + "]";
              
            }
        
    }

    private void Update()
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
                case char_state.MoveDirection:
                    MoveDirection();
                    break;
                case char_state.attack:
                    Attack();
                    break;
            }
        }
        else if (char_hp <= 0)
        
        {
            
            monster_state = char_state.dead; Dead();
            if(droped!=true)
            {
                SpawnDarkEnergy();
                droped = true;
            }
        
   
        }
    }

    void Patroll_Distance()
    {
        Patroll_L = new Vector2(SpawnPos.x - Pat_Dis, SpawnPos.y);
        Patroll_R = new Vector2(SpawnPos.x + Pat_Dis, SpawnPos.y);
    }
}
