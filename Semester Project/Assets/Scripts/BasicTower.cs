using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class BasicTower : MonoBehaviour 
{
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private LayerMask maskEnemy; // mask so that tower only hits enemies and not other objects
    private Transform target;

    [SerializeField] private float towerRange = 3f; // tower attack range
    private float rotationSpeed = 200f;

    public GameObject bulletPrefab;
    public float fireRate = 1f;   // bullets per second
    public float fireDelay; // time until bullet is fired
    public Transform firePoint;

    private float baseFireRate;
    private float baseTowerRange;
    private int baseUpgradeCost = 100;

    public GameObject upgradeMenu;
    public Button upgradeButton;

    private int towerLevel = 1;


    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmosSelected.html
    // used to draw 'gizmos' if object is selected - shows tower range in editor only
    private void OnDrawGizmosSelected()
    {
        // https://docs.unity3d.com/ScriptReference/Handles.DrawWireDisc.html
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, towerRange); // Handles.DrawWireDisc(center, normal, radius)
    }

    void Start()
    {
        baseFireRate = fireRate;
        baseTowerRange = towerRange;

        // https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
        upgradeButton.onClick.AddListener(UpgradeTower);
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
        } else
        {
            fireDelay += Time.deltaTime; // adds time in seconds since last frame to fireDelay

            // fireDelay is the time since the last frame in seconds, once it is greater than or equal to 1 divided by the rate of fire, it fires
            // in this case it is  1 / 1 = 1, so it fires once every second
            if (fireDelay >= 1f / fireRate)
            {
                Fire();
                fireDelay = 0f; // reset fire delay so that if does not always evaluate to true
            }
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletController controller = bullet.GetComponent<BulletController>();
        controller.SetTarget(target);
    }

    private bool TargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= towerRange; // gets distance between target pos, and tower pos, if false then target is out of range
    }

    void LocateTarget()
    {
        // done using array of hits so that certain towers can target the last or first enemy within range like in BTD (and so code can be reused for glue tower)
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerRange, transform.position, 0f, maskEnemy); // CircleCastAll(origin, range, direction, distance, layermask) - returns hits in radius, should only hit things in maskEnemy mask

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

    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        UIManager.master.SetHovering(false);
    }

    // upgrades tower
    public void UpgradeTower()
    {
        if (CostCalculation() > GameManager.master.coins) return;

        GameManager.master.SpendCoins(CostCalculation());

        towerLevel++;

        fireRate = FireRateCalculation();

        towerRange = RangeCalculation();

        CloseUpgradeMenu();

        Debug.Log("New fireRate: " + fireRate);
        Debug.Log("New range: " + towerRange);
        Debug.Log("New cost: " + CostCalculation());
    }

    // calculates fire rate to upgrade to
    private float FireRateCalculation()
    {
        return baseFireRate * Mathf.Pow(towerLevel, 0.5f);
    }

    // calculates range to upgrade to
    private float RangeCalculation()
    {
        return baseTowerRange * Mathf.Pow(towerLevel, 0.4f);
    }

    // calculates cost to upgrade tower
    private int CostCalculation()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(towerLevel, 0.8f));
    }
}
