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
    public bool inRange;
    public bool aimingAtTarget;

    public enum navStatus { idle, following};
    [Header("Current Status")]
    public navStatus currentStatus;

    [Header("Options")]
    public float moveSpeed;
    public float followDistance;
    public float lookAtSpeed;
    public Vector3 lookAtRayOffset;

    [Header("Components")]
    public EnemyComponents enemyComponents;

    //Coroutines
    private IEnumerator NamedFollowTargetRoutine;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Player");
        enemyComponents.navAgent.stoppingDistance = followDistance;
        enemyComponents.navAgent.speed = moveSpeed;

        if (NamedFollowTargetRoutine == null)
        {
            NamedFollowTargetRoutine = FollowTargetRoutine(currentTarget);
            StartCoroutine(NamedFollowTargetRoutine);
        } 
        else
        {
            StopCoroutine(NamedFollowTargetRoutine);
            NamedFollowTargetRoutine = FollowTargetRoutine(currentTarget);
            StartCoroutine(NamedFollowTargetRoutine);
        }
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (inRange)
        {
            TrackTarget();
        }
    }

    public virtual IEnumerator FollowTargetRoutine(GameObject target)
    {
        while (gameObject)
        {
            distanceToTarget = Vector3.Distance(transform.position, currentTarget.transform.position);

            if (distanceToTarget > followDistance)
            {
                enemyComponents.navAgent.destination = currentTarget.transform.position;
                currentStatus = navStatus.following;
                enemyComponents.enemyAnimator.SetTrigger("Running");
                inRange = false;
            }
            else
            {
                currentStatus = navStatus.idle;
                inRange = true;
                if (aimingAtTarget)
                {
                    enemyComponents.enemyAnimator.SetTrigger("Idle");
                }
                
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
