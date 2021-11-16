using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEvent : MonoBehaviour
{
    
  //방 정도 싱글톤으로 저장  그대이터에 따라 다음방 탐색 후 이동 
  //방 클로즈 될때마다 현재방 잠금
    bool DoorIsLock=false;
    

    public Potals.PotalType PotalType;

    private void OnTriggerEnter(Collider other)
    {
        //이벤트 추가
        if(!DoorIsLock)
        {
            Movement(other.gameObject);
        }
      
    }
  
    public void Potal_setting(Potal.Potal_type PotalTypes)
    {
        switch (PotalTypes)
        {
            case Potal.Potal_type.LeftPotal:
                PotalType = Potals.PotalType.L;
                break;
            case Potal.Potal_type.RightPotal:
                PotalType = Potals.PotalType.R;
                break;
            case Potal.Potal_type.TopPotal:
                PotalType = Potals.PotalType.T;
                break;
            case Potal.Potal_type.BottomPotal:
                PotalType = Potals.PotalType.B;
                break;

                
        }
        if(PotalTypes!=Potal.Potal_type.None)
        {
            transform.parent.GetComponent<Potals>().SetPotal(PotalType, gameObject);
        }

    }
 
    public void Movement(GameObject Player)
    {
         

    }
    public void Connecting(GameObject Potal)
    {
     
    }
    public void OpenDoor()
    {
        DoorIsLock = false ;

    }
    public void CloseDoor()
    {
        DoorIsLock = true;
    }
    
}
