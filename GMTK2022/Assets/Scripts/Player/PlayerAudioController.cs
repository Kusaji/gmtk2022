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

    public void PlayAttackSound()
    {
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume / 3);
    }

    public void PlayFootStep()
    {
        speaker.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Count)], PlayerOptions.soundFXVolume / 3);
    }
}
