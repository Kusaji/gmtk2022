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
        if (Input.GetMouseButtonDown(0))
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

        var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position + shootPointOffset, shootPoint.transform.rotation);
        var spawnedProjectileController = spawnedProjectile.GetComponent<PlayerProjectile>();
        spawnedProjectileController.damage = basicAttackDamage;
        spawnedProjectileController.projectileSpeed = basicAttackProjectileSpeed;

        yield return new WaitForSeconds(basicAttackCooldown);
        basicAttackReady = true;
    }
}
