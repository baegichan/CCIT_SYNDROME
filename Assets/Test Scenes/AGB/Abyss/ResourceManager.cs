using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region �̱���

    private static ResourceManager _re;

    public static ResourceManager re
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
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
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_re != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
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
