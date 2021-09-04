using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 0;
    private void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (enemyCount >= 1)
            return;

        enemyCount++;
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.GetComponent<Enemy>().spawnerRef = gameObject;
    }
}
