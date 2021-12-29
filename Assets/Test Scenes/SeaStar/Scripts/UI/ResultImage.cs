using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResultImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image toolTip;
    public Ability ab;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ab.AbType != Ability.ABTYPE.APDrink || ab.AbType != Ability.ABTYPE.HPDrink)
        {
            toolTip.sprite = ab.toolTip;
            toolTip.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ab.AbType != Ability.ABTYPE.APDrink || ab.AbType != Ability.ABTYPE.HPDrink)
            toolTip.enabled = false;
    }
}
