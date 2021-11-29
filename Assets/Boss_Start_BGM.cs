using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Start_BGM : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SoundManager.BGLoop("BattleBGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
