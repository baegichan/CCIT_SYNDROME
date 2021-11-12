using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_GMD : PET
{
   
    void Update()
    {
            if(Modules.MoveModule)
            {
            Modules.MoveModule.Move();
            }
        if (Modules.SensolModule)
        {

            if (Modules.SensolModule.Detected_Object != null)
            {
                if (Modules.SensolModule.Detected_Object[0] != null)
                {
                    Modules.MoveModule.Jump();
                }
                if (Modules.SensolModule.Detected_Object[1] != null)
                {
                    Modules.MoveModule.Jump();
                }
                if (Modules.SensolModule.Detected_Object[2] != null)
                {
                    Modules.MoveModule.Jump();
                }
            }
        }
      
    }
}
