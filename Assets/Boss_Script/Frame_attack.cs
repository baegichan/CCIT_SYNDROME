using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_attack : MonoBehaviour
{
    public GameObject ball;//���ڿ��� ������ ���Ÿ� ����
    
    private void Start()
    {
        Invoke("Ball", 2f);
    }
    void Ball()
    {
        StartCoroutine(BB());
    }

    IEnumerator BB()
    {
        Instantiate(ball, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
    }

}
