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

    void Start()
    {
        AM = AbManager.GetComponent<AbilityManager>();
        ThisCode = Random.Range(0, AM.AbList.Count);
        SelectAbility();
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
                me = AM.AbList[i];
            }
        }
    }

    void AbililtyEffect()
    {
        switch(ThisCode)
        {
            case 0:
                AM.Ability_A();
                Destroy(this.gameObject);
                break;
            case 1:
                AM.Ability_B();
                Destroy(this.gameObject);
                break;
            case 2:
                AM.Ability_C();
                Destroy(this.gameObject);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        { 
            AbililtyEffect();

            PlayerTest pt = col.GetComponent<PlayerTest>();

            if (!pt.HaveAbility[0].IsUsing){ pt.HaveAbility[0] = me; }
            else if (pt.HaveAbility[0].IsUsing && !pt.HaveAbility[1].IsUsing) { pt.HaveAbility[1] = me; }
            else if(pt.HaveAbility[0].IsUsing && pt.HaveAbility[1].IsUsing)
            {
                pt.HaveAbility[0] = pt.HaveAbility[1];
                pt.HaveAbility[1] = me;
            }
        }
    }
}
