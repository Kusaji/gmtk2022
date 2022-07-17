using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerComponents components;

    [Header("Status")]
    public bool isAlive;

    [Header("Attributes")]
    public float maxHealth;
    public float currentHealth;

    public GameObject playerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        playerSpawn = GameObject.Find("PlayerSpawn");
        InitializePlayerHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        components.ui.SetHealthText();

        if (currentHealth <= 0)
        {
            Debug.Log($"Player has died");
            isAlive = false;
            components.animationController.playerAnimator.SetTrigger("Dead");
            components.motor.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            components.ui.pauseMenu.SetActive(true);
        }
    }

    public void HealHealth(float amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        components.ui.SetHealthText();
    }

    public void InitializePlayerHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
        components.ui.SetHealthText();
        components.motor.SetPosition(playerSpawn.transform.position);
    }
}
