using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic Pistol / Blaster weapon player will always start with.
/// Has unlimited Ammo.
/// Meant to be the weapon used to get other weapons / powerups.
/// </summary>
public class BasicBlasterWeapon : WeaponController
{
    public AudioClip fireSound;
    public AudioClip activateSound;
    public AudioClip outOfAmmoSound;
    public AudioSource speaker;


    public override void Start()
    {
        base.Start();
        WeaponUIUpdater();
        unlocked = true; //Unlock by default.. It is the basic weapon after all..
    }

    private void OnEnable()
    {
        speaker.PlayOneShot(activateSound, PlayerOptions.soundFXVolume);
        WeaponUIUpdater();
    }


    public override void BasicAttack()
    {
        base.BasicAttack();
    }

    public override IEnumerator BasicAttackRoutine()
    {
        components.animationController.playerAnimator.SetTrigger("BasicBlasterShot");
        speaker.pitch = Random.Range(0.95f, 1.05f);
        speaker.PlayOneShot(fireSound, PlayerOptions.soundFXVolume);

        GetAim();
        //var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position + shootPointOffset, shootPoint.transform.rotation);
        var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
        var spawnedProjectileController = spawnedProjectile.GetComponent<PlayerProjectile>();
        spawnedProjectileController.damage = basicAttackDamage;
        spawnedProjectileController.projectileSpeed = basicAttackProjectileSpeed;

        WeaponUIUpdater();


        yield return new WaitForSeconds(basicAttackCooldown);

        components.animationController.playerAnimator.SetTrigger("UpperBodyIdle");


    }

    public override void GetAim()
    {
        base.GetAim();
    }

    public override void WeaponUIUpdater()
    {
        components.ui.weaponInfo.text = $"{gameObject.name}\n Ammo: Infinite";
    }
}
