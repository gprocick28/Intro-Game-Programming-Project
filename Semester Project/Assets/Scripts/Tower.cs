using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for spawning towers in other scripts
// https://discussions.unity.com/t/custom-class-wont-show-up-in-inspector-serialization-question/223360
[System.Serializable]
public class Tower
{
    public string towerName;
    public int cost;
    public GameObject prefab;

    public Tower(string towerName_, int cost_, GameObject prefab_)
    {
        towerName = towerName_;
        cost = cost_;
        prefab = prefab_;
    }
}
