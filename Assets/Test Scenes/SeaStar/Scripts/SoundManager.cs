using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;
    private AudioSource Audio;
    void Awake()
    {

        if(instance == null)
        {
            Audio = GetComponent<AudioSource>();
            instance = new SoundManager();
            foreach(Sound audio in EFXs)
            {
                instance.EFX.Add(audio.Soundname, audio.Audio);
            }

            foreach (Sound audio in BGs)
            {
                instance.BG.Add(audio.Soundname, audio.Audio);
            }

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public Sound[] EFXs;
    public Sound[] BGs;
    
    public Dictionary<string, AudioClip> EFX = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> BG = new Dictionary<string, AudioClip>();
    private void Start()
    {
        OneShot("BG", SoundType.BG);
    }
    public enum SoundType
    {
    BG,
    EFX
    }
   
    public void OneShot(string SoundName, SoundType type)
    {
        AudioClip sound;

        switch (type)
        {
            case SoundType.EFX:
                if (EFX.TryGetValue(SoundName, out sound))
                {
                    Debug.Log("Sound on");
                    Audio.PlayOneShot(sound, 40);
                }
                break;
            case SoundType.BG:
                if (BG.TryGetValue(SoundName, out sound))
                {
                    Debug.Log("Soundon");
                    Audio.PlayOneShot(sound, 40);
                }
                break;
        }

       
    }
}
[System.Serializable]
public class Sound
{
    public AudioClip Audio;
    public string Soundname;
}
