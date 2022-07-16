using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttackController : AttackController
{
    [Header("Attributes")]
    public int minimumProjectileSpeed;
    public int maximumProjectileSpeed;
    public float projectileSpeed;

    [Header("GameObjects")]
    public GameObject shootPoint;

    [Header("Prefabs")]
    public GameObject projectile;

    [Header("Options")]

    public bool debugLog;


    //Coroutines
    private IEnumerator namedAttackRoutine;

    private void Start()
    {
        projectileSpeed = Random.Range(minimumProjectileSpeed, maximumProjectileSpeed);
    }

    public override void StartAttacking()
    {
        base.StartAttacking();

        namedAttackRoutine = AttackRoutine();

        if (debugLog)
        {
            Debug.Log($"{gameObject.name} attack routine started.");
        }
        StartCoroutine(namedAttackRoutine);

    }

    public override void Attack()
    {
        base.Attack();
        enemyComponents.enemyAnimator.SetTrigger("Attack");
        enemyComponents.speaker.PlayAttackSound();
        shootPoint.transform.LookAt(enemyComponents.enemyNavController.currentTarget.transform.position + new Vector3(0.0f, 1.0f, 0.0f));
        var spawnedProjectile = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
        var spawnedProjectileController = spawnedProjectile.GetComponent<EnemyProjectileController>();
        spawnedProjectileController.damage = damage;
        spawnedProjectileController.projectileSpeed = projectileSpeed;

        if (debugLog)
        {
            Debug.Log($"{gameObject.name} attacking for {damage} damage.");
        }
    }

    public IEnumerator AttackRoutine()
    {
        isAttacking = true;
        while (enemyComponents.enemyNavController.aimingAtTarget && enemyComponents.enemyHealth.isAlive && enemyComponents.playerHealth.isAlive)
        {
            Attack();
            yield return new WaitForSeconds(attackCooldown);
        }

        if (debugLog)
        {
            Debug.Log($"{gameObject.name} attack routine stopped.");
        }
        isAttacking = false;
        yield break;
    }
}
