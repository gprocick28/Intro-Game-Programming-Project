using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// this script will be changed massively before the game is done - as of now spawning is procedurally based on the wave number, number of enemies to start,
// and difficulty multiplier - I plan to have hard coded and well tested enemy types and amounts for each wave similar to the way waves are done in Bloons TD

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public int startingEnemyNum = 8; // number of enemies to spawn in first wave, effects amounts spawned in later waves
    public float enemiesPerSecond = 1f;
    public float difficultyMultiplier = 1f; // determines number of enemies (used in enemyCalculation)
    public int livingEnemies; // amount of living enemies
    public bool isSpawning = false;
    public int wave = 1; // current wave
    public int enemiesToSpawn; // amount of enemies left to spawn
    private float breakLength = 5f; // amount of time between waves in seconds
    private float timeSince; // amount of time since last enemy in seconds
    public static UnityEvent onEnemyDeath = new UnityEvent(); // intialize to a new UnityEvent()

    // Awake is called when a script instance is loaded (so, very early)
    private void Awake()
    {
        onEnemyDeath.AddListener(EnemyDeath);  // every time onEnemyDeath is called we call EnemyDeath
    }

    // Start is called before the first frame update
    void Start()
    {
        // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
        StartCoroutine(StartWave()); // starts first wave
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
        timeSince += Time.deltaTime; // adds 'deltaTime' each frame; should go up roughly by one each second (I think, the documentation is kinda confusing but it works)

        if (timeSince >= (1f / enemiesPerSecond) && enemiesToSpawn > 0) // spawns enemies (EX: if enemiesPerSecond = 0.5, the closure will trigger twice per second)
        {
            SpawnEnemy();
            enemiesToSpawn--;
            livingEnemies++;
            timeSince = 0f; // prevents if from continuously returning true
        }

        if (livingEnemies == 0 && enemiesToSpawn == 0)
        {
            EndWave();
        }
    }

    private void EnemyDeath()
    {
        livingEnemies--;
    }

    // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(breakLength);
        isSpawning = true;
        enemiesToSpawn = enemyCalculation();
    }

    void EndWave()
    {
        isSpawning = false;
        timeSince = 0f;
        wave++;
        StartCoroutine(StartWave()); // may end up implementing a button to start waves rather than be on a timing system
    }

    // calculates how many enemies to spawn per wave - this is for early testing purposes, I plan to design the rounds like in BTD
    private int enemyCalculation()
    {
        return Mathf.RoundToInt(startingEnemyNum * Mathf.Pow(wave, difficultyMultiplier)); // derives number of enemies from startingEnemyNum and
                                                                                           // wave^difficultyMultiplier - rounds to int
    }

    void SpawnEnemy()
    {
        GameObject typeToSpawn = enemyTypes[0]; // this will be random once more enemies are implemented (it will be random until everything is in and tested, I plan to have hard coded waves like bloons TD)
        // shoutout to MonkeyKidGC on YouTube for showing me how to use the instantiate function for prefabs - https://www.youtube.com/watch?v=zMjhGpJnjP0
        Instantiate(typeToSpawn, GameManager.master.startPoint.position, Quaternion.identity); // Quaternion.identity spawns with current rotation
    }
}
