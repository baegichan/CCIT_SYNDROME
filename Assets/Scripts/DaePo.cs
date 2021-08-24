using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaePo : MonoBehaviour
{
    public GameObject DaePoAL;
    public float cool, time;

    void Update()
    {
        atk();
    }

    void atk()
    {
        if(cool >= time)
        {
            if(transform.eulerAngles.y == 0)
            {
                Instantiate(DaePoAL, new Vector3(transform.position.x -1.08f, transform.position.y + 0.4f), transform.rotation);
            }
            else if (transform.eulerAngles.y == 180)
            {
                Instantiate(DaePoAL, new Vector3(transform.position.x + 1.08f, transform.position.y + 0.4f), transform.rotation);
            }

            cool = 0;
        }
        if(cool < time)
        {
            cool += Time.deltaTime;
        }
    }
}
