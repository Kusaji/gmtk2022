using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles switching and activating weapons player collects through their run.
/// </summary>
public class PlayerWeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons;
    public GameObject currentWeapon;
    public PlayerComponents components;

    private void Start()
    {
        SwitchWeapon(weapons[0]);
    }

    private void Update()
    {
        WeaponSwitcher();
    }

    public void PickUpWeapon(int selectedWeapon, int ammoToGive)
    {
        var weapon = weapons[selectedWeapon].GetComponent<WeaponController>();
        weapon.unlocked = true;
        weapon.currentAmmo += ammoToGive;

        if (weapon.currentAmmo > weapon.maxAmmo)
        {
            weapon.currentAmmo = weapon.maxAmmo;
        }

        //weapon.WeaponUIUpdater();
    }

    public void WeaponSwitcher()
    {
        //Blast Pistol
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (weapons[0].GetComponent<WeaponController>().unlocked)
            {
                SwitchWeapon(weapons[0]);
                components.animationController.playerAnimator.SetTrigger("PistolEquipped");
            }
        }

        //Revolver
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons[1].GetComponent<WeaponController>().unlocked)
            {
                SwitchWeapon(weapons[1]);
                components.animationController.playerAnimator.SetTrigger("PistolEquipped");
            }
        }

        //Blast Rifle
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (weapons[2].GetComponent<WeaponController>().unlocked)
            {
                SwitchWeapon(weapons[2]);
                components.animationController.playerAnimator.SetTrigger("RifleEquipped");
            }
        }
    }

    public void SwitchWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            currentWeapon.SetActive(false);
            currentWeapon = newWeapon;
            currentWeapon.SetActive(true);
        }
        else
        {
            currentWeapon = newWeapon;
            currentWeapon.SetActive(true);
        }
    }

}
