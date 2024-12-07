using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager master;
    public Transform startPoint;
    public Transform[] enemyPath; // type 'Transform' holds position, rotation and scale of an object - used here to store enemy path node locations
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;
    public GameObject gameOver;

    public int coins;
    public int health;

    private EnemySpawning waveTracker; // enemy spawning object to access wave number

    private void Awake()
    {
        master = this; // sets our static instance of GameManager to 'this' instance
    }

    // Start is called before the first frame update
    void Start()
    {
        coins  = 100;
        health = 100;
        waveTracker = GetComponent<EnemySpawning>();
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (health < 0) { health = 0; }
        healthText.text = health.ToString();
        waveText.text = waveTracker.wave.ToString();
        CheckHealthForDeath();
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

    public void SubtractHealth(int num)
    {
        health -= num;
    }

    public void CheckHealthForDeath()
    {
        if (health <= 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }
}
