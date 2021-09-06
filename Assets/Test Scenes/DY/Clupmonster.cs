using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clupmonster : MonoBehaviour
{
    Animator animator;
    public Transform player;
    public float speed;

    public float atkCooltime = 4;
    public float atkDelay;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //패트롤 기본 위치값 선정
    }

    private void Update()
    {
        if (atkDelay >= 0)
            atkDelay -= Time.deltaTime;
    }
    public Transform boxPos;
    public Vector2 boxSize;
    public void ClupAttack()
    {
        if (boxPos.localPosition.x > 0)
            boxPos.localPosition = new Vector2(boxPos.localPosition.x * -1, boxPos.localPosition.y);
        else
        {
            if (boxPos.localPosition.x < 0)
                boxPos.localPosition = new Vector2(Mathf.Abs(boxPos.localPosition.x), boxPos.localPosition.y);
        }
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxPos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("Damage");
            }
        }
    }
}
