using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxSpawner : MonoBehaviour
{
    [Header("Spawn Points")]
    public List<GameObject> spawnPoints;

    [Header("Components & Options")]
    public float minWaitTime;
    public float maxWaitTime;
    public List<GameObject> lootBoxes;
    public List<AudioClip> lootBoxSpawnSounds;
    public AudioSource speaker;

    public PlayerHealth playerHealth;

    private void Start()
    {
        StartCoroutine(LootBoxSpawnRoutine());
    }

    public void SpawnLootBox()
    {
        int selectedBox = Random.Range(0, lootBoxes.Count);
        Instantiate(lootBoxes[selectedBox],
            spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2,2)),
            Quaternion.identity);
        //speaker.PlayOneShot(lootBoxSpawnSounds[selectedBox]);
    }

    public IEnumerator LootBoxSpawnRoutine()
    {
        yield return new WaitForSeconds(1f);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        while (playerHealth.isAlive)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            SpawnLootBox();
        }
    }
}
