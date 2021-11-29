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
    public Text HpPotionInt;
    public Text PillInt;
}
