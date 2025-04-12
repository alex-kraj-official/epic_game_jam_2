using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TowerPlacer : MonoBehaviour
{
    public GameObject towerPrefab;
    public LayerMask placementLayer; // Layer for valid placement surfaces
    private GameObject pendingTower;
    private bool isPlacing = false;

    //variables for panel
    public GameObject panel;
    public TextMeshProUGUI namee;
    public TextMeshProUGUI attackSpeed;
    public TextMeshProUGUI damage;
    public Button B1;
    public Button B2;

    ProduceBuilding e;
    Transform currentTower;


    void Update()
    {
        // Start placement mode on '1' key press
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isPlacing && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("a");
            StartPlacing();
        }
        if (Input.GetMouseButtonDown(0) && !isPlacing && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("b");
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
    void TowerClicker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            e = hit.transform.root.GetComponent<ProduceBuilding>();
            currentTower = hit.transform;
            Debug.Log(hit.transform.tag);
            if (e != null)
            {
                panel.SetActive(true);
                namee.SetText(hit.transform.root.name);
            }
            else
            {
                panel.SetActive(false);
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
   // public void upgradeSpd()
   // {
   //     if (e != null)
   //     {
   //         e.attackRate = Mathf.Round((e.attackRate + 0.1f)*10)*0.1f;
   //         attackSpeed.SetText("speed: " + e.attackRate);
   //     }
   // }
   // public void upgradeDmg()
   // {
   //     if (e != null)
   //     {
   //         e.bulletDamage = e.bulletDamage + 1;
   //         damage.SetText("dmg: " + e.bulletDamage);
   //     }
   // }
}