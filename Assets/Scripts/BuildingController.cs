using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class BuildingController : MonoBehaviour
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
}
