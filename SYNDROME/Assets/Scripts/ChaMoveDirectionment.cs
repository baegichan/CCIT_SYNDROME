using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaMoveDirectionment : MonoBehaviour
{
    public Transform target;
    public Transform shootPoint;
    public float BulletSpd;

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

    public Transform CA_POS;
    public Vector2 BoxSize;

    public GameObject Bullet;
    public float CoolTime_L;
    public float CoolTime_C;
    public float CurrentTIme;

    public enum char_state
    {
        idle=0,
        patroll,
        MoveDirection,
        attack,
        dead
    }

    
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
    public int attack_point=10;
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
        if (PlatformCheck.collider == null)
        {
            nextMove = 0;
        }

        if (transform.position.x < player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        else if (transform.position.x > player.transform.position.x)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (Close_Atk < Vector2.Distance(transform.position, PlayerCheck.collider.transform.position))
        {
            if (CurrentTIme <= 0)
            {
                target = player.gameObject.transform;
                shootPoint = this.gameObject.transform;
                LaucherProjecttile();

                CurrentTIme = CoolTime_L;

                Debug.Log("원거리 공격");
            }
            else
            {
                CurrentTIme -= Time.deltaTime;
            }
        }
        else if (Close_Atk >= Vector2.Distance(transform.position, PlayerCheck.collider.transform.position))
        {
            if(CurrentTIme <= 0)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(CA_POS.position, BoxSize, 0);
                foreach (Collider2D col in collider2Ds)
                {
                    if(col.tag == "Player")
                    {
                       // col.GetComponent<Player>().Damage(1);
                        Debug.Log("근거리 공격");
                    }
                }
                //Animator.SetTrigger("atk");                  //애니메이션 추가 시 사용
                CurrentTIme = CoolTime_C;
            }
            else
            {
                CurrentTIme -= Time.deltaTime;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CA_POS.position, BoxSize);
    }

    public void Dead()
    {

        Debug.Log("사망");
    }
    public void MoveDirection()
    {
        CancelInvoke("NextMove");

        if (PlatformCheck.collider == null)
        {
            nextMove = 0;
            if (distance < Vector2.Distance(this.gameObject.transform.position, player.transform.position))
            {
                PlayerFollow = false;
            }
        }
        else if(PlatformCheck.collider != null)
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
        //플레이어 방향으로
    }

    public void Patroll()
    {
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
            else if(nextMove == 0)
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

            PlatformCheck = Physics2D.Raycast(MonFront, Vector2.down, 3, LayerMask.GetMask("Platform"));
        }
        else if (transform.eulerAngles.y == 0)
        {
            MonFront = new Vector2(transform.position.x + 0.8f, transform.position.y);

            PlatformCheck = Physics2D.Raycast(MonFront, Vector2.down, 3, LayerMask.GetMask("Platform"));
        }
    }

    public void Idle()
    {
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

        if (PlatformCheck.collider != null)
        {
            if (PlayerCheck.collider != null)
            {
                if(PlayerCheck.collider.gameObject.CompareTag("Player"))
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
        }
        else if(PlayerFollow == true)
        {
            monster_state = char_state.MoveDirection;

            if (Vector2.Distance(transform.position, player.gameObject.transform.position) < atkDistance)
            {
                monster_state = char_state.attack;
            }
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
  
    }

    void Patroll_Distance()
    {
        Patroll_L = new Vector2(SpawnPos.x - Pat_Dis, SpawnPos.y);
        Patroll_R = new Vector2(SpawnPos.x + Pat_Dis, SpawnPos.y);
    }

    void LaucherProjecttile()
    {
        float time = Vector3.Distance(target.position, shootPoint.position) / BulletSpd;

        Vector3 Vo = BulletPOS.CalculateVelcoity(target.position, transform.position, time);
        Rigidbody2D obj = Instantiate(Bullet, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        obj.velocity = Vo;
    }
}

