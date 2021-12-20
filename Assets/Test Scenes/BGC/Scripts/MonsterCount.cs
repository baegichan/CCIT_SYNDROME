using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCount : MonoBehaviour
{
    private void Start()
    {
        if(MonsterState==state.Normal)
        {
            transform.parent.parent.parent.GetComponent<MonsterCounter>().Monster_count += 1;
        }
    }
    public enum state
    {
    Normal,
    Abyss
    }
    public state MonsterState;
    public void MonsterKill()
    {
        switch(MonsterState)
        {
            case state.Normal:
                transform.parent.parent.parent.GetComponent<MonsterCounter>().NormalMonsterKillCount += 1;
                break;
            case state.Abyss:
                transform.parent.parent.parent.GetComponent<MonsterCounter>().AbyssMonsterKillCount += 1;
                break;

        }
      
    }
}
