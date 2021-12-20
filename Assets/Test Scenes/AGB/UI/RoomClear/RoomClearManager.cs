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
    private static RoomClearManager _clear;
    public static RoomClearManager clear
    {
        get
        {
            // �ν��Ͻ��� ���� ��쿡 �����Ϸ� �ϸ� �ν��Ͻ��� �Ҵ����ش�.
            if (!_clear)
            {
                _clear = FindObjectOfType(typeof(RoomClearManager)) as RoomClearManager;

                if (_clear == null)
                    Debug.Log("no Singleton obj");
            }
            return _clear;
        }
    }

    
    public void RoomClear()
    {
        if(MaxClear>clearNum)
        StartCoroutine(ClearRoom());
    }
    IEnumerator ClearRoom()
    {
        var d = Instantiate(Particle, Camera.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        d.transform.position = Gagepos.transform.position;
        yield return new WaitForSeconds(3f);
        Destroy(d);
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
            
            //���⿡ ������ �� �����߰�
            
        }

        if (bar.fillAmount == 1)
        {
            BossImage.SetActive(true);

            //���⿡ ������ ��ġ �߰�
        }
           
    }
}
