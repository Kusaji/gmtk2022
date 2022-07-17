using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthToHeal;
    public GameObject healPrefab;
    public bool used;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && used == false)
        {
            other.GetComponent<PlayerComponents>().health.HealHealth(healthToHeal);
            used = true;
            Instantiate(healPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
