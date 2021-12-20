using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCounter : MonoBehaviour
{   //dummy
    public int Monster_count=1;
    public int AbyssMonsterKillCount = 0;
    public int NormalMonsterKillCount = 0;
    public bool BoxEvent=false;
    public bool NormalClearEvent = false;
    public bool AbyssClearEvent = false;
    public GameObject Box_OBJ;

   public void RoomClear()
   {
   if(Monster_count!=0)
   {
            if (Monster_count == AbyssMonsterKillCount && AbyssClearEvent == false)
            {
                AbyssClearEvent = true;

                ClearAniController.s_instance.RandomClearEvent();
                RoomClearManager.clear.RoomClear();
            }
            if (Monster_count == NormalMonsterKillCount && NormalClearEvent == false)
            {
                NormalClearEvent = true;
                ClearAniController.s_instance.ALLMonsterClear();

            }
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
