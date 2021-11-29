using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUI : MonoBehaviour
{
    static PlayerSkillUI _skill;

    public static PlayerSkillUI skill
    {
        get
        {
            if (!_skill)
            {
                _skill = FindObjectOfType(typeof(PlayerSkillUI)) as PlayerSkillUI;

                if (_skill == null)
                    Debug.Log("no Singleton obj");
            }
            return _skill;
        }
    }

    void Awake()
    {
        if (_skill == null)
        {
            _skill = this;
        }
        else if (_skill != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public Image Image_Active;
    public Image Image_Passive;
    public Image Image_CoolTime;

    public Text HpPotionInt;
    public Text PillInt;
    private float coolTime;

    float time = 0;
    IEnumerator CoolTimeStart(float coolTimes)
    {
        Image_CoolTime.fillAmount = 1;
        time = 1f / coolTimes;
        while (Image_CoolTime.fillAmount > 0)
        {

            Image_CoolTime.fillAmount -= time;
            yield return new WaitForSeconds(1f);
        }
       
    }

    public float CoolTime
    {
        set
        {
            coolTime = value;
            StartCoroutine(CoolTimeStart(coolTime));
        }

    }
}
