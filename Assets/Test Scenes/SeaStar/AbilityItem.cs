using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    public int ThisCode;
    public int ThisPrice;
    public Ability me;
    public GameObject Ply;
    public AbilityManager ability;
    public List<Ability> AbList = new List<Ability>();

    void Start()
    {
        ability = this.GetComponent<AbilityManager>();
        ThisCode = Random.Range(0, AbList.Count);
        SelectAbility();
    }

    void Update()
    {
        if (me.IsSelect && Ply != null) { BuyItem(); }
    }

    void SelectAbility()
    {
        for (int i = 0; i < AbList.Count; i++)
        {
            if (ThisCode == AbList[i].AbCode)
            {
                SpriteRenderer spt = this.GetComponent<SpriteRenderer>();
                spt.sprite = AbList[i].AbSprite;
                gameObject.name = AbList[i].AbName;
                ThisPrice = AbList[i].AbPrice;
                me = new Ability(AbList[i].AbCode, AbList[i].AbName, AbList[i].AbType, AbList[i].AbGrade, AbList[i].AbPrice, AbList[i].IsBuy, AbList[i].IsSelect, AbList[i].AbSprite, AbList[i].IsUsing, AbList[i].AbWeight);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
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
        switch (me.AbCode)
        {
            case 0:
                passive = new usePassive(ability.Werewolf);
                break;
            case 1:
                passive = new usePassive(ability.Parao);
                break;
            case 2:
                passive = new usePassive(ability.BomberMan);
                break;
            case 3:
                passive = new usePassive(ability.Ability_D);
                break;
            case 4:
                passive = new usePassive(ability.Ability_E);
                break;
            case 5:
                passive = new usePassive(ability.Ability_F);
                break;
            case 6:
                passive = new usePassive(ability.Double_Jump);
                Ply.GetComponent<PlayerM_>().P_MaxJumpInt = 2;
                break;
        }
    }
}
