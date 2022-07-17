using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Options")]
    public int weaponSlot;
    public int ammoToGive;
    public bool used;

    [Header("Components")]
    public AudioSource speaker;
    public AudioClip pickupSound;
    public GameObject weaponModel;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && used == false)
        {
            var playerComponents = other.GetComponent<PlayerComponents>();
            playerComponents.weaponSwitcher.PickUpWeapon(weaponSlot, ammoToGive);

            if (weaponModel != null)
            {
                weaponModel.SetActive(false);
            }
            
            if (weaponSlot == 1)
            {
                playerComponents.ui.UnlockRevolver();
            }
            else if (weaponSlot == 2)
            {
                playerComponents.ui.UnlockRifle();
            }

            used = true;
            speaker.PlayOneShot(pickupSound, PlayerOptions.soundFXVolume);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(this.gameObject, 1f);
        }
    }
}
