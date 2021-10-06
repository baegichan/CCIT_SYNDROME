using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MiniMapDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Vector2 BeginMousePosition = new Vector3(0, 0);
    public Vector2 MovePoint = new Vector3(0,0 );
    [Range(0,500)]public float insensitive = 10;
    private void OnEnable()
    {
        this.transform.position = new Vector3(0, 0, 0);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {

        BeginMousePosition = eventData.position;
       
    }

    public void OnDrag(PointerEventData eventData)
    {

        if(BeginMousePosition != eventData.position)
        {
            transform.localPosition -= new Vector3((BeginMousePosition.x - eventData.position.x)/ insensitive, (BeginMousePosition.y - eventData.position.y)/ insensitive, 0);
        }
      
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BeginMousePosition = new Vector2(0, 0);
    }
}
