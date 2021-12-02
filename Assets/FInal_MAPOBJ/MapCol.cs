using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCol : MonoBehaviour
{
    private MapLineDraw Potal;
    private PotalEvent PotalEvent;
    private void Start()
    {
        PotalEvent = transform.parent.GetComponent<PotalEvent>();
            Potal = transform.parent.GetComponent<MapLineDraw>();
                transform.localScale = new Vector3(Mathf.Abs(Potal.L_Area)+ Mathf.Abs(Potal.R_Area), Mathf.Abs(Potal.T_Area) + Mathf.Abs(Potal.B_Area), 1);
                transform.localPosition = new Vector3((Potal.L_Area + Potal.R_Area)/2, (Potal.T_Area + Potal.B_Area)/2, 1);
       
      
    }
}
