using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{

    private static StateManager _state;

    public static StateManager state
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_state)
            {
                _state = FindObjectOfType(typeof(StateManager)) as StateManager;

                if (_state == null)
                    Debug.Log("no Singleton obj");
            }
            return _state;
        }
    }

    private void Awake()
    {
        if (_state == null)
        {
            _state = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_state != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }

    #region ����
    int maxHp;
    int hp;

 

    int avssGage;
    int darkFog;
    

    [SerializeField]
    private Image HpBar;

    [SerializeField]
    private Image AbyssBar;


 

    [SerializeField]
    private Text DarkFogText;

    [SerializeField]
    private GameObject PlayerImgBox;
    #endregion


    #region ���ҽ� ��������

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
            HpBar.fillAmount = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);


        }
    }

    public int AbyssGage
    {
        set
        {
            avssGage = value;
            AbyssBar.fillAmount = Convert.ToSingle(avssGage) / Convert.ToSingle(100);
            Debug.Log(AbyssBar.fillAmount);

        }
    }
    public int DarkFog
    {
        set
        {
            darkFog = value;
            DarkFogText.text = Convert.ToString(darkFog);
        }
    }

    public void CharImgSelect(int charnum)
    {
        int i = PlayerImgBox.transform.childCount;

        for (int j = 0; j < i; j++)
        {
            if (j != charnum)
                PlayerImgBox.transform.GetChild(j).gameObject.SetActive(false);
            else
                PlayerImgBox.transform.GetChild(charnum).gameObject.SetActive(true);
        }
    }
    #endregion

}
