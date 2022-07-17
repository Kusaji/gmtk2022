using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Options")]
    public int currentEnemies;
    public int maxEnemies;
    public float enemySpawnCooldown;

    [Header("Scaling Difficulty")]
    public int enemiesSpawned;
    public int nextSpawnIncrease;
    public int spawnIncreaseAmount;

    [Header("Prefabs")]
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemySpawnPoints;

    [Header("Effect Prefabs")]
    public GameObject spawnEffectPrefab;

    private PlayerHealth playerHealth;
    private Transform enemyTransform;
    private PlayerComponents playerComponents;

    // Start is called before the first frame update
    void Start()
    {
        playerComponents = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponents>();
        playerHealth = playerComponents.health;
        enemyTransform = GameObject.Find("EnemySpawner").transform;
        StartCoroutine(EnemySpawnRoutine());
        playerComponents.ui.SetMaxEnemiesText(maxEnemies);
    }

    public void SpawnEnemy()
    {
        var spawnedEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], //Prefab
            enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)].transform.position + new Vector3(Random.Range(-2f, 2f), 0f, Random.Range(-2f, 2f)), //Position
            Quaternion.identity,//Rotation
            enemyTransform); //Transform

        Instantiate(spawnEffectPrefab, spawnedEnemy.transform.position, Quaternion.identity, enemyTransform);

        currentEnemies++;
        spawnedEnemy.GetComponent<EnemyComponents>().enemyHealth.mySpawner = this;
    }

    public IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(1f);

        while (playerHealth.isAlive)
        {
            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
                enemiesSpawned++;

                if (enemiesSpawned > nextSpawnIncrease)
                {
                    maxEnemies += spawnIncreaseAmount;
                    nextSpawnIncrease += nextSpawnIncrease;
                    playerComponents.ui.SetMaxEnemiesText(maxEnemies);
                }
            }
            yield return new WaitForSeconds(enemySpawnCooldown);
        }
    }
}
