using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerComponents components;

    public LayerMask ignorePlayer;
    public LayerMask ignoreRays;

    public float nextShotTime;

    [Space(10)]

    [Header("Lock Status")]
    public bool unlocked;

    [Header("Ammo")]
    public int maxAmmo;
    public int currentAmmo;

    [Header("Weapon Attributes")]
    public GameObject shootPoint;
    public Vector3 shootPointOffset;
    public Vector3 cameraRayHitPoint;

    [Header("Primary Fire")]
    public float basicAttackDamage;
    public float basicAttackCooldown;
    public float basicAttackProjectileSpeed;
    public GameObject basicAttackPrefab;


    [Space(10)]

    [Header("Bools")]
    public bool basicAttackReady;

    public LayerMask layerMask;

    //Coroutines
    private IEnumerator namedBasicAttackRoutine;

    // Start is called before the first frame update
    public virtual void Start()
    {
        basicAttackReady = true;
        WeaponUIUpdater();
        shootPoint = GameObject.Find("PlayerShootPoint");
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (components.health.isAlive)
        {
            if (Input.GetMouseButton(0))
            {
                BasicAttack();
            }
        }

        if (Time.time >= nextShotTime)
        {
            basicAttackReady = true;
        }

    }

    public virtual void WeaponUIUpdater()
    {
        components.ui.weaponInfo.text = $"{gameObject.name}\n Ammo: {currentAmmo} | {maxAmmo}";
    }

    public virtual void BasicAttack()
    {
        if (basicAttackReady)
        {
            namedBasicAttackRoutine = BasicAttackRoutine();
            StartCoroutine(namedBasicAttackRoutine);
            nextShotTime = Time.time + basicAttackCooldown;
            basicAttackReady = false;
        }
    }

    private void OnDisable()
    {
        components.animationController.playerAnimator.SetTrigger("UpperBodyIdle");
    }

    public virtual IEnumerator BasicAttackRoutine()
    {
        components.animationController.playerAnimator.SetTrigger("Attack1");

        GetAim();
        //var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position + shootPointOffset, shootPoint.transform.rotation);
        var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        var spawnedProjectileController = spawnedProjectile.GetComponent<PlayerProjectile>();
        spawnedProjectileController.damage = basicAttackDamage;
        spawnedProjectileController.projectileSpeed = basicAttackProjectileSpeed;

        yield return new WaitForSeconds(basicAttackCooldown);

        components.animationController.playerAnimator.SetTrigger("UpperBodyIdle");
    }

    /// <summary>
    /// Handles offset between 3rd person camera and where prefab is instantiated.
    /// </summary>
    public virtual void GetAim()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 100f, layerMask))
        {
            //
            //Uncomment out if you need to see what the player is aiming at / hitting.
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue, 5f);
            Debug.Log(hit.transform.gameObject);


            cameraRayHitPoint = hit.point;
            shootPoint.transform.LookAt(cameraRayHitPoint);
        }
    }
}
