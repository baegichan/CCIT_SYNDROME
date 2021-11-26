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
    int lastHp = 0;
    float hpFill = 1;
    bool isSlow;

    bool isWorring;
    bool isEnter = false;

    int avssGage;
    int darkFog;

    [Header("HP")]
    [SerializeField]
    private Image HpBar;
    [SerializeField]
    private Image HpBarBack;
    [SerializeField]
    private Image HpBarEffect;

    [Header("Abyss")]
    [SerializeField]
    private Image AbyssBar;
    [SerializeField]
    private Image AbyssEffect;





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
            if (value > maxHp) hp = maxHp;
            else if (value < 0) hp = 0;
            else hp = value;

            hpFill = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);
           
            if (lastHp == 0)
                lastHp = hp;
            isSlow = false;
            StartCoroutine(HpBarEffects());
            StartCoroutine(AddDamgeCount());


            if (hpFill < 0.25f)
            {
                if (!isEnter)
                { 
                    StartCoroutine(Worring());
                }     
            }   
            else
                isWorring = false;
        }
    }


    private void Update()
    {

        if (hpFill != HpBarBack.fillAmount && isSlow)
            HpBarBack.fillAmount = Mathf.Lerp(HpBarBack.fillAmount, hpFill, Time.deltaTime * 20f);

    }


    IEnumerator Worring()
    {
        isEnter = true;
        isWorring = true;
        while (isWorring)
        {
            HpBarEffect.color = new Color(1, 0.347f, 0, 1);
            yield return new WaitForSeconds(0.5f);
            HpBarEffect.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.5f);
        }
        isEnter = false;
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
            HpBarEffect.color = new Color(1, 0.827f, 0.635f, 1);
       
        HpBar.fillAmount = hpFill;
        yield return new WaitForSeconds(0.15f);
        HpBarEffect.color = new Color(1, 1, 1, 0);
        //HpBarBack.fillAmount = Convert.ToSingle(hp) / Convert.ToSingle(maxHp);

    }
    public int AbyssGage
    {
        set
        {
            avssGage = value;
            AbyssBar.fillAmount = Convert.ToSingle(avssGage) / Convert.ToSingle(100);
         

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
