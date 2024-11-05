using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseEnter.html - unity docs provided good basis for this script
public class LotScript : MonoBehaviour
{
    public SpriteRenderer plotRenderer;

    private Color highlightedColor;
    private Color originalColor;
    private GameObject tower;

    private void Start()
    {
        originalColor = plotRenderer.material.color;
        highlightedColor = Color.gray;
    }

    private void OnMouseEnter()
    {
        plotRenderer.material.color = highlightedColor;
    }

    private void OnMouseExit()
    {
        plotRenderer.material.color = originalColor;
    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
    private void OnMouseDown()
    {
        if (tower != null) return;

        GameObject towerToBuild = BuildTower.manager.GetTower();
        tower = Instantiate(towerToBuild, transform.position, Quaternion.identity);
    }
}
