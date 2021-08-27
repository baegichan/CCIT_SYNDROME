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

    void Start()
    {
        AM = AbManager.GetComponent<AbilityManager>();

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
        if(col.tag == "Player") { AbililtyEffect(); }
    }
}
