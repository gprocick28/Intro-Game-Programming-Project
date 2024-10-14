using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager master;
    public Transform startPoint;
    public Transform[] enemyPath; // type 'Transform' holds position, rotation and scale of an object - used here to store enemy path node locations

    private void Awake()
    {
        master = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
