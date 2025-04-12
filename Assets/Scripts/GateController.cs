using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float health;
    public float armor;
    public float lvl;
    public float upgradeCost;
    public float repairCost;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float amount)
    {
        health = health + armor - amount;
    }
    public void repair()
    {
        health = health + 5 * lvl;
    }
    public void upgrade()
    {
        health = health + 50;
        armor = armor + 3;
        lvl++;
        upgradeCost = upgradeCost + 50;
    }

}
