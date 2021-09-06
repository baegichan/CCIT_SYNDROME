using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public GameObject Sword;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            EvilSword.Spawn =  GameObject.FindWithTag("Evil Sword Spawn").GetComponent<Transform>();
            Instantiate(Sword, EvilSword.Spawn);
            Destroy(this.gameObject);

        }
    }
}
