using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCkeck : MonoBehaviour
{
    public GameObject holo;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")//���� ��´ٸ� ��������Ʈ ��������
        {
            holo.gameObject.SetActive(true);

        }
    }
}
