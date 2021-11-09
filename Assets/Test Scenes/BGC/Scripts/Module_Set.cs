using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module_Set : MonoBehaviour
{
    // Start is called before the first frame update
    private MoveModule Move_Module;
    private AttackModule Attack_Module;
    private SpecialModule Speacial_Module;
    private Sensol Sensol_Module;
    public MoveModule MoveModule
    {
        get {return Move_Module;}
        set { Move_Module = value;}
    }
    public AttackModule AttackModule
    {
        get { return Attack_Module; }
        set { AttackModule= value; }

    }
    public SpecialModule SpeacialModule
    {
        get { return Speacial_Module; }
        set { Speacial_Module = value; }

    }
    public Sensol SensolModule
    {
      
        get { return Sensol_Module; }
        set { Sensol_Module = value; }

    }
}
