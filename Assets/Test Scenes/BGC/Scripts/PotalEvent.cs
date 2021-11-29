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
    if(other.tag=="Player")
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
        Vector2[] inputVector = new Vector2[5];
        var Potal = Block.GetComponent<MapLineDraw>();
        inputVector[0] = new Vector2(linedrawer.L_Area, linedrawer.T_Area);
        inputVector[1] = new Vector2(linedrawer.R_Area, linedrawer.T_Area);
        inputVector[2] = new Vector2(linedrawer.R_Area, linedrawer.B_Area);
        inputVector[3] = new Vector2(linedrawer.L_Area, linedrawer.B_Area);
        inputVector[4] = new Vector2(linedrawer.L_Area, linedrawer.T_Area);
        switch (PotalDirection)
        {
            case Potals.PotalType.L:
               var walll= Instantiate(Block, new Vector3(transform.position.x-Mathf.Abs(linedrawer.L_Area)- Mathf.Abs(linedrawer.R_Area), transform.position.y,3), Quaternion.identity, this.transform);
                EdgeCollider2D LPotal = walll.GetComponent<EdgeCollider2D>();
                LPotal.points = inputVector;
                //MapLineDraw Walll = walll.GetComponent<MapLineDraw>();
                //Walll.LineDrow(Potal.L_Area, Potal.R_Area, Potal.T_Area, Potal.B_Area);
                break;
            case Potals.PotalType.R:
                var wallr = Instantiate(Block, new Vector3(transform.position.x + Mathf.Abs(linedrawer.L_Area)+ Mathf.Abs(linedrawer.R_Area), transform.position.y,3), Quaternion.identity, this.transform);
                //MapLineDraw Wallr = wallr.GetComponent<MapLineDraw>();
                //Wallr.LineDrow(Potal.L_Area, Potal.R_Area, Potal.T_Area, Potal.B_Area);
                EdgeCollider2D RPotal = wallr.GetComponent<EdgeCollider2D>();
 
                RPotal.points = inputVector;
                break;
            case Potals.PotalType.T:
                var wallt = Instantiate(Block, new Vector3(transform.position.x, transform.position.y + Mathf.Abs(linedrawer.T_Area) + Mathf.Abs(linedrawer.B_Area),3), Quaternion.identity, this.transform);
                //MapLineDraw Wallt = wallt.GetComponent<MapLineDraw>();
                // Wallt.LineDrow(Potal.L_Area, Potal.R_Area, Potal.T_Area, Potal.B_Area);
                EdgeCollider2D TPotal = wallt.GetComponent<EdgeCollider2D>();
                TPotal.points = inputVector;
                break;
            case Potals.PotalType.B:
                var wallb = Instantiate(Block, new Vector3(transform.position.x, transform.position.y - Mathf.Abs(linedrawer.T_Area) - Mathf.Abs(linedrawer.B_Area),3), Quaternion.identity, this.transform);
                // MapLineDraw Wallb = wallb.GetComponent<MapLineDraw>();
                // Wallb.LineDrow(Potal.L_Area, Potal.R_Area, Potal.T_Area, Potal.B_Area);
                EdgeCollider2D BPotal = wallb.GetComponent<EdgeCollider2D>();
                BPotal.points = inputVector;
                break;
        }


        //Potal.GetComponent<EdgeCollider2D>().points = VertexPoints;
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
    public void PotalD_setting(Potal.Potal_type PotalTypes)
    {
        switch (PotalTypes)
        {
            case Potal.Potal_type.LeftPotal:
                PotalDirection = Potals.PotalType.L;

                break;
            case Potal.Potal_type.RightPotal:
                PotalDirection = Potals.PotalType.R;

                break;
            case Potal.Potal_type.TopPotal:
                PotalDirection = Potals.PotalType.T;

                break;
            case Potal.Potal_type.BottomPotal:
                PotalDirection = Potals.PotalType.B;

                break;


        }

    }

    public void Movement(GameObject Player)
    {
    switch(PotalType)
    {
            case Potals.PotalType.L:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x-1, MapManager.s_Instace.PCurrent_Room.y);
                Potals instance1 = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>();
                Player.transform.position = new Vector3(instance1.R_Potal.transform.position.x-2, instance1.R_Potal.transform.position.y , instance1.R_Potal.transform.position.z);//MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().R_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.R:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x+1, MapManager.s_Instace.PCurrent_Room.y);
                Potals instance2 = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>();
                Player.transform.position = new Vector3(instance2.L_Potal.transform.position.x+2, instance2.L_Potal.transform.position.y, instance2.L_Potal.transform.position.z);
                //Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().L_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.T:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x, MapManager.s_Instace.PCurrent_Room.y+1);
                Potals instance3 = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>();
                Player.transform.position = new Vector3(instance3.B_Potal.transform.position.x, instance3.B_Potal.transform.position.y-2, instance3.B_Potal.transform.position.z);
                Player.transform.position = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.transform.position;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.GetComponent<PotalEvent>().Teleport_able = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>().B_Potal.GetComponent<EdgeCollider2D>().enabled = false;
                MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).GetComponent<Room_data>().Visit();
                MapManager.s_Instace.Minimap.CurrentOn(MapManager.s_Instace.PCurrent_Room);
                break;
            case Potals.PotalType.B:
                MapManager.s_Instace.Minimap.CurrentOff(MapManager.s_Instace.PCurrent_Room);
                MapManager.s_Instace.PCurrent_Room = new Vector2(MapManager.s_Instace.PCurrent_Room.x, MapManager.s_Instace.PCurrent_Room.y-1);
                Potals instance4 = MapManager.s_Instace.Maps.transform.GetChild(MapManager.s_Instace.currentindexreturner()).transform.Find("Potals(Clone)").GetComponent<Potals>();
                Player.transform.position = new Vector3(instance4.T_Potal.transform.position.x, instance4.T_Potal.transform.position.y+2, instance4.T_Potal.transform.position.z);
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
