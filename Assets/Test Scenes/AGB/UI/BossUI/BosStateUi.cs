using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BosStateUi : MonoBehaviour
{
    int maxHp;
    int hp;
    int lastHp = 0;
    float hpFill = 1;
    bool isSlow;

    bool isWorring;
    bool isEnter = false;


    int shield;
    int maxShield;
    [SerializeField]
    private Image HpBar;

    [SerializeField]
    private Image ShieldBar;
    [SerializeField]
    private Image HpBarBack;
    [SerializeField]
    private Image HpBarEffect;
    [SerializeField]
    private Image ChainEffect;


    public int Hp
    {
        set
        {
           
            if (value > maxHp) hp = maxHp;
            else if (value < 0) hp = 0;
            else hp = value;

            hpFill = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);

            if (lastHp == 0)
                lastHp = hp;
            isSlow = false;
            StartCoroutine(HpBarEffects());
            StartCoroutine(AddDamgeCount());
            HpBar.fillAmount = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);
        }
        get
        {
           return hp;
        }
    }
    public int MaxHp
    {
        set
        {
            maxHp = value;
            hp = maxHp;
        }
    }

    public int Shield
    {
        set
        {
            shield = value;
            ShieldBar.fillAmount = Convert.ToSingle(shield) / Convert.ToSingle(maxShield);

        }
        get
        {
            return shield;
        }
    }
    public int MaxShield
    {
        set
        {
            maxShield = value;
            shield = maxShield;
        }
    }


    IEnumerator AddDamgeCount()
    {
        yield return new WaitForSeconds(1.8f);
        if (hpFill == HpBarBack.fillAmount)
            isSlow = false;
        else
            isSlow = true;
    }
    IEnumerator HpBarEffects()
    {

        if (HpBar.fillAmount > hpFill)
        {
            HpBarEffect.color = new Color(1, 0.827f, 0.635f, 1);
            ChainEffect.color = new Color(1, 0.827f, 0.635f, 1);
        }
          
        HpBar.fillAmount = hpFill;
        yield return new WaitForSeconds(0.15f);
        HpBarEffect.color = new Color(1, 1, 1, 0);
        ChainEffect.color = new Color(1, 1, 1, 0);
        //HpBarBack.fillAmount = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);

    }
}
