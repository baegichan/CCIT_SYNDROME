using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce_ : MonoBehaviour
{
    public GameObject player; //�ϴ� �÷��̾� �����ص�
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
        if (this.gameObject.transform.position.x > player.transform.position.x) { Debug.Log("�Ǳ���"); rg.AddForce(new Vector2(1f*PushPower,100)); }   //���߿� �÷��̾ ���� �� player.�� this.gameObject�� �����ϸ� �ɵ�
        else if (this.gameObject.transform.position.x < player.transform.position.x) { rg.AddForce(new Vector2(-1f * PushPower,100)); }   //���߿� �÷��̾ ���� �� player.�� this.gameObject�� �����ϸ� �ɵ�
    }
}
