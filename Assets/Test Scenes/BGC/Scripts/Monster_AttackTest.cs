using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster_AttackTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    //테스트용 스크립트
        GetComponent<Bullet_Attack>().CycleAttack(GameObject.Find("MainCharacter_Eden"));
    }
}
