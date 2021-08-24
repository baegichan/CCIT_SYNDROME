using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healpet : MonoBehaviour
{
    public float speed, playerdistance, jumpPw, teldistance, HEALDistance;
    public Transform playerTF;
    public LayerMask GroundLy;
    public float cooltime;
    Rigidbody2D rb;
    public float curtime;
    //Animator anim;
    public void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //playerTF = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(3, 6); //����ĳ��Ʈ �浹���� (���̾� ��ȣ ����)
        Physics2D.IgnoreLayerCollision(6, 8);
    }


    void Update()
    {
        Move();
        Healing();
    }
    private void Move()
    {
        if (Mathf.Abs(transform.position.x - playerTF.position.x) > playerdistance)//�÷��̾���� �Ÿ��� ���, ���� ���� �� ������ ���밪���� ���Ѵ�
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);


            RaycastHit2D hit1 = Physics2D.Raycast(transform.position, transform.right * -1f, 0.5f, GroundLy); //���� ����ĳ��Ʈ

            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(1 * FlipPet(), 1), 2f, GroundLy); //�缱 ����ĳ��Ʈ

            if (playerTF.position.y - transform.position.y <= 0) //�÷��̾ �Ʒ��������� ����ĳ��Ʈ ��ȯ���� ������ �ö󰡴°� ���� �� �� �ִ� 
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
    
    void Healing()
    {
        if(PlayerMovement.Hp >= PlayerMovement.Max_Hp)
        {
            Move();
        }
        else if(PlayerMovement.Hp < PlayerMovement.Max_Hp)
        {
            heal();
        }
       
    }

    void heal()
    {
        if (curtime <= 0)
        {
            PlayerMovement.Hp += 100;
            Debug.Log("������������");
            curtime = cooltime;
        }
        else
        {
            curtime -= Time.deltaTime;
        }
    }
}