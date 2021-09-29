using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLineDraw : MonoBehaviour
{
    Vector2[] inputVector = new Vector2[5];
    public Color LineColor = new Color(1, 1, 1, 1);
    [Range(-50.0f, 50.0f)] public float L_Area;
    [Range(-50.0f, 50.0f)] public float R_Area;
    [Range(-50.0f, 50.0f)] public float T_Area;
    [Range(-50.0f, 50.0f)] public float B_Area;



    private void OnDrawGizmos()
    {
        


        Gizmos.color = LineColor;
        Gizmos.DrawLine(new Vector2(L_Area+this.transform.position.x, T_Area+this.transform.position.y), new Vector2(R_Area + this.transform.position.x, T_Area + this.transform.position.y));
        Gizmos.DrawLine(new Vector2(R_Area + this.transform.position.x, T_Area + this.transform.position.y), new Vector2(R_Area + this.transform.position.x, B_Area + this.transform.position.y));
        Gizmos.DrawLine(new Vector2(R_Area + this.transform.position.x, B_Area + this.transform.position.y), new Vector2(L_Area + this.transform.position.x, B_Area + this.transform.position.y));
        Gizmos.DrawLine(new Vector2(L_Area + this.transform.position.x, B_Area + this.transform.position.y), new Vector2(L_Area + this.transform.position.x, T_Area + this.transform.position.y));
        inputVector[0] = new Vector2(L_Area , T_Area );
        inputVector[1] = new Vector2(R_Area , T_Area);
        inputVector[2] = new Vector2(R_Area , B_Area );
        inputVector[3] = new Vector2(L_Area, B_Area );
        inputVector[4] = new Vector2(L_Area , T_Area );
        this.GetComponent<EdgeCollider2D>().points = inputVector;
    }

}
