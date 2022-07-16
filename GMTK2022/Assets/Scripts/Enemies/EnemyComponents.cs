using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Single script to hold all required components for easy access from other scripts.
/// </summary>
public class EnemyComponents : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent navAgent;
    public Animator enemyAnimator;
    public AttackController enemyAttackController;
    public EnemyNavController enemyNavController;
    public EnemySoundController speaker;
    public EnemyHealthController enemyHealth;
    public EnemyAggro enemyAggro;
}
