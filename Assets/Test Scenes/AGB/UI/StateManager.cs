using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    public static StateManager state;
    #region ����
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


    #region ���ҽ� ��������
    private void Start()
    {

    }
    public int MaxHp
    {
        set
        {
            //max ���Ŀ� ������ �߰�
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
