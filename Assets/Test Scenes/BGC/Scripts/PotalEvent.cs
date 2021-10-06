using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEvent : MonoBehaviour
{
    
    GameObject ConnectedPotal;
    bool DoorIsLock=false;




    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    IEnumerator BlackOutEvent()
    {
        //블랙아웃 이벤트 만들어야됨
        yield return new WaitForSeconds(1);
    }
    public void Movement(GameObject Player)
    {
            if(DoorIsLock==false)
            {
            
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
