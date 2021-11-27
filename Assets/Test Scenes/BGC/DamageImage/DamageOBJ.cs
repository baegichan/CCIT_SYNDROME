using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOBJ : MonoBehaviour
{
   public TextMesh TM;
    public void DamageText(int i)
    {
        TM.text = System.Convert.ToString(i);
        //TM.color = Color.red;
    }
    public void HealText(int i)
    {
        TM.text = System.Convert.ToString(i);
      //  TM.color = Color.green;
    }
    public void DestroyTextObj()
    {
        Destroy(this.gameObject);
    }
}
