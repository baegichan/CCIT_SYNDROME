using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_data : MonoBehaviour
{

    public int map_code;
    public bool  Top,Right,Bottom,Left;
    public bool Room_Created = false;
    public Vector2 Map_index;
    PolygonCollider2D MapPolygon=null;
    SpriteRenderer spr;
    public Sprite[] sp;
    public bool VisitedRoom= false;
   
    public void Visit()
    {
        VisitedRoom = true;
    }
    public PolygonCollider2D MapPoly { get {return MapPolygon; } set {MapPolygon=value; } }
    public enum RoomType
    {
    Nomal,
    StartRoom,
    Boss,
    Shop,
    Crane
    }
    public RoomType Room_Type = RoomType.Nomal;

    
    public void CheckLocation()
    {
    
    }
    public void SetMapData()
    {
        //Left = false; Right = false; Top = false; Bottom =false;
        map_code = Random.Range(1, 16);
        Room_Created = true;
        switch (map_code)
        {
            case 1:
                Top = true;
                break;
            case 2:
                Right = true;
                break;
            case 3:
                Bottom = true;
                break;
            case 4:
                Left = true;
                break;
            case 5:
                Top = true;
                Right = true;
                break;
            case 6:
                Top = true;
                Bottom = true;
                break;
            case 7:
                Top = true;
                Left = true;
                break;
            case 8:
                Right = true;
                Bottom = true;
                break;
            case 9:
                Right = true;
                Left = true;
                break;
            case 10:
                Left = true;
                Bottom = true;
                break;
            case 11:
                Top = true;
                Bottom = true;
                Right = true;
                break;
            case 12:
                Top = true;
                Left = true;
                Right = true;
                break;
            case 13:
                Bottom = true;
                Left = true;
                Top = true;
                break;
            case 14:
                Bottom = true;
                Left = true;
                Right = true;
                break;
            case 15:
                Bottom = true;
                Left = true;
                Right = true;
                Top = true;
                break;
        }
    }
    
    private void FixedUpdate()
    {
        //if(Room_Type == RoomType.Boss)
        CheckLocation();
    }
    public bool Check_Right_Connect()
    {
        return Right;
    }
    public bool Check_Left_Connect()
    {
        return Left;
    }
    public bool Check_Top_Connect()
    {
        return Top;
    }
    public bool Check_Bottom_Connect()
    {
        return Bottom;

    }



    
}
