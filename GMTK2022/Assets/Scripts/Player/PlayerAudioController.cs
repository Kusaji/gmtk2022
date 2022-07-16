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

    public void PlayAttackSound()
    {
        speaker.PlayOneShot(attackSounds[Random.Range(0, attackSounds.Count)], PlayerOptions.soundFXVolume);
    }
}
