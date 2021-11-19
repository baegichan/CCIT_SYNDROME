using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    /*
    Rigidbody2D rig;

    public GameObject Player;
    Transform aa;

    Vector3 cc;

    public float BombSpeed;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        aa = Player.GetComponent<TestPlayer>().SelectChar.transform;
        cc = new Vector2(aa.position.x, aa.position.y);

        rig.AddForce(new Vector2(cc.x - this.transform.position.x, cc.y - this.transform.position.y) * BombSpeed);
    }
    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(rig.velocity.y, rig.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    *
    /*
    public Transform Bomb_Dir;
    public GameObject Player;
    public Transform Player_Dir;
    Transform aa;

    Vector3 cc;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        aa = Player.GetComponent<TestPlayer>().SelectChar.transform;


        cc = new Vector2(aa.position.x, aa.position.y);

    }
    Vector3 bb;
    void Lookat()
    {
        bb = new Vector2(cc.x - this.transform.position.x , cc.y - this.transform.position.y);

        //Bomb_Dir.right = bb;

    }
    private void Update()
    {
        Lookat();
        this.GetComponent<Rigidbody2D>().velocity = bb * 10;
    }
    */
    
    /*
    public Transform sunrise; //������ ������ġ
    public Transform sunset; //������ ������ġ
    Vector3 aa;
    public GameObject Player;
    public float journeyTime = 1000f; //������ġ���� ������ġ���� �����ϴ� �ð�, ���� �������� ������ ����.
    private float startTime;
    public float reduceHeight = 3f; //Center���� ���̱�, �ش� ���� �������� �������� ���̴� ��������.

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        startTime = Time.time;
        sunrise = this.transform;
        //sunset = Player.GetComponent<TestPlayer>().SelectChar.transform;//�÷��̾� ��ġ �޾ƿü� �ְ�
        aa = Player.GetComponent<TestPlayer>().SelectChar.transform.position;



    }
    Vector3 before;
    void Update()
    {
        Vector3 center = (sunrise.position + aa) * 0.01F; //Center ����ŭ ���� �ö󰣴�.
        center -= new Vector3(0, 0.1f * reduceHeight, 0); //y���� ���̸� ���̰� ��������.
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = aa - center;
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, 0.05f);
        transform.position += center;
         

        if (Vector3.Distance(transform.position, setRelCenter) == 0)
        {
            Destroy(gameObject);
        }

        
    }
    
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(20);
                Destroy(gameObject);
                //Destroy(this.gameObject, 5);�÷��̾ ������ �����°� �ִϸ��̼� ������ �����ٲ���// 5�ʴ� �׳� ����
            }
            if (collision.CompareTag("Ground"))
            {
                
                //Destroy(gameObject);
            }
        
        }
    }
}
