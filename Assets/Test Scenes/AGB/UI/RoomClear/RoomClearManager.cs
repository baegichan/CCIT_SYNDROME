using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomClearManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Camera;
    public GameObject Gagepos;
    public GameObject Particle;
    public Image bar;
   
    public GameObject OnKey;
    public GameObject OffKey;
    public GameObject BossImage;

   

    public int MaxClear = 6;
    int clearNum = 0;
    bool isKey = false;
    bool isclear = false;
    bool isTarClear = false;


    GameObject AfterPa;
    
    private static RoomClearManager _clear;
    public static RoomClearManager clear
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_clear)
            {
                _clear = FindObjectOfType(typeof(RoomClearManager)) as RoomClearManager;

                if (_clear == null)
                    Debug.Log("no Singleton obj");
            }
            return _clear;
        }
    }

    private void Update()
    {
        if(isclear)
        {
            AfterPa.transform.position = new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z + 20);
        }
        else if(isTarClear)
        {
            AfterPa.transform.position=  new Vector3(Gagepos.transform.position.x, Gagepos.transform.position.y, Camera.transform.position.z + 20);
        }
    }

    public void RoomClear()
    {
        if(MaxClear>clearNum)
        StartCoroutine(ClearRoom());
    }
    IEnumerator ClearRoom()
    {
        AfterPa = Instantiate(Particle, new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z + 20), Quaternion.identity);
        isclear = true;
        yield return new WaitForSeconds(2f);
        isclear = false;
        isTarClear = true;
        yield return new WaitForSeconds(3f);
        isTarClear = false;
        Destroy(AfterPa);
        GaeUp();
    }


    void GaeUp()
    {
        clearNum++;
        bar.fillAmount = Convert.ToSingle(clearNum) / Convert.ToSingle(MaxClear);

        if (bar.fillAmount > 0.5 && !isKey)
        {
            
            isKey = true;
            OnKey.SetActive(true);
            OffKey.SetActive(false);
            ClearAniController.s_instance.BossOpen();
            //여기에 보스방 문 열림추가
            
        }

        if (bar.fillAmount == 1)
        {
            BossImage.SetActive(true);
            ClearAniController.s_instance.BossOn();

            //여기에 보스방 위치 추가
        }
           
    }
}
