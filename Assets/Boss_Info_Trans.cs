using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Info_Trans : MonoBehaviour
{

    public void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf == true)
        {
            transform.GetChild(1).GetComponent<Boss>().transform.position = transform.GetChild(0).GetComponent<Boss>().transform.position;
            transform.GetChild(1).GetComponent<Boss>().Hp_Max = transform.GetChild(0).GetComponent<Boss>().Hp_Max;
            transform.GetChild(1).GetComponent<Boss>().Hp_Current = transform.GetChild(0).GetComponent<Boss>().Hp_Current;
        }
    }


}
