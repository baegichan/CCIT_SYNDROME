using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Info_Trans : MonoBehaviour
{

    public void Translate_Boss_State()
    {
        transform.GetChild(1).GetComponent<Boss>().Hp_Current = transform.GetChild(0).GetComponent<Boss>().Hp_Current;
    }


}
