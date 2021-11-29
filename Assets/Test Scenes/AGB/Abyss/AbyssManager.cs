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




    [Header("Abyss Variable")]
    //검은 안개
    public int darkfog = 0;

    public int maxAbyssGage = 100;

    public int abyssGage = 1;
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

    private bool isCoolTime = true;

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










    public AbyssState abyssState = new AbyssState();

    #endregion


    // Start is called before the first frame update
    void Start()
    {

        abyssState = AbyssState.Reality;
        //StateManager.state.DarkFog = darkfog;


    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && isCoolTime)
        {
            isCoolTime = false;
            StartCoroutine(CoolTime());
            MapChangeTester.AbyssMask.test.SetTrigger("Changed");
            if (abyssState == AbyssState.Abyss)
                GoReal();
            else
                GoAbyss();
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(1f);
        isCoolTime = true;
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
    public void GoAbyss()
    {

        isAbyssStay = true;
        abyssState = AbyssState.Abyss; // enum에 상태값 입력

        StartCoroutine(AbyssResource()); // 심연 리소스 소모 코루틴 실행
    }

    /// <summary>
    /// 현실 이동 함수
    /// </summary>
    public void GoReal()
    {

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
            StateManager.state.DarkFog = darkfog;
            return darkfog;

        }

        set
        {
            darkfog = value;
            StateManager.state.DarkFog = darkfog;

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
