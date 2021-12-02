using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class CM_vcam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Player").GetComponent<Char_Parent>().SelectChar.gameObject;
        if (Player != null)
        GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
    }

   
}
