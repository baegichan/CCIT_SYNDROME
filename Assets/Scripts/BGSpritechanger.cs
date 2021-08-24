using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpritechanger : MonoBehaviour
{
    public SpriteRenderer targetsprite;
    public Sprite[] Sprites;
    public float time;
    private float time_counter;
    private int counter = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        time_counter=Mathf.Clamp(time_counter+=Time.deltaTime,0,time);
        if(time_counter==time)
        {
            BGspriteschanger();
            time_counter = 0;
        }
    }
    void BGspriteschanger()
    {
        if(counter!=Sprites.Length)
        {
            targetsprite.sprite = Sprites[counter];
            counter += 1;
            if(counter == Sprites.Length)
             {
                counter = 0;
             }
        }
     
    }
}
