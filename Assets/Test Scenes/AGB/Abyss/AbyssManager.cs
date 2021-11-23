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


    [Header("InputKey")]
    public char abyssKey = 'q';

    [Header("Abyss Variable")]
    //���� �Ȱ�
    private int darkfog = 0;
    public int maxAbyssGage = 100;
    //��� ������
    public int abyssGage = 0;
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


    [Header("MonsterBox")]
    public GameObject RealBox;
    public GameObject AbyssBox;

    [Header("AbyssMonsterPrefab")]
    public GameObject[] AbyssMonsterPrefab;

    [Header("CameraLayer")]
    public Camera MainCamera;
    public int realLayer;
    public int abyssLayer;

    [Header("StateManager")]
    public StateManager stateManager;







    [Header("Temporary")]
    public Text fogText;

    public AbyssState abyssState = new AbyssState();
    //����

    int[] monsterId = new int[50];
    Vector3[] monsterPos = new Vector3[50];

    #endregion

  
    // Start is called before the first frame update
    void Start()
    {
        //AbyssBox.SetActive(false);
        abyssState = AbyssState.Reality;
        fogText.text = Convert.ToString(darkfog);
        //���̰�
        MainCamera.cullingMask = 1 << 11;
        //�Ⱥ��̰� �ϱ�
        MainCamera.cullingMask = ~(1 << 12);

      
    }


    // Update is called once per frame
    void Update()
    {
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
    void GoAbyss()
    {
        //���̰��ϱ�
        MainCamera.cullingMask = 1 << abyssLayer;
        //�Ⱥ��̰� �ϱ�
        MainCamera.cullingMask = ~(1 << realLayer);

        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum�� ���°� �Է�
       
        AbyssMonsterSpawn(); // �ɿ� ���Ͱ� ������� ����
        AbyssBox.SetActive(true);// �ɿ� ���� �׷�  Ȱ��ȭ
        RealBox.SetActive(false);// ���� ���� �׷� ��Ȱ��ȭ
        StartCoroutine("AbyssResource"); // �ɿ� ���ҽ� �Ҹ� �ڷ�ƾ ����
    }

    /// <summary>
    /// ���� �̵� �Լ�
    /// </summary>
    void GoReal()
    {
        //���̰��ϱ�
        MainCamera.cullingMask = 1 << realLayer;
        //�Ⱥ��̰� �ϱ�
        MainCamera.cullingMask = ~(1 << abyssLayer);
        AbyssBox.SetActive(false);// ���� ���� �׷�  Ȱ��ȭ
        RealBox.SetActive(true);  // �ɿ� ���� �׷� ��Ȱ��ȭ
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
            stateManager.AbyssGage = abyssGage;
        }
    }


    #endregion


    #region �ɿ� ���� ���� �Լ�

    void AbyssMonsterSpawn()
    {

        for (int i = 0; i < monsterId.Length; i++)
        {
            if (monsterId[i] == 0 && i != 0)
            {
                monsterId = new int[50];
                break;
            }
            else
            {
                var d = Instantiate(AbyssMonsterPrefab[monsterId[i]], monsterPos[i], Quaternion.identity);
                d.transform.SetParent(AbyssBox.transform, false);
            }
        }
    }

    public void AbyssMonsterAdd(int id, Vector3 pos)
    {
        //������ hp�� ���ݷ� ���ϰ� ���ÿ� ����ֱ�
        for (int i = 0; i < monsterId.Length; i++)
        {
            if (monsterId[i] == 0)
            {
                monsterId[i] = id;
                monsterPos[i] = pos;
                if (i != 0)
                    break;
            }
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
