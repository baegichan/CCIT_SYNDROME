using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmoke : MonoBehaviour
{
    public GameObject B_Ball;
    public GameObject Player;
    public float B_Damage;
    public float B_Speed;
    Rigidbody2D Rigid;
    public Camera Cmr;
    void Start()
    {
        Cmr = Camera.main;   
    }

    void Update()
    {
        B_ThrowBall();
    }

    public void B_ThrowBall()
    {
        if(Input.GetMouseButtonDown(1))
        {
            Vector2 MouseP = Input.mousePosition;
            MouseP = Cmr.ScreenToWorldPoint(MouseP);
            Vector2 Point = Player.transform.position;
            Vector2 Dir = MouseP - Point;
            Dir = Dir.normalized;

            GameObject BB = Instantiate(B_Ball, Point, Quaternion.identity);
            BB.GetComponent<Smoke>().Dir = Dir;
            BB.GetComponent<Smoke>().PP = Point;
        }
    }
}
