using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_attack : MonoBehaviour
{
    public GameObject ball;//���ڿ��� ������ ���Ÿ� ����
    
    //public int ball_speed =3;//���� ����ü ���󰡴� �ӵ�
    public Transform player;
    //BossScript bs;

    private void Start()
    {
        //bs = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossScript>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void new_ball()
    {
        Instantiate(ball, transform.position, Quaternion.identity);
    }



}
