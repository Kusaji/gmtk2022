using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyHealthController : EnemyHealthController
{
    public GameObject enemyModel;
    public Rigidbody enemyRigidBody;
    public SphereCollider enemyCollider;

    public override IEnumerator DeathRoutine()
    {
        isAlive = false;
        enemyComponents.enemyAnimator.SetTrigger("Dead");
        enemyComponents.navAgent.enabled = false;
        enemyComponents.speaker.PlayDeathSound();

        //Enable physics on "ragdoll"
        //enemyModel.transform.parent = null;
        enemyCollider.enabled = true;
        enemyRigidBody.isKinematic = false;


        mySpawner.currentEnemies--;
        yield return new WaitForSeconds(2f);
        Instantiate(explosionPrefab, enemyModel.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
