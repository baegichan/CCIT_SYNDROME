using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill_Active_false : MonoBehaviour
{
    public GameObject Drill;

    public void Event()
    {
        Drill.SetActive(false);
    }
}
