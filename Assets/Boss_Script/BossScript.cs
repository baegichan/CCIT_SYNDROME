using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossScript : MonoBehaviour
{

    public enum Boss_state
    {

        idle,//���� ������ ������ ����
        move,//���ݹ������� ���� ������
        attack,//���ݻ���
        dead,//���� �ֱ�
    }
    public Boss_state boss_state = Boss_state.idle;

    public float Boss_hp;//���� hp
    public float Boss_speed = 1;//���� speed // �� �����ѵ�?



    public float Attack2_cooltime = 20; // ���Ÿ� ���� ��Ÿ��
    public float Attack2_cool = 0;//���� ���� ��Ž
    public float Attack2_h = 0;//���ڰ� ��� �����ߴ���?


    public float bb;//���Ÿ� ���� ���� ��Ÿ��
    public Transform player;
    public int Set_Skill;//��ų ���� �ִϸ��̼� �̺�Ʈ Ȯ�ο�


    public GameObject bullet;//�������� �Ѿ�
    //public GameObject[] new_b;

    //public GameObject Right_Hand_gun;//ù��° ��
    //public GameObject Left_Hand_gun;//�ι�° ��
    //public GameObject Black_Fog;
    public GameObject Fog_zone;



    public float gun_cooltime = 0.1f;//0.1�ʿ� �ѹ��� 4�� ��� �� ���� ��Ÿ��
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

        if (bb < 0 && boss_state != Boss_state.attack)//bb�� ���Ÿ� ���� ��Ÿ�� 
        {

            Boss_speed = 0;//���� ���� ���Ÿ� ���ݽ� ��� ����


            if (Attack2_cool > 0f)//���� ���� ��Ž
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

            if (Attack2_h == 4) //10�� ��°� 4�� �ݺ�
            {
                bb = 30;
            }

        if(HP.value==0)
            {
                StartCoroutine(delete());
            }
        }
        /*
        if (Ball.number_of_Tri == 40)//��� ��ü �����
        {
            Set_Skill = 0;
            Attack2_cooltime = 20;
            Ball.number_of_Tri = 0;
            Boss_speed = 1;//���� ���� ���Ÿ� ���� ������ �ٽ� �����·� �ǵ�����
            fr_ar.SetActive(false);
            anim.SetBool("isSetSkill", false);
        }*/
        if (player.position.x < transform.position.x && HP.value > 0) // �ӵ� ����� �ҵ�?
        {
            float Left_Dir = -2f;
            transform.localScale = new Vector3(Left_Dir, transform.localScale.y, transform.localScale.z);

        }//���� ��ȯ
        else if (player.position.x > transform.position.x &&HP.value>0)
        {

            float Right_Dir = 2f;
            transform.localScale = new Vector3(Right_Dir, transform.localScale.y, transform.localScale.z);
        }//������ȯ
    }
    void asd()// �ִϸ��̼� ���� Set_Skill�� �̺�Ʈ�� �־����
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
                bss.ara();//������ ���� �÷���
            }
        }
    }

    public void idle()
    {

        //ó���� ������ ���� �� ��¼�� �ϰ� �װ� �Ѿ�� �Ѿ�� if�� �ɾ��ٰ�
        //�ٵ� ���� �ϰ͵� �ȳ���
        boss_state = Boss_state.move;



    }
    public void move()
    {
        anim.SetBool("isMove", true);
        if (player.position.x < transform.position.x) // �ӵ� ����� �ҵ�?
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
            Attack2_cooltime -= Time.deltaTime;//attack ���� ������ ���Ÿ� ���� ��Ÿ���� �پ��� �����ϴ�
            if (Attack2_cooltime < 0)
            {
                bb = Attack2_cooltime;//���Ÿ� ��Ÿ�� 0 ����� Ȯ��
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
        //boss_state = Boss_state.dead;// ���� ���� �ٲ��ְ�
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
    public void Make_Black_Fog()//�������� ���� ������ ���� �Ȱ� ����
    {
        //GameObject BF = Instantiate(Black_Fog, Fog_zone.transform.position, Quaternion.identity);
        //BF.transform.rotation = Quaternion.Euler(0, 0, 90);
        //�÷��̾� 
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