using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMasking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        maskingimg = GetComponent<Image>();
        StartCoroutine(Wait());
    }
    float img_alpha=1;
    // Update is called once per frame
    private Image maskingimg;

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(8f);
        StartCoroutine(Fadeout());

    }
    public IEnumerator Fadeout()
    {
    if(img_alpha==0)
    {
            StopCoroutine(Fadeout());
    }
        yield return new WaitForSeconds(0.1f);
        maskingimg.color = new Color(0,0,0, img_alpha);
        img_alpha -= 0.1f;
        StartCoroutine(Fadeout());
      
    }
}
