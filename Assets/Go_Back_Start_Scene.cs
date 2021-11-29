using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Go_Back_Start_Scene : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
       // Debug.Log(23);
        this.GetComponent<Image>().color = new Color32(162, 162, 162, 255);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(23);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log(25);
        For_Fade.FadeOff_To_StartRoom();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
