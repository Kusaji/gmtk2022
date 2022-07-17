using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverBlaster : WeaponController
{
    public AudioClip fireSound;
    public AudioClip activateSound;
    public AudioClip outOfAmmoSound;
    public AudioSource speaker;

    public override void Start()
    {
        base.Start();
        base.WeaponUIUpdater();
    }

    private void OnEnable()
    {
        speaker.PlayOneShot(activateSound, PlayerOptions.soundFXVolume);
        base.WeaponUIUpdater();
    }

    public override void BasicAttack()
    {
        base.BasicAttack();
    }

    public override IEnumerator BasicAttackRoutine()
    {
        if (currentAmmo > 0)
        {
            components.animationController.playerAnimator.SetTrigger("RevolverBlasterShot");
            speaker.pitch = Random.Range(0.95f, 1.05f);
            speaker.PlayOneShot(fireSound, PlayerOptions.soundFXVolume);
            currentAmmo--;
            GetAim();
            //var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position + shootPointOffset, shootPoint.transform.rotation);
            var spawnedProjectile = Instantiate(basicAttackPrefab, shootPoint.transform.position, shootPoint.transform.rotation);
            var spawnedProjectileController = spawnedProjectile.GetComponent<PlayerProjectile>();
            spawnedProjectileController.damage = basicAttackDamage;
            spawnedProjectileController.projectileSpeed = basicAttackProjectileSpeed;

            base.WeaponUIUpdater();

            yield return new WaitForSeconds(basicAttackCooldown);

            components.animationController.playerAnimator.SetTrigger("UpperBodyIdle");
        }
        else
        {
            components.animationController.playerAnimator.SetTrigger("RevolverBlasterShot");
            speaker.PlayOneShot(outOfAmmoSound, PlayerOptions.soundFXVolume);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void GetAim()
    {
        base.GetAim();
    }
}
