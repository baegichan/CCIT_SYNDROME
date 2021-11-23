using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Unit_HPbar : MonoBehaviour
{
    
    public Slider Hp_bar;
    public Character Cha;
    float Max;
    public GameObject Target;
    public Vector2 AdditionalPosition;


    public Image BackGround;

    bool hpbarActive=false;
    // Start is called before the first frame update
    void Start()
    {
        Max = Cha.Hp_Max;
        Hp_bar.maxValue = Max;
        Hp_bar.value = Max;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)Target.transform.position + AdditionalPosition;
        if (!hpbarActive)
        if (Cha.Hp_Max != Cha.Hp_Current)
        {
                Hp_bar.gameObject.SetActive(true);
                Hp_bar.value = Cha.Hp_Current;
        }

        if(Hp_bar.value != Cha.Hp_Current)
        {
            Hp_bar.value = Cha.Hp_Current;
        
        }
       
    }

}
