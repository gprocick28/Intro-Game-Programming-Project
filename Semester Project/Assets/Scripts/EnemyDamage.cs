using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public int health = 1;
    private bool isDead = false; // prevents onEnemyDeath from being invoked multiple times if towers hit an enemy at the same time
    private int enemyWorth = 50; // amount of coins that you get for defeating enemy

    public void DoDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            // https://docs.unity3d.com/ScriptReference/Events.UnityEvent.AddListener.html
            // https://docs.unity3d.com/ScriptReference/Events.UnityEvent.html
            // https://discussions.unity.com/t/unityevent-where-have-you-been-all-my-life/577808
            EnemySpawning.onEnemyDeath.Invoke();
            GameManager.master.AddCoins(enemyWorth);
            isDead = true;
            Destroy(gameObject);
        }
    }
}
