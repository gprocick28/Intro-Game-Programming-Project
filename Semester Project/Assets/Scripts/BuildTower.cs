using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : MonoBehaviour
{
    public static BuildTower manager;

    public Tower[] towers; // array of tower objects to build - simple implementation

    private int selectedTower = 0; // index of currently selected tower

    private void Awake()
    {
        manager = this; // sets our static instance of BuildTower manager to 'this' instance
    }

    public Tower GetTower()
    {
        return towers[selectedTower];
    }

    public void SetTower(int tower_)
    {
        selectedTower = tower_;
    }
}
