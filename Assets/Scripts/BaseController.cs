using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        //asdasdasd
    }
    //lolo
    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage(float damage)
    {
        health = health-damage;
    }
}
