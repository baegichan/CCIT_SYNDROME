using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MiniMapDrag : MonoBehaviour, IEndDragHandler, IDragHandler
{
    public Vector2 BeginMousePosition = new Vector3(0, 0);
    //«ÿ¥Á πÊ - ∫§≈Õ
    public Vector2 MovePoint = new Vector3(0,0 );
    bool Loaded = false;
    [Range(0,500)]public float insensitive = 10;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           
         
            BeginMousePosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            MovePoint = Input.mousePosition;
            transform.localPosition -= new Vector3((BeginMousePosition.x - Input.mousePosition.x) / insensitive, (BeginMousePosition.y - Input.mousePosition.y) / insensitive, 0);
            BeginMousePosition.x = Input.mousePosition.x;
            BeginMousePosition.y = Input.mousePosition.y;
        }
    }
    private void OnEnable()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    public void OnDrag(PointerEventData eventData)
    {
        /*
        if(BeginMousePosition != eventData.position)
        {
            MovePoint = eventData.position;
            transform.localPosition -= new Vector3((BeginMousePosition.x - eventData.position.x)/ insensitive, (BeginMousePosition.y - eventData.position.y)/ insensitive, 0);
            BeginMousePosition.x = eventData.position.x;
            BeginMousePosition.y = eventData.position.y;
        }*/
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {/*
        Time.timeScale = 1;
        BeginMousePosition = new Vector2(0, 0);*/
    }
}
