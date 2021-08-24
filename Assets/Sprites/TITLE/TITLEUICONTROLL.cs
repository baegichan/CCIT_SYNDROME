using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TITLEUICONTROLL : MonoBehaviour, IPointerEnterHandler, IPointerUpHandler , IPointerExitHandler, IPointerDownHandler
{
    private GameObject titleimage;
    private GameObject titleimagebg;
    public Sprite[] images;
    // Start is called before the first frame update
    void Start()
    {
        titleimage = this.transform.GetChild(1).gameObject;
        titleimagebg = this.transform.GetChild(0).gameObject;
    }

    
    // Update is called once per frame

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleimagebg.GetComponent<Animator>().SetTrigger("ONUI");
        titleimage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        titleimage.GetComponent<Image>().sprite = images[1];
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        titleimage.GetComponent<Image>().color = new Color32(255, 255, 255, 170);
        titleimagebg.GetComponent<Animator>().SetTrigger("OUTUI");
        titleimage.GetComponent<Image>().sprite = images[0];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        titleimagebg.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        titleimage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        titleimage.GetComponent<Image>().sprite = images[2];
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        titleimagebg.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        titleimage.GetComponent<Image>().sprite = images[1];
    }
}
