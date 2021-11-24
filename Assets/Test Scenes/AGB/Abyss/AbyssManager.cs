using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{

    #region �̱���

    private static AbyssManager _abyss;

    public static AbyssManager abyss
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_abyss)
            {
                _abyss = FindObjectOfType(typeof(AbyssManager)) as AbyssManager;

                if (_abyss == null)
                    Debug.Log("no Singleton obj");
            }
            return _abyss;
        }
    }

    private void Awake()
    {
        if (_abyss == null)
        {
            _abyss = this;
        }
        // �ν��Ͻ��� �����ϴ� ��� ���λ���� �ν��Ͻ��� �����Ѵ�.
        else if (_abyss != this)
        {
            Destroy(gameObject);
        }
        // �Ʒ��� �Լ��� ����Ͽ� ���� ��ȯ�Ǵ��� ����Ǿ��� �ν��Ͻ��� �ı����� �ʴ´�.
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    #region ����




    [Header("Abyss Variable")]
    //���� �Ȱ�
    public int darkfog = 0;

    public int maxAbyssGage = 100;
   
    public int abyssGage = 1;
    //hp ������
    public int hpGage = 0;

    //��� ������ �پ��� ��

    public int abyssGageConsumption = 1;

    //��� ������ �پ��� �ð�
    public float abyssConsumptionTime = 1f;

    //�ɿ� ���� ����
    private bool isAbyssStay = false;
    //�ɿ� �ڷ�ƾ �������� ����
    private bool isAbyssEnd = true;

    public enum AbyssState { Reality, Abyss };


    //[Header("MonsterBox")]
    //public GameObject RealBox;
    //public GameObject AbyssBox;



    //[Header("CameraLayer")]
    //public Camera MainCamera;
    //public int realLayer;
    //public int abyssLayer;

    //[Header("StateManager")]
    //public StateManager stateManager;







    [Header("Temporary")]
    public Text fogText;

    public AbyssState abyssState = new AbyssState();
    
    #endregion

  
    // Start is called before the first frame update
    void Start()
    {
    
        abyssState = AbyssState.Reality;
        fogText.text = Convert.ToString(darkfog);
 
      
    }


    // Update is called once per frame
    void Update()
    {
        //��ü   if (Input.GetKeyDown(settingmanager.GM.abyss))
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isAbyssStay == false && isAbyssEnd == true)
                GoAbyss();
            else
                GoReal();

        }
    }

    IEnumerator AbyssResource()
    {
       
        isAbyssEnd = false;
        int exint;
        while (isAbyssStay)
        {
            exint = abyssGage - abyssGageConsumption;
            if (exint > 0)
                AbyssGage -= abyssGageConsumption;
            else
            {
                AbyssGage = 0;
                hpGage -= abyssGageConsumption;
            }

            if (hpGage <= 0)
            {
                if (hpGage < 0)
                    hpGage = 0;
            
                isAbyssStay = false;
                abyssState = AbyssState.Reality;
                break;

            }
       
            yield return new WaitForSeconds(abyssConsumptionTime);
        }
        abyssState = AbyssState.Reality;
       
        isAbyssEnd = true;
    }


    #region �ɿ��� ��� ���� �Լ�

    /// <summary>
    /// �ɿ� �̵� �Լ�
    /// </summary>
    public void GoAbyss()
    {
        
        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum�� ���°� �Է�
       
        StartCoroutine(AbyssResource()); // �ɿ� ���ҽ� �Ҹ� �ڷ�ƾ ����
    }

    /// <summary>
    /// ���� �̵� �Լ�
    /// </summary>
    public void GoReal()
    {

        abyssState = AbyssState.Reality;
        isAbyssStay = false;
    }

    #endregion
    //���� óġ�� or �Һ�� �Լ�

    #region ��� ���� ���� getset 
    public int Darkfog
    {
        get
        {
            fogText.text = Convert.ToString(darkfog);
            return darkfog;
        }

        set
        {
            darkfog = value;
            fogText.text = Convert.ToString(darkfog);
            
        }
    }

    public int AbyssGage
    {
        get
        {
            return abyssGage;
        }

        set
        {
            abyssGage = value;
           StateManager.state.AbyssGage = abyssGage;
        }
    }


    #endregion


    
    #region ���ҽ� ����


   

    /// <summary>
    /// �ɿ������� ����
    /// </summary>
    /// <param Gage="int�� ������ �߰��� �־��ٰ�"></param>
    public void GetAbyssGage(int gage)
    {
        int ex = abyssGage + gage;
        if (ex > maxAbyssGage)
            abyssGage = maxAbyssGage;
        else
            abyssGage += gage;
   

    }
    #endregion
}
