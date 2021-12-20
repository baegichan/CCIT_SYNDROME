using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssKillCount : MonoBehaviour
{
   
    public void AbyssMonsterKill()
    {
        transform.parent.parent.GetComponent<MonsterCounter>().AbyssMonsterKillCount += 1;
    }

 
}
