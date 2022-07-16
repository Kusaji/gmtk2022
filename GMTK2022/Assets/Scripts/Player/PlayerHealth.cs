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
    // Start is called before the first frame update
    void Start()
    {
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
        }
    }

    public void InitializePlayerHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
        components.ui.SetHealthText();
    }
}
