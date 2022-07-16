using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour
{
    public float setSoundFXVolume;
    public static float soundFXVolume;
    public float bgmVolume;

    private void Start()
    {
        soundFXVolume = setSoundFXVolume;
    }
}
