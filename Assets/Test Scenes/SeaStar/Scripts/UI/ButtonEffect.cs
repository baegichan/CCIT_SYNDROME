using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite Default;
    public Sprite Hover;
    public Sprite Disable;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(transform.GetComponent<Image>().sprite != Default)
        {
            transform.GetComponent<Image>().sprite = Hover;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.GetComponent<Image>().sprite != Default)
        {
            transform.GetComponent<Image>().sprite = Disable;
        }
    }
}
