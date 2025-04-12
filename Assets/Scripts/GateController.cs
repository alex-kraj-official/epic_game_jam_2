using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public float health;

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void takeDamage(float amount)
    {
        health = health - amount;
    }
}
