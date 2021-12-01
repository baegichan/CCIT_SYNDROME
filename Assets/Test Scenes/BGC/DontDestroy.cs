using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        For_Fade.aa.Add(this.gameObject);
    }

    private void Update()
    {
        
    }

}
