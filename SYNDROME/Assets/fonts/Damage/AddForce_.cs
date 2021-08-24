using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce_ : MonoBehaviour
{
    public GameObject player; //일단 플레이어 선언해둠
    public Rigidbody2D rg;
    public float PushPower;
    public Collider2D col;
    void Start()
    {
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void check()
    {
        if (this.gameObject.transform.position.x > player.transform.position.x) { Debug.Log("되긴함"); rg.AddForce(new Vector2(1f*PushPower,100)); }   //나중에 플레이어에 넣을 때 player.을 this.gameObject로 변경하면 될덧
        else if (this.gameObject.transform.position.x < player.transform.position.x) { rg.AddForce(new Vector2(-1f * PushPower,100)); }   //나중에 플레이어에 넣을 때 player.을 this.gameObject로 변경하면 될덧
    }
}
