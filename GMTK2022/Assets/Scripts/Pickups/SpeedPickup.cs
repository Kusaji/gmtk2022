using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float speedIncrease;
    public GameObject speedEffectPrefab;
    public bool used;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && used == false)
        {
            var playerComponents = other.GetComponent<PlayerComponents>();
            playerComponents.character.MaxStableMoveSpeed += speedIncrease;
            playerComponents.ui.SetMaxMoveSpeedText(playerComponents.character.MaxStableMoveSpeed);
            used = true;
            Instantiate(speedEffectPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
