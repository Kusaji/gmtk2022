using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [Header("Attributes")]
    public float damage;
    public float attackCooldown;

    [Header("Statuses")]
    public bool isAttacking;

    [Header("Components")]
    public EnemyComponents enemyComponents;

    public virtual void StartAttacking()
    {
        //Implement in Extended Class
    }

    public virtual void StopAttacking()
    {
        //Implement in Extended Class
    }

    public virtual void Attack()
    {
        //Implement in Extended Class
    }
}
