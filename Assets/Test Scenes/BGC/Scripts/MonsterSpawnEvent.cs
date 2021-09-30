using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
public class MonsterSpawnEvent : MapEvent
{
    // Start is called before the first frame update
    MonsterSpawnEvent()
    {
        base.EventType = MapEvent.Event.MonsterSpawn;
    }
    public float SpawnDelay = 0.5f;
    public bool UseSpawner = false;
    int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UseSpawner != true)
        {
            if (collision.tag == "Player")
            {
                StartCoroutine(Spawn_Monster_Inorder());
                //Spawn_Monster();
            }
        }
    }
    private void Start()
    {
       count = transform.childCount;
    }
    
    IEnumerator Spawn_Monster_Inorder()
    {
        UseSpawner = true;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(SpawnDelay);
     
            Debug.Log(transform.GetChild(0).gameObject.name + " Spawned");
            Instantiate(Resources.Load(transform.GetChild(0).gameObject.name), transform.GetChild(0).position, Quaternion.identity, this.gameObject.transform);
            Destroy(transform.GetChild(0).gameObject);
        }
    }
    public void Spawn_Monster()
    {
        
        if (transform.childCount>0)
        {
            for(int i =0; i< count; i++)
            {
                Debug.Log(transform.GetChild(i).gameObject.name + " Spawned");
                Instantiate(Resources.Load(transform.GetChild(i).gameObject.name), transform.GetChild(i).position,Quaternion.identity,this.gameObject.transform);
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        UseSpawner = true;
    }

   
}
