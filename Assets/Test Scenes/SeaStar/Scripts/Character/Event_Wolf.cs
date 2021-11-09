using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Wolf : MonoBehaviour
{
    void CanMove()
    {
        transform.GetComponent<Char_Wolf>().Ani.SetBool("CanIThis", true);
    }
}
