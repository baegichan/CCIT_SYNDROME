using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame_attack : MonoBehaviour
{
    public GameObject ball;//액자에서 나오는 원거리 공격
    
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
