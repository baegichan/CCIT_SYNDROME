using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItem : MonoBehaviour
{
    public GameObject AbManager;
    AbilityManager AM;
    public int ThisCode;
    public int ThisPrice;
    public Ability me;
    public GameObject Ply;

    void Start()
    {
        AM = AbManager.GetComponent<AbilityManager>();
        ThisCode = Random.Range(0, AM.AbList.Count);
        SelectAbility();
    }

    void Update()
    {
        if (me.IsSelect && Ply != null) { BuyItem(); }
    }

    void SelectAbility()
    {
        for (int i = 0; i < AM.AbList.Count; i++)
        {
            if (ThisCode == AM.AbList[i].AbCode)
            {
                SpriteRenderer spt = this.GetComponent<SpriteRenderer>();
                spt.sprite = AM.AbList[i].AbSprite;
                gameObject.name = AM.AbList[i].AbName;
                ThisPrice = AM.AbList[i].AbPrice;
                me = new Ability(AM.AbList[i].AbCode, AM.AbList[i].AbName, AM.AbList[i].AbType, AM.AbList[i].AbGrade, AM.AbList[i].AbPrice, AM.AbList[i].IsBuy, AM.AbList[i].IsSelect, AM.AbList[i].AbSprite, AM.AbList[i].IsUsing);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Ply = col.gameObject;
            PlayerM_ pt = col.GetComponent<PlayerM_>();
            me.IsSelect = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { me.IsSelect = false; }
    }

    void BuyItem()
    {
        PlayerM_ pt = Ply.GetComponent<PlayerM_>();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (me.IsBuy)
            {
                switch (me.AbType)
                {
                    case 0:
                        pt.ActiveAbility = me;
                        pt.SelectAbility();
                        break;
                    case 1:
                        pt.PassiveAbility = me;
                        UsePassive();
                        passive();
                        break;
                    case 2:
                        pt.MulYakInt++;
                        break;
                    case 3:
                        pt.AlYakInt++;
                        break;
                }
                Destroy(this.gameObject);
            }
            else
            {
                if (me.AbPrice > pt.P_Money)
                {
                    Debug.Log("돈이 부족합니다.");
                }
                else if (me.AbPrice <= pt.P_Money)
                {
                    pt.P_Money -= me.AbPrice;
                    me.IsBuy = true;
                }
            }
        }
    }

    public delegate void usePassive();
    usePassive passive;

    void UsePassive()
    {
        switch(me.AbCode)
        {
            case 0:
                passive = new usePassive(AM.Werewolf);
                break;
            case 1:
                passive = new usePassive(AM.Parao);
                break;
            case 2:
                passive = new usePassive(AM.BomberMan);
                break;
            case 3:
                passive = new usePassive(AM.Ability_D);
                break;
            case 4:
                passive = new usePassive(AM.Ability_E);
                break;
            case 5:
                passive = new usePassive(AM.Ability_F);
                break;
            case 6:
                passive = new usePassive(AM.Double_Jump);
                Ply.GetComponent<PlayerM_>().P_MaxJumpInt = 2;
                break;
        }
    }
}
