using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 10f;
    public float speed = 5f;
    public float damage = 5f;

    public Transform[] target;
    public int currentTargetIndex = 0;


    private void Update()
    {
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if (currentTargetIndex >= target.Length)
        {
            //Debug.Log("reached final target");
            return; // All targets reached
        }

        transform.position = Vector3.MoveTowards(transform.position, target[currentTargetIndex].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target[currentTargetIndex].position) < 0.01f)
        {
            currentTargetIndex++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Base"))
        {
            BaseController a = other.GetComponent<BaseController>();
            a.TakeDamage(damage);
            Debug.Log("REACHED BASE LOLOLOL");
        }
    }
}
