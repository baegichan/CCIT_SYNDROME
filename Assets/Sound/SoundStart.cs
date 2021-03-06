using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundStart : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Sound
    {
        DrWattsLabBGM,
        RealWorldBGM,
        TitleBGM,
        BattleBGM
    }
    public Sound SoundType;
    void Start()
    {
        switch (SoundType)
        {
            case Sound.BattleBGM:
                SoundManager.BGLoop("BattleBGM");
                break;
            case Sound.DrWattsLabBGM:
                SoundManager.BGLoop("LabBGM");
                break;
            case Sound.RealWorldBGM:
                SoundManager.BGLoop("BattleField");
                break;
            case Sound.TitleBGM:
                SoundManager.BGLoop("TitleBGM");
                break;

                
        
        
        }

    }
}
