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
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_state != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    #region 변수
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


    #region 리소스 가져오기

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
