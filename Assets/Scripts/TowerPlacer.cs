using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public LayerMask placementLayer; // Layer for valid placement surfaces
    private GameObject pendingTower;
    private bool isPlacing = false;

    //variables for panel
    [Header("PRODUCE AND TOWER PANELS")]
    public GameObject produceUpgradePanel;
    public GameObject towerUpgradePanel;
    public GameObject gateUpgradePanel;

    //tower panel variables
    [Header("TOWER CURRENT LEVEL VARIABLES")]
    public TextMeshProUGUI towerName;
    public TextMeshProUGUI towerSpeed;
    public TextMeshProUGUI towerDamage;
    public TextMeshProUGUI towerLevel;
    public TextMeshProUGUI towerCost;

    [Header("TOWER NEXT LEVEL VARIABLES")]
    public TextMeshProUGUI towerSpeedN;
    public TextMeshProUGUI towerDamageN;
    public TextMeshProUGUI towerLevelN;

    //produce building panel variables
    [Header("PRODUCE CURRENT LEVEL VARIABLES")]
    public TextMeshProUGUI produceName;
    public TextMeshProUGUI produceSpeed;
    public TextMeshProUGUI produceAmount;
    public TextMeshProUGUI produceLevel;
    public TextMeshProUGUI produceCost;

    [Header("PRODUCE NEXT LEVEL VARIABLES")]
    public TextMeshProUGUI produceSpeedN;
    public TextMeshProUGUI produceAmountN;
    public TextMeshProUGUI produceLevelN;

    [Header("GATE CURRENT LEVEL VARIABLES")]
    public TextMeshProUGUI gateName;
    public TextMeshProUGUI gateLevel;
    public TextMeshProUGUI gateHealth;
    public TextMeshProUGUI gateArmor;
    public TextMeshProUGUI gateCost;
    public TextMeshProUGUI repairCost;

    [Header("GATE NEXT LEVEL VARIABLES")]
    public TextMeshProUGUI gateLevelN;
    public TextMeshProUGUI gateHealthN;
    public TextMeshProUGUI gateArmorN;

    BuildingController buildingController;
    TowerController towerController;
    GateController gateController;

    Transform currentGate;
    Transform currentTower;
    Transform currentProduce;
    public ResourceManager resourceManager;



    void Update()
    {
        // Start placement mode on '1' key press
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isPlacing && !EventSystem.current.IsPointerOverGameObject())
        {
            StartPlacing();
        }
        if (Input.GetMouseButtonDown(0) && !isPlacing && !EventSystem.current.IsPointerOverGameObject())
        {
            ProduceClicker();
            TowerClicker();
            GateClicker();
        }

        // While in placement mode
        if (isPlacing)
        {
            FollowMousePosition();
            CheckValidPosition();

            // Place on left click
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                PlaceTower();
            }

            // Cancel on right click
            if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                CancelPlacing();
            }
        }
    }
    void CheckValidPosition()
    {
        Collider[] colliders = Physics.OverlapBox(
            pendingTower.transform.position,
            pendingTower.transform.localScale / 2,
            Quaternion.identity
        );

        if (colliders.Length <= 2) // Only colliding with itself or ground
        {
            //pendingTower.GetComponent<Tower>().enabled = true;
            Debug.Log("good position");
        }
        else
        {
            // Optional: Show invalid placement feedback
            Debug.Log("Can't place here!");
        }
    }
    void StartPlacing()
    {
        isPlacing = true;
        pendingTower = Instantiate(towerPrefab);
        // Disable tower functionality while placing
        //pendingTower.GetComponent<Tower>().enabled = false;
    }
    void ProduceClicker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            buildingController = hit.transform.GetComponent<BuildingController>();
            currentProduce = hit.transform;
            Debug.Log(hit.transform.tag);
            if (buildingController != null)
            {
                Debug.Log("asdasd");
                towerUpgradePanel.SetActive(false);
                produceUpgradePanel.SetActive(true);
                gateUpgradePanel.SetActive(false);

                produceName.SetText(currentProduce.name);
                produceSpeed.SetText(buildingController.productionTime.ToString());
                produceAmount.SetText(buildingController.productionAmount.ToString());
                produceLevel.SetText("Level " + buildingController.level.ToString());
                produceCost.SetText(buildingController.upgradeCost.ToString());

                produceSpeedN.SetText((buildingController.productionTime + 1).ToString());
                produceAmountN.SetText((buildingController.productionAmount + 1).ToString());
                produceLevelN.SetText((buildingController.level + 1).ToString());

               // namee.SetText(hit.transform.root.name);
               // attackSpeed.SetText(e.productionTime.ToString());
               // Debug.Log("a");
               // damage.SetText(e.productionAmount.ToString());
               // Lvl.SetText(e.level.ToString());
               // nextLvl.SetText((e.level+1).ToString());
               // attackSpeedNextLvl.SetText((e.productionTime-1).ToString());
               // damageNextLvl.SetText((e.productionAmount+1).ToString());
            }
            else
            {
                Debug.Log("no buildingcontroller");
                produceUpgradePanel.SetActive(false);
            }
        }
    }
    void TowerClicker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            towerController = hit.transform.root.GetComponent<TowerController>();
            currentTower = hit.transform;
            Debug.Log(hit.transform.tag);
            if (towerController != null)
            {
                produceUpgradePanel.SetActive(false);
                towerUpgradePanel.SetActive(true);
                gateUpgradePanel.SetActive(false);

                towerName.SetText("Tower");
                towerSpeed.SetText(towerController.attackRate.ToString());
                towerDamage.SetText(towerController.bulletDamage.ToString());
                towerLevel.SetText("Level " + towerController.level.ToString());
                towerCost.SetText(towerController.upgradeCost.ToString());

                towerSpeedN.SetText((towerController.attackRate + 1).ToString());
                towerDamageN.SetText((towerController.bulletDamage + 1).ToString());
                towerLevelN.SetText((towerController.level + 1).ToString());

            }
            else
            {
                towerUpgradePanel.SetActive(false);
            }
        }
    }

    void GateClicker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            gateController = hit.transform.root.GetComponent<GateController>();
            currentGate = hit.transform;
            Debug.Log(hit.transform.tag);
            if (gateController != null)
            {
                produceUpgradePanel.SetActive(false);
                towerUpgradePanel.SetActive(false);
                gateUpgradePanel.SetActive(true);

                gateName.SetText(currentGate.name);
                gateLevel.SetText("Level " + gateController.lvl.ToString());
                gateHealth.SetText(gateController.maxHealth.ToString());
                gateArmor.SetText(gateController.armor.ToString());
                gateCost.SetText(gateController.upgradeCost.ToString());

                gateLevelN.SetText((gateController.lvl + 1).ToString());
                gateHealthN.SetText((gateController.maxHealth + 1).ToString());
                gateArmorN.SetText((gateController.armor + 1).ToString());
            }
            else
            {
                gateUpgradePanel.SetActive(false);
            }
        }
    }


    void FollowMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, placementLayer))
        {
            pendingTower.transform.position = hit.point;
        }
    }

    void PlaceTower()
    {
        // Check if placement is valid (not colliding with other towers)
        Collider[] colliders = Physics.OverlapBox(
            pendingTower.transform.position,
            pendingTower.transform.localScale / 2,
            Quaternion.identity
        );

        if (colliders.Length <= 2) // Only colliding with itself or ground
        {
            //pendingTower.GetComponent<Tower>().enabled = true;
            isPlacing = false;
            pendingTower = null;
        }
        else
        {
            // Optional: Show invalid placement feedback
            Debug.Log("Can't place here!");
        }
    }

    void CancelPlacing()
    {
        Destroy(pendingTower);
        isPlacing = false;
    }
    public void upgradeTower()
    {
        if (resourceManager.money >= towerController.upgradeCost)
        {
            towerController.attackRate++;
            towerController.bulletDamage++;
            towerController.level++;
            towerController.upgradeCost = towerController.upgradeCost + 50;

            towerName.SetText(currentTower.name);
            towerSpeed.SetText(towerController.attackRate.ToString());
            towerDamage.SetText(towerController.bulletDamage.ToString());
            towerLevel.SetText("Level " + towerController.level.ToString());
            towerCost.SetText(towerController.upgradeCost.ToString());

            towerSpeedN.SetText((towerController.attackRate + 1).ToString());
            towerDamageN.SetText((towerController.bulletDamage + 1).ToString());
            towerLevelN.SetText((towerController.level + 1).ToString());

            resourceManager.removeGold(towerController.upgradeCost);
        }
        else
        {
            Debug.Log("not enough money");
            return;
        }
    }
    public void upgradeProduce()
    {
        if (resourceManager.money >= buildingController.upgradeCost)
        {
            buildingController.productionTime++;
            buildingController.productionAmount++;
            buildingController.level++;
            buildingController.upgradeCost = buildingController.upgradeCost + 50;

            produceName.SetText(currentProduce.name);
            produceSpeed.SetText(buildingController.productionTime.ToString());
            produceAmount.SetText(buildingController.productionAmount.ToString());
            produceLevel.SetText("Level " + buildingController.level.ToString());
            produceCost.SetText(buildingController.upgradeCost.ToString());

            produceSpeedN.SetText((buildingController.productionTime + 1).ToString());
            produceAmountN.SetText((buildingController.productionAmount + 1).ToString());
            produceLevelN.SetText((buildingController.level + 1).ToString());

            resourceManager.removeGold(buildingController.upgradeCost);
        }
        else
        {
            Debug.Log("not enough money");
            return;
        }
    }
    public void upgradeGate()
    {
        if (resourceManager.money >= gateController.upgradeCost)
        {
            gateController.upgrade();

            gateName.SetText(currentGate.name);
            gateLevel.SetText("Level " + gateController.lvl.ToString());
            gateHealth.SetText(gateController.maxHealth.ToString());
            gateArmor.SetText(gateController.armor.ToString());
            gateCost.SetText(gateController.upgradeCost.ToString());

            gateLevelN.SetText((gateController.lvl + 1).ToString());
            gateHealthN.SetText((gateController.maxHealth + 1).ToString());
            gateArmorN.SetText((gateController.armor + 1).ToString());
            resourceManager.removeGold(gateController.upgradeCost);
        }
        else
        {
            return;
        }

    }
    public void repairGate()
    {
        if (resourceManager.money >= gateController.repairCost)
        {
            gateController.repair();
            gateHealth.SetText(gateController.maxHealth.ToString());
            resourceManager.removeGold(gateController.repairCost);
        }
        else
        {
            return;
        }
    }
}