using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossScript : MonoBehaviour
{

    public enum Boss_state
    {

        idle,//보스 공격후 딜레이 상태
        move,//공격범위까지 가는 움직임
        attack,//공격상태
        dead,//보스 주금
    }
    public Boss_state boss_state = Boss_state.idle;

    public float Boss_hp;//보스 hp
    public float Boss_speed = 1;//보스 speed // 딱 적당한듯?



    public float Attack2_cooltime = 20; // 원거리 공격 쿨타임
    public float Attack2_cool = 0;//액자 공격 쿨탐
    public float Attack2_h = 0;//액자가 몇번 공격했는지?


    public float bb;//원거리 공격 남은 쿨타임
    public Transform player;
    public int Set_Skill;//스킬 공격 애니메이션 이벤트 확인용


    public GameObject bullet;//보스몬스터 총알
    //public GameObject[] new_b;

    //public GameObject Right_Hand_gun;//첫번째 총
    //public GameObject Left_Hand_gun;//두번째 총
    //public GameObject Black_Fog;
    public GameObject Fog_zone;



    public float gun_cooltime = 0.1f;//0.1초에 한번씩 4번 쏘는 총 공격 쿨타임
    public int bullet_count = 0;
    public GameObject fr_ar;
    Black_Stone_Script bss;

    public Animator anim;
    private void Awake()
    {
        fr_ar = GameObject.FindGameObjectWithTag("Frame");

    }
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        //sp = GetComponent<SpriteRenderer>();
        fr_ar.SetActive(false);
        bss = GameObject.Find("Black_Stone").GetComponent<Black_Stone_Script>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {

        if (bb < 0 && boss_state != Boss_state.attack)//bb는 원거리 공격 쿨타임 
        {

            Boss_speed = 0;//보스 몬스터 원거리 공격시 잠깐 멈춤


            if (Attack2_cool > 0f)//액자 공격 쿨탐
            {
                Attack2_cool -= Time.deltaTime;
            }

            if (Attack2_cool < 0f)
            {
                fr_ar.SetActive(true);
                anim.SetBool("isSetSkill", true);

                if (Set_Skill == 1)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        //fr_ar.GetComponent<Frame_attack>().new_ball();
                    }
                    Attack2_h++;
                    Attack2_cool = 1f;
                }
            }

            if (Attack2_h == 4) //10개 쏘는거 4번 반복
            {
                bb = 30;
            }

        if(HP.value==0)
            {
                StartCoroutine(delete());
            }
        }
        /*
        if (Ball.number_of_Tri == 40)//모든 구체 사라짐
        {
            Set_Skill = 0;
            Attack2_cooltime = 20;
            Ball.number_of_Tri = 0;
            Boss_speed = 1;//보스 몬스터 원거리 공격 끝나고 다시 원상태로 되돌리기
            fr_ar.SetActive(false);
            anim.SetBool("isSetSkill", false);
        }*/
        if (player.position.x < transform.position.x && HP.value > 0) // 속도 늦춰야 할듯?
        {
            float Left_Dir = -2f;
            transform.localScale = new Vector3(Left_Dir, transform.localScale.y, transform.localScale.z);

        }//방향 전환
        else if (player.position.x > transform.position.x &&HP.value>0)
        {

            float Right_Dir = 2f;
            transform.localScale = new Vector3(Right_Dir, transform.localScale.y, transform.localScale.z);
        }//방향전환
    }
    void asd()// 애니메이션 지연 Set_Skill에 이벤트로 넣어놓음
    {
        Set_Skill = 1;


    }
    public Camera_movement camera;
    private void FixedUpdate()
    {
        if (camera.Map_Code == 4)
        {
            if (Boss_hp > 0)
            {

                switch (boss_state)
                {
                    case Boss_state.idle:
                        idle();
                        break;
                    case Boss_state.move:
                        move();
                        break;
                    case Boss_state.attack:
                        attack();
                        break;

                }
            }
            else if (Boss_hp <= 0)
            {
                dead();
                bss.ara();//검은돌 위로 올려줌
            }
        }
    }

    public void idle()
    {

        //처음에 보스가 무슨 말 어쩌고 하고 그거 넘어가면 넘어가는 if문 걸어줄곳
        //근데 아직 암것도 안나옴
        boss_state = Boss_state.move;



    }
    public void move()
    {
        anim.SetBool("isMove", true);
        if (player.position.x < transform.position.x) // 속도 늦춰야 할듯?
        {
            transform.position += Vector3.left * (Boss_speed * Time.deltaTime);

        }
        else if (player.position.x > transform.position.x)
        {

            transform.position += Vector3.right * (Boss_speed * Time.deltaTime);
        }
        else if (player.position.x == transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);

        }
    }
    public void attack()
    {
        if (Attack2_cooltime > 0)
        {
            Attack2_cooltime -= Time.deltaTime;//attack 상태 에서만 원거리 공격 쿨타임이 줄어들게 했읍니다
            if (Attack2_cooltime < 0)
            {
                bb = Attack2_cooltime;//원거리 쿨타임 0 됬는지 확인
            }
        }
    }
    public Slider HP;
    public void damaged(int damage)
    {
        Mathf.Clamp(Boss_hp -= damage, 0, 2000);
        HP.value = Boss_hp;
    }

    public void dead()
    {
        //boss_state = Boss_state.dead;// 보스 상태 바꿔주고
        anim.SetBool("isDead", true);

    }
    public void Turn_move()
    {
        boss_state = Boss_state.move;
        anim.SetBool("isAttack", false);
        anim.SetBool("isMove", true);
    }
    /*public void Turn_idle()
    {
        boss_state = Boss_state.idle;
    }*/
    public void Turn_attack()
    {
        boss_state = Boss_state.attack;
        anim.SetBool("isMove", false);
        anim.SetBool("isAttack", true);


    }
    IEnumerator delete()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
    public void Turn_dead()
    {
        boss_state = Boss_state.dead;

    }

    public void Stomping()
    {
        anim.SetBool("isStomping", true);
        //Invoke("Sef_false_Stomping", 4f);
    }
    public void Make_Black_Fog()//근접공격 보스 주위에 검은 안개 생성
    {
        //GameObject BF = Instantiate(Black_Fog, Fog_zone.transform.position, Quaternion.identity);
        //BF.transform.rotation = Quaternion.Euler(0, 0, 90);
        //플레이어 
        Fog_zone.SetActive(true);
        Invoke("Set_false", 1.5f);
    }
    public void Set_false()
    {
        Fog_zone.SetActive(false);

    }
    public void Sef_false_Stomping()
    {
        anim.SetBool("isStomping", false);
    }
    public void Reback_Speed()
    {
        Boss_speed = 1;
    }
}