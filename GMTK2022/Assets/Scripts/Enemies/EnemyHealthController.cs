using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [Header("Player Components")]
    public EnemyComponents enemyComponents;
    public EnemySpawner mySpawner;

    [Header("Status")]
    public bool isAlive;

    [Header("Attributes")]
    public float maxHealth;
    public float currentHealth;

    [Header("Prefabs")]
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InitializePlayerHealth();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (enemyComponents.enemyNavController.currentTarget == null)
        {
            enemyComponents.enemyNavController.currentTarget = GameObject.FindGameObjectWithTag("Player");
            enemyComponents.enemyNavController.StartFollowingTarget();
        }
        if (currentHealth <= 0 && isAlive)
        {
            StartCoroutine(DeathRoutine());
        }
    }

    public void InitializePlayerHealth()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public IEnumerator DeathRoutine()
    {
        isAlive = false;
        enemyComponents.enemyAnimator.SetTrigger("Dead");
        enemyComponents.navAgent.enabled = false;
        enemyComponents.speaker.PlayDeathSound();
        mySpawner.currentEnemies--;
        yield return new WaitForSeconds(2f);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
