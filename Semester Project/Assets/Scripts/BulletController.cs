using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public Rigidbody2D rigidBody;

    public float bulletSpeed = 5f;
    public int bulletDamage = 1;
    private Transform target;

    // FixedUpdate for physics stuff
    void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized; // gets direction for bullet, normalized to stay between 0 and 1

        rigidBody.velocity = direction * bulletSpeed; // gets velocity for bullet
    }

    // allows us to set target from oher scripts
    public void SetTarget(Transform target_)
    {
        target = target_;
    }

    // when bullet hits enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<EnemyDamage>().DoDamage(bulletDamage); // do damage to enemy that is hit
        Destroy(gameObject); // destroy bullet
    }
}
