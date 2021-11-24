using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityItem : MonoBehaviour
{
    public int ThisCode;
    public int ThisPrice;
    public bool IsBuy;
    public Ability me;
    public GameObject Ply;
    public AbilityManager ability;
    public List<Ability> commonList = new List<Ability>();
    public List<Ability> rareList = new List<Ability>();
    public List<Ability> uniqueList = new List<Ability>();
    public List<Ability> DrinkList = new List<Ability>();
    public List<Ability> AbList = new List<Ability>();

    void Awake()
    {
        AlyakList();
    }

    void Start()
    {
        if(transform.tag == "Pill") { DecideCode_Gacha(); }
    }

    void Update()
    {
        if (me.IsSelect && Ply != null) { BuyItem(); }
    }

    public void AlyakList()
    {
        for (int i = 0; i < AbList.Count; i++)
        {
            switch (AbList[i].AbGrade)
            {
                case Ability.ABGRADE.Common:
                    commonList.Add(AbList[i]);
                    break;
                case Ability.ABGRADE.Rare:
                    rareList.Add(AbList[i]);
                    break;
                case Ability.ABGRADE.Unique:
                    uniqueList.Add(AbList[i]);
                    break;
                case Ability.ABGRADE.Drink:
                    DrinkList.Add(AbList[i]);
                    break;
            }
        }
    }

    void DecideCode_Gacha()
    {
        int ItemType = Random.Range(0, 100);

        if(ItemType <= 75)
        {
            int DecideGrade = Random.Range(0, 10000);
            if (DecideGrade <= 7000)
            {
                int i = Random.Range(0, commonList.Count);
                ThisCode = commonList[i].AbCode;
            }
            else if(DecideGrade > 7000 && DecideGrade <= 9000)
            {
                int i = Random.Range(0, rareList.Count);
                ThisCode = rareList[i].AbCode;
            }
            else if (DecideGrade > 9000 && DecideGrade <= 10000)
            {
                int i = Random.Range(0, uniqueList.Count);
                ThisCode = uniqueList[i].AbCode;
            }
        }
        else
        {
            int DecideDrink = Random.Range(0, 10000);
            if(DecideDrink <= 8000) { ThisCode = DrinkList[0].AbCode; }
            else { ThisCode = DrinkList[1].AbCode; }
        }

        SelectAbility();
    }

    public void SelectAbility()
    {
        for (int i = 0; i < AbList.Count; i++)
        {
            if (ThisCode == AbList[i].AbCode)
            {
                if(transform.tag == "Pill")
                {
                    SpriteRenderer spt = this.GetComponent<SpriteRenderer>();
                    spt.sprite = AbList[i].AbSprite;
                    gameObject.name = AbList[i].AbName;
                    ThisPrice = AbList[i].AbPrice;
                }
                me = new Ability(AbList[i].AbCode, AbList[i].AbName, AbList[i].AbType, AbList[i].AbGrade, AbList[i].Enhance, AbList[i].Enhance_Cost, AbList[i].AbPrice, AbList[i].IsSelect, AbList[i].AbIcon, AbList[i].AbSprite, AbList[i].IsUsing, AbList[i].AbExplan);
                AbList.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Ply = col.gameObject;
            Char_Parent pt = col.GetComponent<Char_Parent>();
            me.IsSelect = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { me.IsSelect = false; }
    }

    void BuyItem()
    {
        Char_Parent pt = Ply.GetComponentInParent<Char_Parent>();

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsBuy)
            {
                switch (me.AbType)
                {
                    case Ability.ABTYPE.Active:
                        pt.ActiveAbility = me;
                        pt.SelectAbility();
                        break;
                    case Ability.ABTYPE.Passive:
                        pt.PassiveAbility = me;
                        pt.UsePassive();
                        pt.passive();
                        break;
                    case Ability.ABTYPE.HPDrink:
                        pt.MulYakInt++; 
                        break;
                    case Ability.ABTYPE.APDrink:
                        pt.AlYakInt++;
                        break;
                }
                pt.SaveAbilityHistory(me);
                pt.DecideChar();
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
                    IsBuy = true;
                }
            }
        }
    }
}
