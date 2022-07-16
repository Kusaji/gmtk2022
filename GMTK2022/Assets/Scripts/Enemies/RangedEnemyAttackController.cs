using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttackController : AttackController
{
    [Header("Attributes")]
    public float projectileSpeed;

    [Header("Prefabs")]
    public GameObject projectile;

    [Header("Options")]
    public bool debugLog;


    //Coroutines
    private IEnumerator namedAttackRoutine;

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
        var spawnedProjectile = Instantiate(projectile, transform.position + enemyComponents.enemyNavController.lookAtRayOffset, transform.rotation);
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
        while (enemyComponents.enemyNavController.aimingAtTarget)
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
