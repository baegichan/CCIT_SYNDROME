using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAniController : MonoBehaviour
{
    public static ClearAniController s_instance;
    public Animator ClearController;
    public void Instance()
    {
    if(s_instance==null)
    {
            s_instance = this;

    }
    else
    {
            Destroy(gameObject);
    }
    if(ClearController==null)
    {
            ClearController = GetComponent<Animator>();
    }
    }

    private void Awake()
    {
        Instance();
    }
  public void ALLClear()
  {
        ClearController.ResetTrigger("MapBossOn");
        ClearController.ResetTrigger("BossOn");
        ClearController.ResetTrigger("AllMonster");
        ClearController.ResetTrigger("DoorLock");
        ClearController.ResetTrigger("AbyssAllClear");
        ClearController.ResetTrigger("AnimationEnd");
        ClearController.SetBool("GetPotion",false);
        ClearController.SetBool("GetHPPotion",false);
        ClearController.SetBool("Get150",false);
       
    }


    public void RandomClearEvent()
    {
        int i = Random.Range(0, 3);
        switch(i)
        {
            case 0:
                ALLAbyssClear();
                Get150();
                break;
            case 1:
                ALLAbyssClear();
                HPPotion();
                break;
            case 2:
                ALLAbyssClear();
                AbyssPotion();
                break;
           
        }
    }
    public void Get150()
    {
        ClearController.SetBool("Get150", true);
    }
  public void HPPotion()
  {
        ClearController.SetBool("GetHPPotion", true);
  }
  public void AbyssPotion()
  {
        ClearController.SetBool("GetPotion",true);
    }
  public void BossOn()
  {
        ClearController.SetTrigger("BossOn");
    }
  public void BossOpen()
  {
        ClearController.SetTrigger("MapBossOn");
    }
  public void ALLMonsterClear()
  {
        ClearController.SetTrigger("AllMonster");
   }
  public void NonEnergy()
  {
        ClearController.SetTrigger("DoorLock");
    }
  public void ALLAbyssClear()
  {
        ClearController.SetTrigger("AbyssAllClear");
    }
}
