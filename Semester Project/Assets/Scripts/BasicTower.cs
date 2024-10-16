using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BasicTower : MonoBehaviour 
{
    public Transform rotationPoint;
    private Transform target;

    public LayerMask maskEnemy;
    public float towerRange = 3f; // tower attack range
    private float rotationSpeed = 200f;

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmosSelected.html
    // used to draw 'gizmos' if object is selected
    private void OnDrawGizmosSelected()
    {
        // https://docs.unity3d.com/ScriptReference/Handles.DrawWireDisc.html
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, towerRange); // Handles.DrawWireDisc(center, normal, radius)
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) // if target is null, find target
        {
            LocateTarget();
            return;
        }

        FaceTarget(); // makes tower face target

        // if target is out of range set target to null so we can find new target
        if (!TargetInRange())
        {
            target = null;
        }
    }

    private bool TargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerRange; // gets distance between target pos, and tower pos, if false then target is out of range
    }

    void LocateTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerRange, (Vector2) transform.position, 0f, maskEnemy); // CircleCastAll(origin, range, direction, distance, layermask)

        // if there was a hit
        if (hits.Length > 0)
        {
            // first hit is target
            target = hits[0].transform;
        }
    }

    private void FaceTarget()
    {
        // https://discussions.unity.com/t/rotating-a-2d-sprite-to-face-a-target-on-a-single-axis/97449/2
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        // https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
        Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, rotation, rotationSpeed * Time.deltaTime); // smoothes rotation - https://discussions.unity.com/t/solved-quaternion-smooth-rotation/580350
    }
}
