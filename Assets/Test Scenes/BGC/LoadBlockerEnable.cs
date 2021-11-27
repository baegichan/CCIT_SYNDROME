using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
public class LoadBlockerEnable : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem.MainModule module;
    private void Start()
    {
        module = GetComponent<ParticleSystem.MainModule>();
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
