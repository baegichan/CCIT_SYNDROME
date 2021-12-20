using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum AbilityType { ACTIVE, PASSIVE }
    public AbilityType AT;
    public Image toolTip;

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("¿Ö¾ÈµÅ!!!");
        if (Char_Parent.ply.ActiveAbility.AbCode != 99 && AT == AbilityType.ACTIVE)
        {
            toolTip.enabled = false;
        }
        else if ((Char_Parent.ply.PassiveAbility.AbCode != 99 && AT == AbilityType.PASSIVE))
        {
            toolTip.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("¿Ö¾ÈµÅ@@@");
        if (Char_Parent.ply.ActiveAbility.AbCode != 99 && AT == AbilityType.ACTIVE)
        {
            toolTip.sprite = Char_Parent.ply.ActiveAbility.toolTip;
            toolTip.enabled = true;
        }
        else if ((Char_Parent.ply.PassiveAbility.AbCode != 99 && AT == AbilityType.PASSIVE))
        {
            toolTip.sprite = Char_Parent.ply.PassiveAbility.toolTip;
            toolTip.enabled = true;
        }
    }
}
