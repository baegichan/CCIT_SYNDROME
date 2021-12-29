using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearAniController : MonoBehaviour
{
    public static ClearAniController s_instance;
    public Animator ClearController;
    public Char_Parent Player_cha;
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
        //ClearController.ResetTrigger("MapBossOn");
        //ClearController.ResetTrigger("BossOn");
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
                Get150();
                ALLAbyssClear();
               
                break;
            case 1:
                HPPotion();
                ALLAbyssClear();
               
                break;
            case 2:
                AbyssPotion();
                ALLAbyssClear();
             
                break;
           
        }
    }
    public void Get150()
    {
        ClearController.SetBool("Get150", true);
        AbyssManager.abyss.Darkfog += 150;
    }
  public void HPPotion()
  {
        Player_cha.MulYakInt += 1;
        PlayerSkillUI.skill.HpPotionInt.text = Player_cha.MulYakInt.ToString();
        ClearController.SetBool("GetHPPotion", true);
    
    }
  public void AbyssPotion()
  {
        StateManager.state.AbyssGage += 50;

        ClearController.SetBool("GetPotion",true);
     

    }


    private bool BossOnCheck = false;
    private bool BossMapOn = false;
  public void BossOn()
  {
        if(BossOnCheck==false)
          {
            BossOnCheck = true;
            ClearController.SetTrigger("BossOn");
        }
       
    }
  public void BossOpen()
    {
        if (BossMapOn == false)
        {
            BossMapOn = true;
            ClearController.SetTrigger("MapBossOn");
        }
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
