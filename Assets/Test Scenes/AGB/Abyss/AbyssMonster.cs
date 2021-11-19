using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssMonster : MonoBehaviour
{
    // Start is called before the first frame update


    AbyssManager abyssManager;

    [Header("DarkFogPrefab")]
    public GameObject DarkFog;
    [Header("AbyssGage")]
    public int giveAbyssGage = 5;
    [Header("Monster ID")]
    public int id = 0;
    AbyssManager.AbyssState state;
    private void Start()
    {
        abyssManager = GameObject.Find("AbyssManager").transform.GetComponent<AbyssManager>();



    }

   
  
    public void MonsterDie()
    {
        if (abyssManager.abyssState == AbyssManager.AbyssState.Reality)
        {
            abyssManager.AbyssMonsterAdd(id, transform.position);
            abyssManager.GetAbyssGage(giveAbyssGage);
            Destroy(this.gameObject);

        }
        else
        {
            Instantiate(DarkFog,transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        
        }

    }

  
}
