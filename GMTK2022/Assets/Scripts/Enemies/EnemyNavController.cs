using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Base class for enemies.
/// </summary>
public class EnemyNavController : MonoBehaviour
{
    public Quaternion desiredLookAt;

    [Header("Target")]
    public GameObject currentTarget;
    public float distanceToTarget;
    public float maxDistance;
    public bool inRange;
    public bool aimingAtTarget;

    public enum navStatus { idle, following};
    [Header("Current Status")]
    public navStatus currentStatus;

    [Header("Options")]
    public float moveSpeed;
    public float followDistance;
    public float regularFollowDistance;
    public float heightDifferenceFollowDifference;
    public float lookAtSpeed;
    public Vector3 lookAtRayOffset;

    public float heightDifference;

    [Header("Components")]
    public EnemyComponents enemyComponents;

    //Coroutines
    private IEnumerator NamedFollowTargetRoutine;

    // Start is called before the first frame update
    void Start()
    {
        enemyComponents.navAgent.stoppingDistance = followDistance;
        enemyComponents.navAgent.speed = moveSpeed;

/*        if (NamedFollowTargetRoutine == null)
        {
            NamedFollowTargetRoutine = FollowTargetRoutine(currentTarget);
            StartCoroutine(NamedFollowTargetRoutine);
        } 
        else
        {
            StopCoroutine(NamedFollowTargetRoutine);
            NamedFollowTargetRoutine = FollowTargetRoutine(currentTarget);
            StartCoroutine(NamedFollowTargetRoutine);
        }*/
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (inRange && enemyComponents.enemyHealth.isAlive)
        {
            TrackTarget();
        }
    }

    public void StartFollowingTarget()
    {
        enemyComponents.playerHealth = currentTarget.GetComponent<PlayerHealth>();
        NamedFollowTargetRoutine = FollowTargetRoutine(currentTarget);
        enemyComponents.speaker.PlayAggroSound();
        StartCoroutine(NamedFollowTargetRoutine);
    }

    public virtual IEnumerator FollowTargetRoutine(GameObject target)
    {
        while (enemyComponents.enemyHealth.isAlive)
        {
            distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            heightDifference = transform.position.y - currentTarget.transform.position.y;

            if (heightDifference < -1 || heightDifference > 1)
            {
                followDistance = heightDifferenceFollowDifference;
                enemyComponents.navAgent.stoppingDistance = followDistance;
                inRange = false;
            }
            else
            {
                followDistance = regularFollowDistance;
                enemyComponents.navAgent.stoppingDistance = followDistance;
            }

            if (distanceToTarget > followDistance && distanceToTarget <= maxDistance)
            {
                enemyComponents.navAgent.destination = currentTarget.transform.position;
                currentStatus = navStatus.following;
                enemyComponents.enemyAnimator.SetTrigger("Running");
                inRange = false;
            }
            else if (distanceToTarget <= followDistance)
            {
                currentStatus = navStatus.idle;
                inRange = true;
                if (aimingAtTarget)
                {
                    enemyComponents.enemyAnimator.SetTrigger("Idle");
                }
                
            }
            else if (distanceToTarget > maxDistance)
            {
                target = null;
                enemyComponents.enemyAnimator.SetTrigger("Idle");
                yield break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public virtual void TrackTarget()
    {
        //While stationary, smoothly track target
        Vector3 relativePos = (currentTarget.transform.position - transform.position);
        Quaternion lookRotation = Quaternion.LookRotation(relativePos);
        desiredLookAt = lookRotation;

        //Set Rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lookAtSpeed * Time.deltaTime);

        //Determine if enemy is directly aiming at target.
        RaycastHit hit;

        if (Physics.Raycast(transform.position + lookAtRayOffset, transform.TransformDirection(Vector3.forward), out hit, 50f))
        {
            Debug.DrawRay(transform.position + lookAtRayOffset, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            aimingAtTarget = true;
            if (enemyComponents.enemyAttackController.isAttacking == false)
            {
                enemyComponents.enemyAttackController.StartAttacking();
            }
        }
        else
        {
            Debug.DrawRay(transform.position + lookAtRayOffset, transform.TransformDirection(Vector3.forward) * 50f, Color.white);
            enemyComponents.enemyAnimator.SetTrigger("Walking");
            aimingAtTarget = false;
            if (enemyComponents.enemyAttackController.isAttacking == true)
            {
                enemyComponents.enemyAttackController.StopAttacking();
            }
        }
    }
}
