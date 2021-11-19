using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    public static StateManager state;
    #region 변수
    int maxHp;
    int hp;

    int avssGage;

  


    public AbyssManager abyssManager;

    [SerializeField]
    private Image HpBar;

    [SerializeField]
    private Image AbyssBar;

    #endregion
    // Start is called before the first frame update


    // Update is called once per frame


    #region 리소스 가져오기
    private void Start()
    {

    }
    public int MaxHp
    {
        set
        {
            //max 추후에 증가본 추가
            maxHp = value;

        }
    }
    public int Hp
    {
        set
        {
            hp = value;
            HpBar.fillAmount = Convert.ToSingle(hp) /Convert.ToSingle(maxHp);
           
        
        }
    }

    public int AbyssGage
    {
        set
        {
            avssGage = value;
            AbyssBar.fillAmount = Convert.ToSingle(avssGage) / Convert.ToSingle(100);

        }
    }
    #endregion

    #region
  
    #endregion
}
