using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10f;
    public float speed = 5f;
    public float damage = 5f;

    public Transform[] target;
    public int currentTargetIndex = 0;

    public bool isAttacking = false;

    private GateController gateController;

    private void Start()
    {
        gateController = FindObjectOfType<GateController>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (currentTargetIndex >= target.Length)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackLoop());
            }
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target[currentTargetIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target[currentTargetIndex].position) < 0.01f)
        {
            currentTargetIndex++;
        }
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            attack();
            yield return new WaitForSeconds(speed);
            
        }
    }
    void attack()
    {
        Debug.Log("attacked gate for: " + damage +  "damage");
        gateController.takeDamage(damage);
    }
}
