using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private Transform enemy;
    public Transform top;

    public float attackRange = 5f;
    public LayerMask enemyLayer;

    public GameObject bullet;
    public Transform shootPoint;
    public float attackRate = 0.5f;
    private float nextTimeToFire = 0f;
    public float upgradeCost;
    public float level;

    public float bulletDamage = 1f;

    void Start()
    {
        top = this.gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        getTarget();
        aim();
    }
    void getTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // If we already have an enemy, check if it's still in range
        if (enemy != null)
        {
            float dist = Vector3.Distance(transform.position, enemy.position);
            if (dist > attackRange)
            {
                enemy = null;
            }
        }

        // If no valid enemy, try to find a new one
        if (enemy == null && hits.Length > 0)
        {
            enemy = hits[0].transform;
        }

    }
    void aim()
    {
        if (enemy != null)
        {
            Vector3 direction = enemy.position - top.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            top.rotation = rotation;
            if (Time.time >= nextTimeToFire)
            {
                shoot();
            }   
        }
    }
    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        ProjectileController e = newBullet.GetComponent<ProjectileController>();
        e.target = enemy;
        e.damage = bulletDamage;
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            //bulletRb.AddForce(shootPoint.forward*20f, ForceMode.Impulse);
        }
        nextTimeToFire = Time.time + 1f / attackRate;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
