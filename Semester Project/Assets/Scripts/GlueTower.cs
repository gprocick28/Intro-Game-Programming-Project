using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class GlueTower : MonoBehaviour
{
    [SerializeField] private LayerMask maskEnemy;
    [SerializeField] private float towerRange = 2f;

    public float triggerRate = 1f; // amount of times tower triggers per second
    public float glueTime = 2f; // duration of glue effect
    private float triggerDelay; // time between triggering tower

    private float baseTriggerRate;
    private float baseGlueTime;
    private int baseUpgradeCost = 100;

    public GameObject upgradeMenu;
    public Button upgradeButton;

    private int towerLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        baseTriggerRate = triggerRate;
        baseGlueTime = glueTime;

        // https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button-onClick.html
        upgradeButton.onClick.AddListener(UpgradeTower);
    }

    // Update is called once per frame
    void Update()
    {
        triggerDelay += Time.deltaTime; // adds time in seconds since last frame to triggerDelay

        // triggerDelay is the time since the last frame in seconds, once it is greater than or equal to 1 divided by the rate of fire, it fires
        // in this case it is  1 / 1 = 1, so it fires once every second
        if (triggerDelay >= 1f / triggerRate)
        {
            FireGlue();
            triggerDelay = 0f; // reset trigger delay so that if does not always evaluate to true
        }
    }

    private void FireGlue()
    {
        // done using array of hits so that multiple enemies can be affected
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, towerRange, transform.position, 0f, maskEnemy); // CircleCastAll(origin, range, direction, distance, layermask) - returns hits in radius, should only hit things in maskEnemy mask

        // if there was at least one hit
        if (hits.Length > 0)
        {
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements
            foreach (RaycastHit2D hit in hits)
            {
                EnemyMovement movement = hit.transform.GetComponent<EnemyMovement>();
                movement.ChangeSpeed(2.5f);

                // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
                StartCoroutine(ResetEnemySpeed(movement));
            }
        }
    }

    // https://stackoverflow.com/questions/30056471/how-to-make-the-script-wait-sleep-in-a-simple-way-in-unity
    private IEnumerator ResetEnemySpeed(EnemyMovement movement)
    {
        yield return new WaitForSeconds(glueTime);
        movement.ResetSpeed();
    }

    // https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnDrawGizmosSelected.html
    // used to draw 'gizmos' if object is selected - shows tower range in editor only
    private void OnDrawGizmosSelected()
    {
        // https://docs.unity3d.com/ScriptReference/Handles.DrawWireDisc.html
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, towerRange); // Handles.DrawWireDisc(center, normal, radius)
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

        glueTime = GlueTimeCalculation();

        triggerRate = TriggerRateCalculation();

        CloseUpgradeMenu();

        Debug.Log("New triggerRate: " + triggerRate);
        Debug.Log("New glue time: " + glueTime);
        Debug.Log("New cost: " + CostCalculation());
    }

    // calculates cost to upgrade tower
    private int CostCalculation()
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(towerLevel, 0.8f));
    }

    // calculates trigger rate to upgrade to
    private float TriggerRateCalculation()
    {
        return baseTriggerRate * Mathf.Pow(towerLevel, 0.5f);
    }

    // calculates glue time to upgrade to
    private float GlueTimeCalculation()
    {
        return baseGlueTime * Mathf.Pow(towerLevel, 0.4f);
    }
}
