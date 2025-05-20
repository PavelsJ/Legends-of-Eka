using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject warriorPrefab;
    public GameObject archerPrefab;
    public GameObject tankerPrefab;
    
    [Header("Item Prefabs")]
    public GameObject mushroomPrefab;

    [Header("Spawn Settings")] 
    public Transform target;
    public Transform[] spawnPoints;
    public float spawnDelay = 1f;
    
    [Header("Waves")]
    public int currentWave = 0;
    public int enemiesPerWave = 3;
    public float waveInterval = 3f;
    
    private int enemiesAlive = 0;
    
    private void Start()
    {
        StartCoroutine(SpawnNextWave());
    }

    private IEnumerator SpawnNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        currentWave++;
        int enemiesToSpawn = enemiesPerWave + (currentWave - 1) * 2;

        Debug.Log($"Wave {currentWave}: Spawning {enemiesToSpawn} enemies");

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnRandomEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        
        SpawnMushroom();
    }

    private void SpawnRandomEnemy()
    {
        GameObject prefabToSpawn = GetRandomEnemyPrefab();
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject enemy = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity, transform.root);
        
        Enemy enemyNav = enemy.GetComponent<Enemy>();
        if (enemyNav != null)
        {
            enemyNav.target = target;
        }
        
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        if (enemyStats != null)
        {
            enemyStats.OnDeath += OnEnemyDied; 
        }

        enemiesAlive++;
    }

    private GameObject GetRandomEnemyPrefab()
    {
        GameObject[] options = { warriorPrefab, archerPrefab, tankerPrefab };
        return options[Random.Range(0, options.Length)];
    }
    
    private void SpawnMushroom()
    {
        if (mushroomPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Mushroom prefab or spawn points not set.");
            return;
        }

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(mushroomPrefab, spawnPoint.position, Quaternion.identity, transform.root);
    }


    private void OnEnemyDied()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0)
        {
            StartCoroutine(SpawnNextWave());
        }
    }
}
