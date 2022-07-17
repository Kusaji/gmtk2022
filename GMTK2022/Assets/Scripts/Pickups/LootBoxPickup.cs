using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxPickup : MonoBehaviour
{
    public List<GameObject> possibleDrops;
    public GameObject explosionPrefab;
    public AudioSource speaker;
    public AudioClip openSound;
    public AudioClip landedSound;
    public bool opened;
    public bool landed;

    public GameObject lootBoxTop;
    public GameObject lootBoxBottom;
    public GameObject idleEffect;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && opened == false)
        {
            opened = true;
            speaker.PlayOneShot(openSound, PlayerOptions.soundFXVolume);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Instantiate(possibleDrops[Random.Range(0, possibleDrops.Count)], transform.position, Quaternion.identity);
            lootBoxBottom.SetActive(false);
            lootBoxTop.SetActive(false);
            idleEffect.SetActive(false);
            Destroy(this.gameObject, 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!landed)
        {
            landed = true;
            speaker.PlayOneShot(landedSound, PlayerOptions.soundFXVolume * 0.5f);
        }
    }
}
