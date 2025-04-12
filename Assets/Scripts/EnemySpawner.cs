using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy1;
    //public Transform enemyWave;
    public float level;
    public Transform spawnPosition;
    public Transform[] pathPoints; // Assign green dots here
    public Transform[] pathPoints2;
    public Transform[] pathPoints3;
    public Transform[] pathPoints4;

    public List<Transform[]> pathList = new List<Transform[]>();

    private void Start()
    {
        pathList.Add(pathPoints);  // pathPoints is an array of transforms
        pathList.Add(pathPoints2);
        pathList.Add(pathPoints3);
        pathList.Add(pathPoints4);

        StartCoroutine(SpawnWave());
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spawnEnemy(enemy1, pathPoints);
        }
    }

    public void spawnEnemy(Transform enemy, Transform[] path)
    {
        Transform newEnemy = Instantiate(enemy1, transform.position, Quaternion.identity);
        EnemyController controller = newEnemy.GetComponent<EnemyController>();
        controller.target = path;
    }

    IEnumerator SpawnWave()
    {
        // Iterate through each path in the pathList
        foreach (Transform[] path in pathList)
        {
            // Instantiate the enemy for this path
            Transform newEnemy = Instantiate(enemy1, spawnPosition.position, Quaternion.identity);

            // Get the EnemyController component and assign the path
            EnemyController controller = newEnemy.GetComponent<EnemyController>();
            controller.target = path; // Assign path to enemy's target

            // Wait for 2 seconds before spawning the next enemy
            yield return new WaitForSeconds(2f);
        }
    }

}
