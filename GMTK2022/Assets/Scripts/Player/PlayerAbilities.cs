using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public PlayerComponents components;

    [Header("Teleport")]
    public bool teleportReady;
    public float teleportCooldown;
    public float minimumTeleportDistance;
    public float maximumTeleportDistance;
    public GameObject teleportEffectPrefab;

    // Start is called before the first frame update
    void Start()
    {
        teleportReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && teleportReady)
        {
            if (Input.GetKey(KeyCode.W)) 
            {
                Teleport(transform.forward);
            }
            else if (Input.GetKey(KeyCode.S)) 
            {
                Teleport(-transform.forward);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                Teleport(-transform.right);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Teleport(transform.right);
            }
            else
            {
                Teleport(transform.forward);
            }
        }
    }

    void Teleport(Vector3 direction)
    {
        Instantiate(teleportEffectPrefab, transform.position, Quaternion.Euler(90f, 0.0f, 0.0f));
        components.motor.SetPosition(transform.position + direction * Random.Range(minimumTeleportDistance, maximumTeleportDistance));
        Instantiate(teleportEffectPrefab, transform.position, Quaternion.Euler(90f, 0.0f, 0.0f));
        components.audioController.PlayAbilitySound(0, 0.5f);
        StartCoroutine(TeleportCooldownRoutine());
    }

    public IEnumerator TeleportCooldownRoutine()
    {
        teleportReady = false;
        yield return new WaitForSeconds(teleportCooldown);
        teleportReady = true;
    }
}
