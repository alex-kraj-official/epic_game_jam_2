using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy1;
    public Transform smallBoss;
    public Transform cyclops;

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
    }

    public void StartWave1() => StartCoroutine(Wave_1());
    public void StartWave2() => StartCoroutine(Wave_2());
    public void StartWave3() => StartCoroutine(Wave_3());
    public void StartWave4() => StartCoroutine(Wave_4());
    public void StartWave5() => StartCoroutine(Wave_5());
    public void StartWave6() => StartCoroutine(Wave_6());
    public void StartWave7() => StartCoroutine(Wave_7());
    public void StartWave8() => StartCoroutine(Wave_8());

    IEnumerator Wave_1()
    {
        Debug.Log("Wave 1: 1x4");
        yield return SpawnEnemies(enemy1, 1, 4);
    }

    IEnumerator Wave_2()
    {
        Debug.Log("Wave 2: 2x4");
        yield return SpawnEnemies(enemy1, 2, 4);
    }

    IEnumerator Wave_3()
    {
        Debug.Log("Wave 3: 3x4");
        yield return SpawnEnemies(enemy1, 3, 4);
    }

    IEnumerator Wave_4()
    {
        Debug.Log("Wave 4: 1x4 + 1 boss");
        yield return SpawnEnemies(enemy1, 1, 4);
        yield return new WaitForSeconds(1f);
        yield return SpawnEnemies(smallBoss, 1, 1);
    }

    IEnumerator Wave_5()
    {
        Debug.Log("Wave 5: 2x4 + 1 boss");
        yield return SpawnEnemies(enemy1, 2, 4);
        yield return new WaitForSeconds(1f);
        yield return SpawnEnemies(smallBoss, 1, 1);
    }

    IEnumerator Wave_6()
    {
        Debug.Log("Wave 6: 2x4 + 2 bosses");
        yield return SpawnEnemies(enemy1, 2, 4);
        yield return new WaitForSeconds(1f);
        yield return SpawnEnemies(smallBoss, 2, 1);
    }

    IEnumerator Wave_7()
    {
        Debug.Log("Wave 7: 2x4 + 3 bosses");
        yield return SpawnEnemies(enemy1, 2, 4);
        yield return new WaitForSeconds(1f);
        yield return SpawnEnemies(smallBoss, 3, 1);
    }

    IEnumerator Wave_8()
    {
        Debug.Log("Wave 8: Big cyclops boss");
        yield return SpawnEnemies(cyclops, 1, 1);
    }

    // Spawns `groups` of `enemiesPerGroup`, rotating through paths
    IEnumerator SpawnEnemies(Transform enemyType, int groups, int enemiesPerGroup)
    {
        for (int g = 0; g < groups; g++)
        {
            for (int i = 0; i < enemiesPerGroup && i < pathList.Count; i++)
            {
                Transform newEnemy = Instantiate(enemyType, spawnPosition.position, Quaternion.identity);
                EnemyController controller = newEnemy.GetComponent<EnemyController>();
                controller.target = pathList[i];
                yield return new WaitForSeconds(1f); // delay between enemies
            }

            yield return new WaitForSeconds(10f); // delay between groups
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartWave4();
        }
    }

   // public void spawnEnemy(Transform enemy, Transform[] path)
   // {
   //     Transform newEnemy = Instantiate(enemy1, transform.position, Quaternion.identity);
   //     EnemyController controller = newEnemy.GetComponent<EnemyController>();
   //     controller.target = path;
   // }

    //IEnumerator SpawnWave()
    //{
    //    // Iterate through each path in the pathList
    //    foreach (Transform[] path in pathList)
    //    {
    //        // Instantiate the enemy for this path
    //        Transform newEnemy = Instantiate(enemy1, spawnPosition.position, Quaternion.identity);
    //
    //        // Get the EnemyController component and assign the path
    //        EnemyController controller = newEnemy.GetComponent<EnemyController>();
    //        controller.target = path; // Assign path to enemy's target
    //
    //        // Wait for 2 seconds before spawning the next enemy
    //        yield return new WaitForSeconds(2f);
    //    }
    //}

}
