using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEvent : MonoBehaviour
{
    
  //�� ���� �̱������� ����  �״����Ϳ� ���� ������ Ž�� �� �̵� 
  //�� Ŭ���� �ɶ����� ����� ���
    bool DoorIsLock=false;
    

    public Potals.PotalType PotalType;

    private void OnTriggerEnter(Collider other)
    {
        //�̺�Ʈ �߰�
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
