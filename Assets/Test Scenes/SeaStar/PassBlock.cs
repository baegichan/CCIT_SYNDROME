using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassBlock : MonoBehaviour
{
    public bool IsUse;
    float Timer = 0f;
    float TimerMax = 0.6f;
    public PlatformEffector2D PE;

    private void Start()
    {
        PE = transform.GetComponent<PlatformEffector2D>();
    }
    void Update()
    {
        if(IsUse)
        {
            if(Timer < TimerMax) { Timer += Time.deltaTime; }
            else if(Timer >= TimerMax)
            {
                Timer = 0f;
                PE.colliderMask = Physics.AllLayers;
                IsUse = false;
            }
        }
    }
}
