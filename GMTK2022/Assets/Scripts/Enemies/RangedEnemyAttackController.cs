using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttackController : AttackController
{
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
        //Spawn Projectile Prefab.

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
