using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    public float minRespawnTime;
    public float maxRespawnTime;
    public GameObject healthPickup;
    public bool isSpawned;

    public AudioSource speaker;
    public AudioClip healthSpawnSound;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HealthSpawnCooldownRoutine());
    }
    IEnumerator HealthSpawnCooldownRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minRespawnTime, maxRespawnTime));
        SpawnHealthPickup();
        yield break;
    }

    public void SpawnHealthPickup()
    {
        Instantiate(healthPickup, transform.position, Quaternion.identity);
        isSpawned = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isSpawned)
        {
            isSpawned = false;
            StartCoroutine(HealthSpawnCooldownRoutine());
        }
    }
}
