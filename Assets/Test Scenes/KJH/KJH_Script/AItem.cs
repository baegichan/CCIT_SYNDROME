using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AItem : MonoBehaviour
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
        if (IsBuy) { dc = DecideCode_Gacha; }
        else
        {
            dc = DecideCode_Shop;
        }
    }

    void Start()
    {
        dc();
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

    public delegate void Decide();
    public Decide dc;

    void DecideCode_Shop()
    {
        float SelectItem = Random.Range(0, 100.0f);
        if (SelectItem <= 65)
        {
            int i = Random.Range(0, commonList.Count);
            ThisCode = commonList[i].AbCode;
        }
        else if (SelectItem > 65 && SelectItem <= 70)
        {
            int i = Random.Range(0, rareList.Count);
            ThisCode = rareList[i].AbCode;
        }
        else if (SelectItem > 70 && SelectItem <= 100)
        {
            int DecideDrink = Random.Range(0, 10000);
            if (DecideDrink <= 8000) { ThisCode = DrinkList[0].AbCode; }
            else { ThisCode = DrinkList[1].AbCode; }
        }

        SelectAbility();
    }

    void DecideCode_Gacha()
    {
        int ItemType = Random.Range(0, 100);

        if (ItemType <= 75)
        {
            int DecideGrade = Random.Range(0, 10000);
            if (DecideGrade <= 7000)
            {
                int i = Random.Range(0, commonList.Count);
                ThisCode = commonList[i].AbCode;
            }
            else if (DecideGrade > 7000 && DecideGrade <= 9000)
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
            if (DecideDrink <= 8000) { ThisCode = DrinkList[0].AbCode; }
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
                if (transform.tag == "Pill")
                {
                    SpriteRenderer spt = this.GetComponent<SpriteRenderer>();
                    spt.sprite = AbList[i].AbSprite;
                    gameObject.name = AbList[i].AbName;
                    ThisPrice = AbList[i].AbPrice;
                }
                me = new Ability(AbList[i].AbCode, AbList[i].AbName, AbList[i].AbType, AbList[i].AbGrade, AbList[i].Enhance, AbList[i].Enhance_Cost, AbList[i].AbPrice, AbList[i].IsSelect, AbList[i].AbIcon, AbList[i].AbSprite, AbList[i].ResultIcon, AbList[i].icon, AbList[i].CoolTime, AbList[i].IsUsing, AbList[i].AbExplan);
                AbList.RemoveAt(i);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Ply = col.gameObject;
            TestPlayer pt = col.GetComponent<TestPlayer>();
            me.IsSelect = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { me.IsSelect = false; }
    }

    void BuyItem()
    {
        TestPlayer pt = Ply.GetComponentInParent<TestPlayer>();

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
                        break;
                    case Ability.ABTYPE.HPDrink:
                        pt.MulYakInt++;
                        break;
                    case Ability.ABTYPE.APDrink:
                        pt.AlYakInt++;
                        break;
                }
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


    public delegate void usePassive();
    usePassive passive;

    void UsePassive()
    {
        ability = Ply.GetComponentInParent<AbilityManager>();

        switch (me.AbCode)
        {
            case 6:
                passive = new usePassive(ability.Double_Jump);
                Ply.GetComponentInParent<TestPlayer>().P_MaxJumpInt = 2;
                break;
        }
    }
}
