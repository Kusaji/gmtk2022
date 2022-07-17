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
        speaker.pitch = Random.Range(0.95f, 1.05f);
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume * 0.3f);
    }

    public void PlayDeathSound()
    {
        speaker.pitch = Random.Range(0.95f, 1.05f);
        speaker.PlayOneShot(deathSounds[Random.Range(0, deathSounds.Count)], PlayerOptions.soundFXVolume * 0.3f);
    }

    public void PlayAggroSound()
    {
        speaker.pitch = Random.Range(0.95f, 1.05f);
        speaker.PlayOneShot(aggroSounds[Random.Range(0, aggroSounds.Count)], PlayerOptions.soundFXVolume * 0.2f);
    }
}
