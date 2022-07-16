using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public List<AudioClip> attackSounds;
    public List<AudioClip> deathSounds;
    public List<AudioClip> aggroSounds;
    public AudioSource speaker;

    public void PlayAttackSound()
    {
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume);
    }

    public void PlayDeathSound()
    {
        speaker.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Count)], PlayerOptions.soundFXVolume);
    }

    public void PlayAggroSound()
    {
        speaker.PlayOneShot(aggroSounds[Random.Range(0, aggroSounds.Count)], PlayerOptions.soundFXVolume * 0.5f);
    }
}
