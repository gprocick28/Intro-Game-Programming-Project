using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager master;
    public Transform startPoint;
    public Transform[] enemyPath; // type 'Transform' holds position, rotation and scale of an object - used here to store enemy path node locations

    public int coins;

    private void Awake()
    {
        master = this; // sets our static instance of GameManager to 'this' instance
    }

    // Start is called before the first frame update
    void Start()
    {
        coins = 100;
    }

    public void AddCoins(int num)
    {
        coins += num;
    }

    public void SpendCoins(int num)
    {
        if (num <= coins)
        {
            coins -= num;
        } else
        {
            Debug.Log("You don't have enough coins.");
        }
    }
}
