using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// https://discussions.unity.com/t/difference-between-onpointerenter-and-onmouseenter/133345/4
// https://docs.unity3d.com/2017.4/Documentation/ScriptReference/EventSystems.IPointerEnterHandler.html
public class UpgradeManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool mouseOver = false;

    // OnPointerEnter is like OnMouseEnter but for UI
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        UIManager.master.SetHovering(true);
    }

    // OnPointerExit is like OnMouseExit but for UI
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        UIManager.master.SetHovering(false);
        gameObject.SetActive(false);
    }

}
