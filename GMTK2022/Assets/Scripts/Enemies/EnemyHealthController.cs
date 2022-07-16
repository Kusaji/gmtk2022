using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Player Components")]
    public EnemyComponents enemyComponents;

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

        if (currentHealth <= 0)
        {
            isAlive = false;
            enemyComponents.enemyAnimator.SetTrigger("Dead");
            enemyComponents.navAgent.enabled = false;
            Destroy(gameObject, 3f);
        }
    }

    public void InitializePlayerHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }
}
