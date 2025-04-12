using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public float lvl;
    public float upgradeCost;
    public float repairCost;

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float amount)
    {
        currentHealth = currentHealth + armor - amount;
    }
    public void repair()
    {
        currentHealth = currentHealth + 5 * lvl;
    }
    public void upgrade()
    {
        maxHealth = maxHealth + 50;
        currentHealth = maxHealth;
        armor = armor + 3;
        lvl++;
        upgradeCost = upgradeCost + 50;
    }

}
