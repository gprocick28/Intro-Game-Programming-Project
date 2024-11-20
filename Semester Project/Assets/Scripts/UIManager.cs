using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for state management regarding the Upgrade Menu
public class UIManager : MonoBehaviour
{
    public static UIManager master;

    private bool isHovering;

    void Awake()
    {
        master = this;
    }

    public void SetHovering(bool state)
    {
        isHovering = state;
    }

    public bool IsHovering()
    {
        return isHovering;
    }
}
