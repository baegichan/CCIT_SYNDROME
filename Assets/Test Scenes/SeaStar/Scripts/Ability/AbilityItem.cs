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
    public bool isthisShop;

    void Awake()
    {
        AlyakList();
    }

    void Start()
    {
        if (transform.tag == "Pill") { DecideCode_Gacha(); }
    }

    void Update()
    {
        if(me.IsSelect && Ply != null && transform.tag == "Pill") { BuyItem(); }
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
            if (DecideGrade <= 5000)
            {
                int i = Random.Range(0, commonList.Count);
                ThisCode = commonList[i].AbCode;
            }
            else if(DecideGrade > 5000 && DecideGrade <= 8000)
            {
                int i = Random.Range(0, rareList.Count);
                ThisCode = rareList[i].AbCode;
            }
            else if (DecideGrade > 8000 && DecideGrade <= 10000)
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
                me = new Ability(AbList[i].AbCode, AbList[i].AbName, AbList[i].AbType, AbList[i].AbGrade, AbList[i].Enhance, AbList[i].Enhance_Cost, AbList[i].Effect, AbList[i].AbPrice, AbList[i].IsSelect, AbList[i].AbIcon, AbList[i].AbSprite, AbList[i].ResultIcon, AbList[i].icon, AbList[i].CoolTime, AbList[i].IsUsing, AbList[i].AbExplan);
                AbList.RemoveAt(i);
                if (!isthisShop)
                {
                    Vector2 EffectPosition = new Vector2(transform.position.x, transform.position.y - 0.6f);
                    GameObject Effect = Instantiate(me.Effect, EffectPosition, Quaternion.identity);
                    Effect.transform.parent = transform;
                }
            }
        }
    }

    Char_Parent pt;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Ply = col.gameObject;
            pt = col.GetComponentInParent<Char_Parent>();
            me.IsSelect = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") { me.IsSelect = false; }
    }

    void BuyItem()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsBuy)
            {
                switch (me.AbType)
                {
                    case Ability.ABTYPE.Active:
                        pt.ActiveAbility = me;
                        pt.DecideChar();
                        pt.SelectAbility();
                        PlayerSkillUI.skill.Image_Active.sprite = me.icon;
                        PlayerSkillUI.skill.Image_CoolTime.sprite = me.CoolTime;
                        pt.SaveAbilityHistory(me);
                        break;
                    case Ability.ABTYPE.Passive:
                        pt.PassiveAbility = me;
                        pt.UsePassive();
                        pt.passive();
                        PlayerSkillUI.skill.Image_Passive.sprite = me.icon;
                        pt.SaveAbilityHistory(me);
                        break;
                    case Ability.ABTYPE.HPDrink:
                        pt.MulYakInt++;
                        PlayerSkillUI.skill.HpPotionInt.text = pt.MulYakInt.ToString();
                        break;
                    case Ability.ABTYPE.APDrink:
                        pt.AlYakInt++;
                        PlayerSkillUI.skill.PillInt.text = pt.AlYakInt.ToString();
                        break;
                }
                if (me.AbCode != 0) { pt.DecideChar(); }
                else if (me.AbCode != 0)
                {
                    pt.Ani.SetFloat("AbilityNum", 0);
                    pt.Ani.SetTrigger("Ability");
                }
                Destroy(this.gameObject);
            }
            else
            {
                if (me.AbPrice > pt.P_Money)
                {
                    Debug.Log("���� �����մϴ�.");
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
