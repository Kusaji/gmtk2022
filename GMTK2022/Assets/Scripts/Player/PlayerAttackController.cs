using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerComponents components;

    [Space(10)]

    [Header("Player Attributes")]
    public GameObject shootPoint;
    public Vector3 shootPointOffset;
    public Vector3 cameraRayHitPoint;

    [Header("Basic Attack")]
    public float basicAttackDamage;
    public float basicAttackCooldown;
    public float basicAttackProjectileSpeed;
    public GameObject basicAttackPrefab;


    [Space(10)]

    [Header("Bools")]
    public bool basicAttackReady;

    //Coroutines
    private IEnumerator namedBasicAttackRoutine;

    // Start is called before the first frame update
    void Start()
    {
        basicAttackReady = true;
        shootPoint = GameObject.Find("PlayerShootPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            BasicAttack();
        }

    }

    public void BasicAttack()
    {
        if (basicAttackReady)
        {
            namedBasicAttackRoutine = BasicAttackRoutine();
            StartCoroutine(namedBasicAttackRoutine);
        }
    }

    public IEnumerator BasicAttackRoutine()
    {
        basicAttackReady = false;
        components.animationController.playerAnimator.SetTrigger("Attack1");
        components.audioController.PlayAttackSound();

        GetAim();
        //var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position + shootPointOffset, shootPoint.transform.rotation);
        var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        var spawnedProjectileController = spawnedProjectile.GetComponent<PlayerProjectile>();
        spawnedProjectileController.damage = basicAttackDamage;
        spawnedProjectileController.projectileSpeed = basicAttackProjectileSpeed;

        yield return new WaitForSeconds(basicAttackCooldown);

        components.animationController.playerAnimator.SetTrigger("UpperBodyIdle");

        basicAttackReady = true;
    }

    public void GetAim()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100f))
        {
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue, 5f);
            cameraRayHitPoint = hit.point;
            shootPoint.transform.LookAt(cameraRayHitPoint);
        }
    }
}
