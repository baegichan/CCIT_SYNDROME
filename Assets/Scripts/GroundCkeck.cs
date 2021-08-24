using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCkeck : MonoBehaviour
{
    public GameObject holo;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")//땅에 닿는다면 스프라이트 나오도록
        {
            holo.gameObject.SetActive(true);

        }
    }
}
