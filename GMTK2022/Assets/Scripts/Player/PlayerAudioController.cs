using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public PlayerComponents components;
    public AudioSource speaker;

    public List<AudioClip> attackSounds;
    public List<AudioClip> deathSounds;
    public List<AudioClip> painSounds;
    public List<AudioClip> footStepSounds;
    public List<AudioClip> abilitySounds;

    public AudioClip basicBlasterSound;
    public AudioClip revolverBlasterSound;

    public void PlayAttackSound()
    {
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume);
    }

    public void PlayFootStep()
    {
        speaker.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Count)], PlayerOptions.soundFXVolume * 0.1f);
    }

    public void PlayAbilitySound(int soundEffect, float volumeReduction)
    {
        speaker.PlayOneShot(abilitySounds[soundEffect], PlayerOptions.soundFXVolume * volumeReduction);
    }

    public void PlaySoundEffect(AudioClip soundEffect)
    {
        speaker.PlayOneShot(soundEffect, PlayerOptions.soundFXVolume);
    }
}
