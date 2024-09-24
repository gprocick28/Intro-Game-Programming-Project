using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody; // '[SerializeField]' allows a private variable to show and be edited in inspector
    [SerializeField] private float moveSpeed = 5f;  // '[SerializeField]' allows a private variable to show and be edited in inspector
    private Transform target; // point that enemy will move to
    private int pathPosition = 0; // keeps track of position on path

    // Start is called before the first frame update
    void Start()
    {
        target = LevelManager.master.enemyPath[pathPosition]; // sets target to first element of enemyPath[] (startPoint)
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f) // if enemy gets very close to target node
        {
            pathPosition++; // increment position to indicate that enemy has arrived at target

            if (pathPosition == LevelManager.master.enemyPath.Length) // if enemy has reached last node in enemyPath[]
            {
                Destroy(gameObject); // destroy the enemy object
                return; // return to prevent further code execution in this case
            } else // if enemy has not reached last node
            {
                target = LevelManager.master.enemyPath[pathPosition]; // update the target to the next node in enemyPath[]
            }
        }
    }

    private void FixedUpdate() // related to physics engine, should manipulate rigidbody in FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; // normalized so the value stays between 0 and 1. gives direction enemy must move in to reach next node
        rigidBody.velocity = direction * moveSpeed; // moves rigidBody using velocity derived from direction and movement speed
    }
}
