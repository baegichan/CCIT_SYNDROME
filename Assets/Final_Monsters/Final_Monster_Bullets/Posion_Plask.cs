using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posion_Plask : MonoBehaviour
{
    [Range(0, 1)]
    public float Test;


    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    public GameObject Player;
    //public GameObject Explosion;

    public int Weight;
    public int Height;
    public int Height2;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //Player = GameObject.FindGameObjectWithTag("Player").transform.parent.gameObject;
        Transform Player_Transform = Player.GetComponent<TestPlayer>().SelectChar.transform;


        p1 = transform.position;
        p2 = new Vector3(p1.x, p1.y + Height, p1.z);
        p3 = new Vector3(Player_Transform.position.x, Player_Transform.position.y + Height2, Player_Transform.position.z);
        p4 = Player_Transform.position;

    }

    public void Update()
    {
        transform.position = BezirTest(p1, p2, p3, p4, Test);
        Test += Time.deltaTime;

        if (Test >= 1)
        {
            //Instantiate(Explosion, this.transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
        transform.Rotate(new Vector3(0, 0, -1000f * Time.deltaTime));

    }
    public Vector3 BezirTest(
        Vector3 P_1,
        Vector3 P_2,
        Vector3 P_3,
        Vector3 P_4,
        float Value
        )
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, Value);
        Vector3 B = Vector3.Lerp(P_2, P_3, Value);
        Vector3 C = Vector3.Lerp(P_3, P_4, Value);

        Vector3 D = Vector3.Lerp(A, B, Value);
        Vector3 E = Vector3.Lerp(B, C, Value);

        Vector3 F = Vector3.Lerp(D, E, Value);
        return F;
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground"))
        {
            if (collision.CompareTag("Player"))
            {
                collision.transform.parent.GetComponent<Character>().Damage(4);
                Destroy(gameObject);
            }
            if (collision.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }

        }
    }
}

