using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColorChanger : MonoBehaviour
{
    public SpriteRenderer[] Materials=null;
    List<Color32> colors = new List<Color32>();
    public Color DefaultColor = new Color(1, 0.3686275f, 0.1764706f, 1f);
    public float Change_time = 0.1f;
    private void Start()
    {
        for (int i = 0; i < Materials.Length; i++)
        {
            colors.Add(Materials[i].material.GetColor("_Color"));


        }
     
    }

    public void Damaged()
    {
        StartCoroutine(ChangeColor());
    }
    public IEnumerator ChangeColor()
    {
       
        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i].material.SetColor("_Color", DefaultColor);
        }

        yield return new WaitForSeconds(Change_time);
   
        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i].material.SetColor("_Color", colors[i]);
        }
    }

}
