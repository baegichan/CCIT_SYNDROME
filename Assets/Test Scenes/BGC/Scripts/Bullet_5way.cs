using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_5way : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //�׽�Ʈ�� ��ũ��Ʈ target �ʿ�
        GetComponent<Bullet_Attack>().target = GameObject.Find("MainCharacter_Eden");
        GetComponent<Bullet_Attack>().Attack();
    }
}
