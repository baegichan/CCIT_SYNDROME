using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_5way : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        //테스트용 스크립트 target 필요
        GetComponent<Bullet_Attack>().target = GameObject.Find("MainCharacter_Eden");
        GetComponent<Bullet_Attack>().Attack();
    }
}
