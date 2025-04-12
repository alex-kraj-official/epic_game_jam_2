using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class ProduceBuilding : MonoBehaviour
{
    public float level;
    public float productionAmount;
    public float productionTime;
    public float upgradeCost;
    public ResourceManager resourceManager;

    private void Start()
    {
        StartCoroutine(ProduceLoop());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            upgrade();
        }
    }
    IEnumerator ProduceLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(productionTime);

            if (resourceManager != null)
            {
                switch (gameObject.tag)
                {
                    case "sheep":
                        resourceManager.getSheep(productionAmount);
                        break;
                    case "wood":
                        resourceManager.getWood(productionAmount);
                        break;
                    case "people":
                        resourceManager.getPeople(productionAmount);
                        break;
                    case "ore":
                        resourceManager.getOre(productionAmount);
                        break;
                    case "wheat":
                        resourceManager.getWheat(productionAmount);
                        break;
                    default:
                        Debug.LogWarning("Unknown building tag: " + gameObject.tag);
                        break;
                }
            }
        }
    }

    //kell egy IEnumerator consumptionLoop

    public void upgrade()
    {
        if (resourceManager.money >= upgradeCost)
        {
            level++;
            productionTime--;
            productionAmount++;
            upgradeCost = upgradeCost + 50;
        }
        else
        {
            Debug.Log("not enough money");
            return;
        }
    }
}
