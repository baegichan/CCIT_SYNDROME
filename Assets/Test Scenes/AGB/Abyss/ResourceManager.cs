using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region 싱글톤

    private static ResourceManager _re;

    public static ResourceManager re
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_re)
            {
                _re = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;

                if (_re == null)
                    Debug.Log("no Singleton obj");
            }
            return _re;
        }
    }

    private void Awake()
    {
        if (_re == null)
        {
            _re = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_re != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public int darkFog;

    private int enhance_Health = 0;
    private int enhance_Strength = 0;
    private int enhance_Speed = 0;
    private Ability activeAbility;
    private Ability passiveAbility;
   

    public int DarkFog 
    {
        get
        {
            return darkFog;
        }
        set
        {
            darkFog = value;
        }
    }

    public int Enhance_Health
    {
        get
        {
            return enhance_Health;
        }
        set
        {
            enhance_Health = value;
        }
    }
    public int Enhance_Strength
    {
        get
        {
            return enhance_Strength;
        }
        set
        {
            enhance_Strength = value;
        }
    }
    public int Enhance_Speed
    {
        get
        {
            return enhance_Speed;
        }
        set
        {
            enhance_Speed = value;
        }
    }
    public Ability ActiveAbility
    {
        get
        {
            return activeAbility;
        }
        set
        {
            activeAbility = value;
        }
    }
    public Ability PassiveAbility
    {
        get
        {
            return passiveAbility;
        }
        set
        {
            passiveAbility = value;
        }
    }

   
}
