using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public List<AudioClip> attackSounds;
    public AudioSource speaker;

    public void PlayAttackSound()
    {
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume);
    }
}
