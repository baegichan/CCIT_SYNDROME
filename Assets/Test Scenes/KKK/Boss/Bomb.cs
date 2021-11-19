using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bomb : MonoBehaviour
{
    [Range(0, 1)]
    public float Test;
   

    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    public GameObject Player;

    public int Weight;
    public int Height;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Transform Player_Transform = Player.GetComponent<TestPlayer>().SelectChar.transform;
        

        p1 = transform.position;
        p2 = new Vector3(p1.x, p1.y + Height, p1.z);
        p3 = new Vector3(p2.x - Weight, p2.y, p2.z);
        p4 = Player_Transform.position;

    }

    public void Update()
    {
        transform.position = BezirTest(p1, p2, p3, p4, Test);
        Test += Time.deltaTime;
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
                collision.transform.parent.GetComponent<Character>().Damage(20);
                Destroy(gameObject);
                //Destroy(this.gameObject, 5);플레이어가 밟으면 터지는거 애니메이션 끝나면 없애줄꺼임// 5초는 그냥 설정
            }
            if (collision.CompareTag("Ground"))
            {
                
                //Destroy(gameObject);
            }
        
        }
    }
}
