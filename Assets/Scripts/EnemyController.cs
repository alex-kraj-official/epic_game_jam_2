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

    public GameObject intact;
    public GameObject broke;

    Animator animator;
    private GameObject gate;

    private void Start()
    {
        gateController = FindObjectOfType<GateController>();
        gate = GameObject.FindGameObjectWithTag("Gate");
        animator = intact.GetComponent<Animator>();
        animator.Play("Giant@Run 01 - Forward"); // Name of your clip exactly
    }

    private void Update()
    {
        if (health <=0 )
        {
            intact.SetActive(false);
            broke.SetActive(true);
            Destroy(this.gameObject,1.5f);
        }
        else
        {
            MoveToTarget();
        }
    }

    void StartPunching()
    {
        animator.Play("punch");
    }

    void MoveToTarget()
    {
        if (currentTargetIndex >= target.Length)
        {
            // Rotate toward the gate
            Vector3 directionToGate = (gate.transform.position - transform.position).normalized;

            if (directionToGate != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(directionToGate);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }

            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackLoop());
                StartPunching();
            }
            return;
        }

        // Normal movement logic
        Vector3 direction = (target[currentTargetIndex].position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
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
        //Debug.Log("attacked gate for: " + damage +  "damage");
        gateController.takeDamage(damage);
    }
    public void takeDamage(float damage)
    {
        health = health - damage;
        Debug.Log("ouch");
        return;
    }
}
