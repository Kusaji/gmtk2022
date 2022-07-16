using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Options")]
    public int currentEnemies;
    public int maxEnemies;
    public float enemySpawnCooldown;

    [Header("Prefabs")]
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemySpawnPoints;

    [Header("Effect Prefabs")]
    public GameObject spawnEffectPrefab;

    private PlayerHealth playerHealth;
    private Transform enemyTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemyTransform = GameObject.Find("EnemySpawner").transform;
        StartCoroutine(EnemySpawnRoutine());
    }

    public void SpawnEnemy()
    {
        var spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], //Prefab
            enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), //Position
            Quaternion.identity,//Rotation
            enemyTransform); //Transform

        Instantiate(spawnEffectPrefab, spawnedEnemy.transform.position, Quaternion.identity, enemyTransform);

        currentEnemies++;
        spawnedEnemy.GetComponent<EnemyHealthController>().mySpawner = this;
    }

    public IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (playerHealth.isAlive)
        {
            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(enemySpawnCooldown);
        }
    }
}
