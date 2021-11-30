using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;
    public AudioSource SFX;

    public AudioSource BGM;

    private bool looping;

    float bgmvol;
    float effvol;

    void Awake()
    {

        if(instance == null)
        {
            Debug.Log("Created");
            instance = this;
            for(int i = 0; i<EFXs.Length;i++)
            {
                EFX.Add(EFXs[i].Soundname, i);
            }
            for (int j = 0; j < BGs.Length; j++)
            {
                BG.Add(BGs[j].Soundname, j);
                Debug.Log(BGs[j].Soundname + "   ");
            }
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public Sound[] EFXs;
    public Sound[] BGs;
    
    public Dictionary<string, int> EFX = new Dictionary<string, int>();
    public Dictionary<string, int> BG = new Dictionary<string, int>();
    private void Start()
    {
       // SoundManager.BGLoop("BattleBGM");
       
    }
    public enum SoundType
    {
    BG,
    EFX
    }

    
  
   public static void BGLoop(string SoundName)
   {
        int sound;
        if (instance.BG.TryGetValue(SoundName, out sound))
        {
            instance.BGM.clip = instance.BGs[sound].Audio;
            instance.BGM.Play();
        }
       
    }


    public static float BgmVol
    {
        get
        {
            return instance.bgmvol;
        }
        set
        {
            instance.bgmvol = value;
            instance.BGM.volume = instance.bgmvol;
        }
    }

    public static float EffVol
    {
        get
        {
            return instance.effvol;
        }
        set
        {
            instance.effvol = value;
            instance.SFX.volume = instance.effvol;
        }
    }

    public static void OneShot(string SoundName)
    {
        int sound;

      
                if (instance.EFX.TryGetValue(SoundName, out sound))
                {
                    Debug.Log("Sound on");
                  instance.SFX.PlayOneShot(instance.EFXs[sound].Audio, 0.5f);
                }
          
        }

       
    }

[System.Serializable]
public class Sound
{
    public AudioClip Audio;
    public string Soundname;
}
