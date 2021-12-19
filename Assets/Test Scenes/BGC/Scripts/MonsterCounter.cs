using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCounter : MonoBehaviour
{   //dummy
    public int Monster_count=1;
    public int AbyssMonsterKillCount = 0;
    public bool BoxEvent=false;
    public GameObject Box_OBJ;

   public void RoomClear()
   {
    if(Monster_count==AbyssMonsterKillCount)
    {
        //클리어임 instanceate(Box_OBJ)
    }
   }
    private void Awake()
    {
        Monster_count = 0;
    }

    private void Update()
    {
        RoomClear();
    }
    public void CountUP()
   {
        Monster_count += 1;
   }
   public void AbyssKillCount()
   {
        AbyssMonsterKillCount += 1;
   }
}
