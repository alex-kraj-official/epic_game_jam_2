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
    public GameObject produceUpgradePanel;
    public GameObject towerUpgradePanel;

    public TextMeshProUGUI namee;
    public TextMeshProUGUI Lvl;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI damage;

    public TextMeshProUGUI nextLvl;
    public TextMeshProUGUI attackSpeedNextLvl;
    public TextMeshProUGUI damageNextLvl;


    public Button upgradeBtn;

    ProduceBuilding e;
    TowerController f;
    Transform currentTower;
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
            e = hit.transform.root.GetComponent<ProduceBuilding>();
            currentTower = hit.transform;
            Debug.Log(hit.transform.tag);
            if (e != null)
            {
                produceUpgradePanel.SetActive(true);
                namee.SetText(hit.transform.root.name);
                attackSpeed.SetText(e.productionTime.ToString());
                Debug.Log("a");
                damage.SetText(e.productionAmount.ToString());
                Lvl.SetText(e.level.ToString());
                nextLvl.SetText((e.level+1).ToString());
                attackSpeedNextLvl.SetText((e.productionTime-1).ToString());
                damageNextLvl.SetText((e.productionAmount+1).ToString());
            }
            else
            {
                produceUpgradePanel.SetActive(false);
                towerUpgradePanel.SetActive(false);
            }
        }
    }
    void TowerClicker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            f = hit.transform.root.GetComponent<TowerController>();
            currentTower = hit.transform;
            Debug.Log(hit.transform.tag);
            if (f != null)
            {
                towerUpgradePanel.SetActive(true);
                namee.SetText(hit.transform.root.name);
                attackSpeed.SetText(f.attackRate.ToString());
                damage.SetText(f.bulletDamage.ToString());
                Lvl.SetText(f.level.ToString());
                nextLvl.SetText((f.level + 1).ToString());
                attackSpeedNextLvl.SetText((f.attackRate + 1).ToString());
                damageNextLvl.SetText((f.bulletDamage + 1).ToString());
            }
            else
            {
                towerUpgradePanel.SetActive(false);
                produceUpgradePanel.SetActive(false);
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
    public void upgrade()
    {
        if (resourceManager.money >= f.upgradeCost)
        {
            f.attackRate++;
            f.bulletDamage++;
            f.level++;
            f.upgradeCost = f.upgradeCost + 50;
        }
        else
        {
            Debug.Log("not enough money");
            return;
        }
    }
}