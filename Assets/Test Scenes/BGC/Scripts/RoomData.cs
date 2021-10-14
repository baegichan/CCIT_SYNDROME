using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{

    public GameObject Room;
    public int RoomindexX, RoomindexY;
    public bool IsCreated = false;
    public int RoomCode=0;
    public bool VisitedRoom = true;
    public uint Test;
   public bool L_Brige, R_Brige, T_Brige, B_Brige;
    public GameObject L_Room, R_Room, T_Room, B_Room;

    public enum Roomdir
    {
        Left,
        Right,
        Top,
        Bottom
    }
    public int GetArroundRoom()
    {
        int o = 0;
        
        if (L_Room != null)
        {
            if (L_Room.transform.childCount > 0 && L_Room.GetComponent<RoomData>().R_Brige)
            {
                o += 8;
            }
        }
        if (R_Room != null)
        {
            if (R_Room.transform.childCount > 0 && R_Room.GetComponent<RoomData>().L_Brige)
            {
                o += 4;
            }
        }
        if (T_Room != null)
        {
            if (T_Room.transform.childCount > 0 && T_Room.GetComponent<RoomData>().B_Brige)
            {
                o += 2;
            }
        }
        if (B_Room != null)
        {
            if (B_Room.transform.childCount > 0 && B_Room.GetComponent<RoomData>().T_Brige)
            {
                o += 1;
            }
        }
        return o;
    }
    public bool Check(Roomdir dir)
    {
      switch(dir)
        {
            case Roomdir.Left:
                if (L_Room != null)
                {
                    if (L_Room.GetComponent<RoomData>().IsCreated)
                    {
                        if (L_Room.GetComponent<RoomData>().R_Brige == L_Brige)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            case Roomdir.Right:
                if (R_Room != null)
                {
                    if (R_Room.GetComponent<RoomData>().IsCreated)
                    {
                        if (R_Room.GetComponent<RoomData>().L_Brige == R_Brige)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            case Roomdir.Top:
                if (T_Room != null)
                {
                    if (T_Room.GetComponent<RoomData>().IsCreated)
                    {
                        if (T_Room.GetComponent<RoomData>().B_Brige == T_Brige)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }    

            case Roomdir.Bottom:
                if (B_Room != null)
                {
                    if (B_Room.GetComponent<RoomData>().IsCreated)
                    {
                        if (B_Room.GetComponent<RoomData>().T_Brige == B_Brige)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
        }
        return true;
    }
    public void SetArroundRoom(Roomdir roomdirection, GameObject room)
    {
        switch(roomdirection)
        {
            case Roomdir.Left:
                L_Room = room;
                break;
            case Roomdir.Right:
                R_Room = room;
                break;
            case Roomdir.Top:
                T_Room = room;
                break;
            case Roomdir.Bottom:
                B_Room = room;
                break;
        }
    }
    public void StartRoomSetting()
    {       
        RoomCode=Random.Range(5, 16);
        for (int i = 0; i < 4; i++)
        {
            if ((RoomCode >> i & 0b0001) == 1)
            {
                switch (i)
                {
                    case 0:
                        B_Brige = true;
                        break;
                    case 1:
                        T_Brige = true;
                        break;
                    case 2:
                        R_Brige = true;
                        break;
                    case 3:
                        L_Brige = true;
                        break;
                }
            }
        }
        VisitedRoom = true;
        IsCreated = true;
    }
    public bool ChangeRoomCode(int PreMask)
    {
     B_Brige = false;
     T_Brige = false;
     R_Brige = false;
     L_Brige = false;
        int instanceRandom = Random.Range(1, 17);
        for (int i = 0; i < 4; i++)
        {
            if ((instanceRandom >> i & 0b0001) == 1)
            {
                switch (i)
                {
                    case 0:
                        B_Brige = true;
                        break;
                    case 1:
                        T_Brige = true;
                        break;
                    case 2:
                        R_Brige = true;
                        break;
                    case 3:
                        L_Brige = true;
                        break;
                }
            }
        }
        if (((GetArroundRoom() & instanceRandom) == GetArroundRoom())&& Check(Roomdir.Left) && Check(Roomdir.Right) && Check(Roomdir.Bottom) && Check(Roomdir.Top))
        { 
            switch (PreMask)
            {
                //0bLRTB
                case 0b0001:
                    if ((instanceRandom >> 1 & 0b0001) == 1)
                    {
                        IsCreated = true;
                        RoomCode = instanceRandom;
                        return true;
                    }
                    break;
                case 0b0010:
                    if ((instanceRandom >> 0 & 0b0001) == 1)
                    {
                        IsCreated = true;
                        RoomCode = instanceRandom;
                        return true;
                    }
                    break;
                case 0b0100:
                    if ((instanceRandom >> 3 & 0b0001) == 1)
                    {
                        IsCreated = true;
                        RoomCode = instanceRandom;
                        return true;
                    }                
                    break;
                case 0b1000:
                    if ((instanceRandom >> 2 & 0b0001) == 1)
                    {
                        IsCreated = true;
                        RoomCode = instanceRandom;
                        return true;
                    }
                    break;
            }
            return false;
        }
        else
        {
            ChangeRoomCode(PreMask);
            return false;
        }
    }
}
