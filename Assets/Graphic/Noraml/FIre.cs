using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIre : MonoBehaviour
{
    public Sprite[] Sprites = null;
    // Start is called before the first frame update
    int count = 0;
    SpriteRenderer SR;
    private void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(SpriteChanger());
    }
    // Update is called once per frame
    public IEnumerator SpriteChanger()
  {
        yield return new WaitForSeconds(0.1f);
        count++; 
        if(count==Sprites.Length)
        {
            count = 0;
        }
        SR.sprite = Sprites[count];
        StartCoroutine(SpriteChanger());
  }
}
