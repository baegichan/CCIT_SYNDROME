using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drillmonster : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

}