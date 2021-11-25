using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalEvent : MonoBehaviour
{
    
  //방 정도 싱글톤으로 저장  그대이터에 따라 다음방 탐색 후 이동 
  //방 클로즈 될때마다 현재방 잠금
   public  bool DoorIsLock=false;
   public  bool Teleport_able = true;

    public Potals.PotalType PotalType;
    public Potals.PotalType PotalDirection;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (MapManager.s_Instace.Map_Lock != true)
        {


            if (!DoorIsLock)
            {
                if (Teleport_able == true)
                {
                    Movement(other.gameObject);
                    // MapManager.s_Instace.PotalMove(PotalType);
                }


            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Mapon()); 
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
    
    }
    public void SetinterBlock()
    {
        var linedrawer = GetComponent<MapLineDraw>();
        Vector2[] VertexPoints = new Vector2[5];

        GameObject Block = (GameObject)Resources.Load("Wall");
       
        var Potal = Block.GetComponent<MapLineDraw>();
        switch (PotalType)
        {
            case Potals.PotalType.L:
                Instantiate(Block, new Vector2(transform.position.x-Mathf.Abs(linedrawer.L_Area)- Mathf.Abs(linedrawer.R_Area), transform.position.y), Quaternion.identity, this.transform);

                break;
            case Potals.PotalType.R:
                Instantiate(Block, new Vector2(transform.position.x + Mathf.Abs(linedrawer.L_Area)+ Mathf.Abs(linedrawer.R_Area), transform.position.y), Quaternion.identity, this.transform);


                break;
            case Potals.PotalType.T:
                Instantiate(Block, new Vector2(transform.position.x, transform.position.y + Mathf.Abs(linedrawer.T_Area) + Mathf.Abs(linedrawer.B_Area)), Quaternion.identity, this.transform);

                break;
            case Potals.PotalType.B:
                Instantiate(Block, new Vector2(transform.position.x, transform.position.y - Mathf.Abs(linedrawer.T_Area) - Mathf.Abs(linedrawer.B_Area)), Quaternion.identity, this.transform);

                break;
        }


        Potal.GetComponent<EdgeCollider2D>().points = VertexPoints;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!DoorIsLock)
        {
            Teleport_able = true;
        }
        
    }
    
    IEnumerator Mapon()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<EdgeCollider2D>().enabled = true;
        Teleport_able = true;
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
    switch(PotalType)
    {
            case Potals.PotalType.L:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x-1, MapManager.s_Instace.PCurrent_Room.y);
                Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.R:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x+1, MapManager.s_Instace.PCurrent_Room.y);
                Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.T:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x, MapManager.s_Instace.PCurrent_Room.y+1);
                Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.B:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x, MapManager.s_Instace.PCurrent_Room.y-1);
                Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().T_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().T_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().T_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
        }
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
