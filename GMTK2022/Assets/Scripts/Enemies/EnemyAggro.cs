using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public EnemyComponents enemyComponents;
    public Vector3 enemyAggroRadius;
    // Start is called before the first frame update

    private void Start()
    {
        transform.localScale = enemyAggroRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (enemyComponents.enemyNavController.currentTarget == null)
            {
                enemyComponents.enemyNavController.currentTarget = other.gameObject;
                enemyComponents.enemyNavController.StartFollowingTarget();
            }
        }
    }
}
