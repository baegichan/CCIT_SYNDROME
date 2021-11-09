using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = new SoundManager();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public List<AudioSource> EFX = new List<AudioSource>();
    public List<AudioSource> BG = new List<AudioSource>();
}
