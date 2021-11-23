using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class AbyssManager : MonoBehaviour
{

    #region 싱글톤

    private static AbyssManager _abyss;

    public static AbyssManager abyss
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
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
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_abyss != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }
    #endregion
    #region 변수


    [Header("InputKey")]
    public char abyssKey = 'q';

    [Header("Abyss Variable")]
    //검은 안개
    private int darkfog = 0;
    public int maxAbyssGage = 100;
    //어비스 게이지
    public int abyssGage = 0;
    //hp 게이지
    public int hpGage = 0;

    //어비스 게이지 줄어드는 값

    public int abyssGageConsumption = 1;

    //어비스 게이지 줄어드는 시간
    public float abyssConsumptionTime = 1f;

    //심연 유지 여부
    private bool isAbyssStay = false;
    //심연 코루틴 끝났는지 여부
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
    //몬스터

    int[] monsterId = new int[50];
    Vector3[] monsterPos = new Vector3[50];

    #endregion

  
    // Start is called before the first frame update
    void Start()
    {
        //AbyssBox.SetActive(false);
        abyssState = AbyssState.Reality;
        fogText.text = Convert.ToString(darkfog);
        //보이게
        MainCamera.cullingMask = 1 << 11;
        //안보이게 하기
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


    #region 심연의 경계 변경 함수

    /// <summary>
    /// 심연 이동 함수
    /// </summary>
    void GoAbyss()
    {
        //보이게하기
        MainCamera.cullingMask = 1 << abyssLayer;
        //안보이게 하기
        MainCamera.cullingMask = ~(1 << realLayer);

        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum에 상태값 입력
       
        AbyssMonsterSpawn(); // 심연 몬스터가 있을경우 실행
        AbyssBox.SetActive(true);// 심연 몬스터 그룹  활성화
        RealBox.SetActive(false);// 현실 몬스터 그룹 비활성화
        StartCoroutine("AbyssResource"); // 심연 리소스 소모 코루틴 실행
    }

    /// <summary>
    /// 현실 이동 함수
    /// </summary>
    void GoReal()
    {
        //보이게하기
        MainCamera.cullingMask = 1 << realLayer;
        //안보이게 하기
        MainCamera.cullingMask = ~(1 << abyssLayer);
        AbyssBox.SetActive(false);// 현실 몬스터 그룹  활성화
        RealBox.SetActive(true);  // 심연 몬스터 그룹 비활성화
        abyssState = AbyssState.Reality;
        isAbyssStay = false;
    }

    #endregion
    //몬스터 처치시 or 소비시 함수

    #region 어비스 관련 변수 getset 
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


    #region 심연 몬스터 관련 함수

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
        //들어오면 hp값 공격력 더하고 스택에 집어넣기
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

    #region 리소스 관리


   

    /// <summary>
    /// 심연게이지 습득
    /// </summary>
    /// <param Gage="int값 게이지 추가할 넣어줄것"></param>
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
