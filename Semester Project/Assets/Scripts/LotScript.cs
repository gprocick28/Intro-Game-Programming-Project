using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseEnter.html - unity docs provided good basis for this script
public class LotScript : MonoBehaviour
{
    public SpriteRenderer plotRenderer;

    private Color highlightedColor;
    private Color originalColor;
    private GameObject towerObject;

    public BasicTower basicTower;
    public GlueTower glueTower;

    private void Start()
    {
        originalColor = plotRenderer.material.color;
        highlightedColor = Color.gray;
    }

    // highlights current lot
    private void OnMouseEnter()
    {
        plotRenderer.material.color = highlightedColor;
    }

    // unhighlights current lot
    private void OnMouseExit()
    {
        plotRenderer.material.color = originalColor;
    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseDown.html
    private void OnMouseDown()
    {
        if (UIManager.master.IsHovering()) return;

        if (towerObject == null) // if tower == null, then there is no tower currently on the lot
        {
            Tower towerToBuild = BuildTower.manager.GetTower(); // get tower that player wants to build

            if (towerToBuild.cost > GameManager.master.coins) // if the cost is more than the player has, do not place tower
            {
                Debug.Log("he cannot afford. great success. (in borat voice)");
                return;
            }

            GameManager.master.SpendCoins(towerToBuild.cost); // subtract coins from player
            towerObject = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity); // place tower

            // https://discussions.unity.com/t/c-how-to-check-if-a-gameobject-contains-a-certain-component/70710
            if (towerObject.GetComponent<BasicTower>() != null)
            {
                basicTower = towerObject.GetComponent<BasicTower>();
            } else if (towerObject.GetComponent<GlueTower>() != null)
            {
                glueTower = towerObject.GetComponent<GlueTower>();
            }
        }
        else if (towerObject != null)
        {
            if (towerObject.GetComponent<BasicTower>() != null)
            {
                basicTower.OpenUpgradeMenu();
            }
            else if (towerObject.GetComponent<GlueTower>() != null)
            {
                glueTower.OpenUpgradeMenu();
            }
                return; // do nothing (besides open upgrade menu) if lot has a tower
        }
    }
}
