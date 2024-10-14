using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    public float moveSpeed = 5f;

    private Transform target; // current point that enemy should move toward
    private int location = 0; // current location of enemy on path


    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.master.enemyPath[location]; // set target of enemy to first point of path
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f) // if enemy is very close to current target
        {
            location++;  // update location (and in turn target) to "point" to next node in path

            if(location == GameManager.master.enemyPath.Length) // if enemy reaches end of path
            {
                EnemySpawning.onEnemyDeath.Invoke(); // calls onEnemyDeath (EnemyDeath()) immediately
                Destroy(gameObject); // destroy enemy
                return;
            } else
            {
                target = GameManager.master.enemyPath[location]; // updates target after location changed
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; // direction that enemy must move in to reach target (target postion - current position normalized)
        rigidBody.velocity = direction * moveSpeed; // moves rigidBody in correct direction by calculating velocity based on direction and moveSpeed
    }
}
