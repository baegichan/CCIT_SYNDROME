using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region ����
    float maxHp;
    float hp;
    float darkeFog;
    float avssGage;
    
    float beforeHp;
    float beforeMaxhp;
    float beforeDark;
    float beforeAvss;
    

    public AbyssManager abyssManager;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(beforeAvss != avssGage)
    }

    #region ���ҽ� ��������

    public float MaxHp
    {
        set { maxHp = value; }
    }
   public float Hp
    {
        set { hp = value; }
    }


    #endregion

    #region
    #endregion
}
