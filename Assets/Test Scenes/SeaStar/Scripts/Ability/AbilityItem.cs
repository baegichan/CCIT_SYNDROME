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
    public LayerMask TargetMask;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(7, 30);
        AlyakList();
    }

    void Start()
    {
        if (transform.tag == "Pill") { DecideCode_Gacha(); }
    }

    void Update()
    {
        BuyItem();
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
                me = new Ability(AbList[i].AbCode, AbList[i].AbName, AbList[i].AbType, AbList[i].AbGrade, AbList[i].Enhance, AbList[i].Enhance_Cost, AbList[i].AbPrice, AbList[i].IsSelect, AbList[i].AbIcon, AbList[i].AbSprite, AbList[i].ResultIcon, AbList[i].icon, AbList[i].CoolTime, AbList[i].IsUsing, AbList[i].AbExplan);
                AbList.RemoveAt(i);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Player")
        {
            Debug.Log(0);
            Ply = col.gameObject;
            Char_Parent pt = col.transform.GetComponent<Char_Parent>();
            me.IsSelect = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Player") { me.IsSelect = false; Debug.Log(1); }
    }

    void BuyItem()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Physics2D.queriesStartInColliders = false;
            Collider2D playerCol = Physics2D.OverlapBox(transform.position, new Vector2(5, 5), 0, TargetMask);
            if(playerCol != null)
            {
                Ply = playerCol.gameObject;
                Char_Parent pt = playerCol.transform.GetComponentInParent<Char_Parent>();
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
}
