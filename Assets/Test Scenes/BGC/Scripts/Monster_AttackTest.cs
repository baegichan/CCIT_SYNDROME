using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster_AttackTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    //�׽�Ʈ�� ��ũ��Ʈ
        GetComponent<Bullet_Attack>().CycleAttack(GameObject.Find("MainCharacter_Eden"));
    }
}
