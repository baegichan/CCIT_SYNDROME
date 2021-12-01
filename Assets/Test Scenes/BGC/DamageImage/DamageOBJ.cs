using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageOBJ : MonoBehaviour
{
   public TextMesh TM;
    public Text Text;
    public void DamageText(int i)
    {
         if(TM!=null)
          {
            TM.text = System.Convert.ToString(i);
        }
   
        if(Text!=null)
        {
            Text.text = System.Convert.ToString(i);
        }
        //TM.color = Color.red;
    }
    public void HealText(int i)
    {
        if (TM != null)
        {
            TM.text = System.Convert.ToString(i);
        }

        if (Text != null)
        {
            Text.text = System.Convert.ToString(i);
        }
    }
    public void DestroyTextObj()
    {
        Destroy(this.gameObject);
    }
}
