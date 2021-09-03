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
            EvilSword.Player =  GameObject.FindWithTag("Player").GetComponent<Transform>();
            Instantiate(Sword, EvilSword.Player);
            Destroy(this.gameObject);

        }
    }
}
