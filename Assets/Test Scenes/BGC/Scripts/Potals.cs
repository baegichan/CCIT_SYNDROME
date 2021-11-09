using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potals : MonoBehaviour
{
    public GameObject L_Potal;
    public GameObject R_Potal;
    public GameObject T_Potal;
    public GameObject B_Potal;
    

    
    public enum PotalType
    {
        L,R,T,B
    }

    public void SetPotal(PotalType potaltype,GameObject Potal)
    {
        switch (potaltype)
        {
            case PotalType.L:
                L_Potal = Potal;
                break;
            case PotalType.R:
                 R_Potal= Potal;
                break;
            case PotalType.T:
                 T_Potal= Potal;
                break;
            case PotalType.B:
                 B_Potal= Potal;
                break;
        }
    }
public GameObject GetPotal(PotalType potaltype)
    {
        switch(potaltype)
        {
            case PotalType.L:
                return L_Potal;
         
            case PotalType.R:
                return R_Potal;
               
            case PotalType.T:
                return T_Potal;
          
            case PotalType.B:
                return B_Potal;
        }
        return null;
    }
}
