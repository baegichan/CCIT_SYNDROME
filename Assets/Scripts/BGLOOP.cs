using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGLOOP : MonoBehaviour
{

    public GameObject[] loopbgs;
    public float BGspeed;
    public float x_lengh;
    //speed~ give me what i need~ yeah
    private int BGcount;
    private Vector3[] BGlotation;
    private float x_move_size;

    private void Start()
    {
        BGcount = loopbgs.Length;
        BGlotation = new Vector3[BGcount];
        for (int i = 0; i < BGcount; i++)
        {
          
            BGlotation[i] = loopbgs[i].transform.position;
        }
    }
    void Update()
    {
        x_move_size = Mathf.Clamp(x_move_size + BGspeed*Time.deltaTime , 0, x_lengh);
        if (x_move_size != x_lengh)
        {
            for (int i = 0; i < BGcount; i++)
            {
                loopbgs[i].transform.position = new Vector3(loopbgs[i].transform.position.x + BGspeed * Time.deltaTime, loopbgs[i].transform.position.y, loopbgs[i].transform.position.z);
               
            }
        }
        else
        {
            for (int i = 0; i < BGcount; i++)
            {
              
                loopbgs[i].transform.position = BGlotation[i];
            }
            x_move_size = 0;
        }
        
    }
}
