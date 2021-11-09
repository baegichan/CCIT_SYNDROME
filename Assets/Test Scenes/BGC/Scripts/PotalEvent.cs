using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEvent : MonoBehaviour
{
    
    GameObject ConnectedPotal;
    bool DoorIsLock=false;


    public Potals.PotalType PotalType;

    private void OnTriggerEnter(Collider other)
    {
        //�̺�Ʈ �߰�
        Movement(other.gameObject);
    }

    public void Start()
    {
        transform.parent.GetComponent<Potals>().SetPotal(PotalType,gameObject);
    }
    public void Movement(GameObject Player)
    {
            if(DoorIsLock==false)
            {
              if(ConnectedPotal!=null)
                {
                //����Ȱɷ� �̵� ����
               }
            }
    }
    public void Connecting(GameObject Potal)
    {
        ConnectedPotal = Potal;
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
