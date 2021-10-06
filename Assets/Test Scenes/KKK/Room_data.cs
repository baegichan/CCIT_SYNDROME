using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_data : MonoBehaviour
{

    public int map_code;
    public bool  Left, Right,Top, Bottom;
    public bool Room_Created = false;
    
   
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
  
    private void Start()
    {

    }
    private void Update()
    {
        if (this.Room_Created == true)
        {
            if (this.map_code == 1)
            {
                this.Top = true;
            }
            else if (this.map_code == 2)
            {
                this.Right = true;
            }
            else if (this.map_code == 3)
            {
                this.Bottom = true;
            }
            else if (this.map_code == 4)
            {
                this.Left = true;
            }
            else if (this.map_code == 5)
            {
                this.Top = true;
                this.Right = true;
            }
            else if (this.map_code == 6)
            {
                this.Top = true;
                this.Bottom = true;
            }
            else if (this.map_code == 7)
            {
                this.Top = true;
                this.Left = true;
            }
            else if (this.map_code == 8)
            {
                this.Right = true;
                this.Bottom = true;
            }
            else if (this.map_code == 9)
            {
                this.Right = true;
                this.Left = true;
            }
            else if (this.map_code == 10)
            {
                this.Left = true;
                this.Bottom = true;
            }
            else if (this.map_code == 11)
            {
                this.Right = true;
                this.Top = true;
                this.Bottom = true;
            }
            else if (this.map_code == 12)
            {
                this.Right = true;
                this.Top = true;
                this.Left = true;
            }
            else if (this.map_code == 13)
            {
                this.Left = true;
                this.Top = true;
                this.Bottom = true;
            }
            else if (this.map_code == 14)
            {
                this.Right = true;
                this.Left = true;
                this.Bottom = true;
            }

            else if (this.map_code == 15)
            {
                this.Right = true;
                this.Bottom = true;
                this.Top = true;
                this.Left = true;
            }
        }

    }
}
