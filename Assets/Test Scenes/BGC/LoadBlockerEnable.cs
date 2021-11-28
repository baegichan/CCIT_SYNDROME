using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
public class LoadBlockerEnable : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem.MainModule module;
    public ParticleSystem particle;
    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        module = particle.main;
    }
    private void OnEnable()
    {
        if(MapManager.s_Instace.Map_Lock==false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            module.startLifetime = 3;
        }
    }
}
