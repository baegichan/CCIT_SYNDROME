using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Active_True : MonoBehaviour
{
    public GameObject Drill;

    public void Event2()
    {
        Drill.SetActive(true);
    }
}
