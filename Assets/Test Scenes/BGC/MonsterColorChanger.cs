using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterColorChanger : MonoBehaviour
{
    public Character Cha;
    public SpriteRenderer[] Materials=null;
    [SerializeField]
    List<Color32> colors = new List<Color32>();
    public Color DefaultColor = new Color(1, 0.3686275f, 0.1764706f, 1f);
    public float Change_time = 0.1f;
    private int hp;
    private void Update()
    {
       
        if(Cha.Hp_Current<hp)
        {
            Debug.Log("AAA");
            Damaged();
            hp = Cha.Hp_Current;
        }
    }
    private void Start()
    {
        hp = Cha.Hp_Current;
        Debug.Log(Cha.Hp_Current);
        for (int i = 0; i < Materials.Length; i++)
        {
            colors.Add(Materials[i].color);


        }
     
    }

    public void Damaged()
    {
        StartCoroutine(ChangeColor());
    }
    public IEnumerator ChangeColor()
    {
        Debug.Log("Rune");
        for (int i = 0; i < Materials.Length; i++)
        {

            Materials[i].color = DefaultColor;
            //Materials[i].material.SetColor("_Color", DefaultColor);
        }

        yield return new WaitForSeconds(Change_time);
   
        for (int i = 0; i < Materials.Length; i++)
        {
            Materials[i].color = colors[i];
            //Materials[i].material.SetColor("_Color", colors[i]);
        }
    }

}
