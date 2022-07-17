using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallCatcher : MonoBehaviour
{
    public GameObject playerSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerComponents>().motor.SetPosition(playerSpawn.transform.position);
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}
