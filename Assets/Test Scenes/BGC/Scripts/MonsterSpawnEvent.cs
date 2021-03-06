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
    public bool UseSpawner = true;
    public bool OneTimeSpawn = false;
    int count = 0;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
                  if (UseSpawner == true)
                  {
                    if(OneTimeSpawn)
                  {
                    Spawn_Monster(transform.parent.Find("NormalMonsterP(Clone)").gameObject);
                  }
                  else
                  {
                    StartCoroutine(Spawn_Monster_Inorder(transform.parent.Find("NormalMonsterP(Clone)").gameObject)); ;
                  }
              
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
            
            Instantiate(Resources.Load(transform.GetChild(0).gameObject.name), transform.GetChild(0).position, Quaternion.identity, this.gameObject.transform);
            Destroy(transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }

    IEnumerator Spawn_Monster_Inorder(GameObject target)
    {
        UseSpawner = true;
        for (int i = 0; i < count; i++)
        {

            Instantiate(Resources.Load(transform.GetChild(0).gameObject.name), transform.GetChild(0).transform.position, Quaternion.identity, target.transform);
            Destroy(transform.GetChild(0).gameObject);
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
    public void Spawn_Monster()
    {
        
        if (transform.childCount>0)
        {
            for(int i =0; i< count; i++)
            {
            
                Instantiate(Resources.Load(transform.GetChild(i).gameObject.name), transform.GetChild(i).position,Quaternion.identity,this.gameObject.transform);
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        UseSpawner = true;
    }
    public void Spawn_Monster(GameObject target)
    {

        if (transform.childCount > 0)
        {
            for (int i = 0; i < count; i++)
            {

                Instantiate(Resources.Load(transform.GetChild(i).gameObject.name), transform.GetChild(i).position, Quaternion.identity, target.transform);
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        UseSpawner = true;
    }

}
