using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssMonster : MonoBehaviour
{
    // Start is called before the first frame update


    

    [Header("DarkFogPrefab")]
    public GameObject DarkFog;
    [Header("AbyssGage")]
    public int giveAbyssGage = 5;
    [Header("Monster ID")]
    public int id = 0;
 
  
   
  
    public void MonsterDie()
    {
        if (AbyssManager.abyss.abyssState == AbyssManager.AbyssState.Reality)
        {
            AbyssManager.abyss.AbyssMonsterAdd(id, transform.position);
            AbyssManager.abyss.GetAbyssGage(giveAbyssGage);
            Destroy(this.gameObject);

        }
        else
        {
            Instantiate(DarkFog,transform.position, Quaternion.identity);
            Destroy(transform.gameObject);
        
        }

    }

  
}
