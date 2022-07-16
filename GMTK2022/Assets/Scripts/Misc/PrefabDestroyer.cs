using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDestroyer : MonoBehaviour
{
    public float destroyTime;
    public AudioSource speaker;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        if (speaker != null)
        {
            speaker.volume = PlayerOptions.soundFXVolume;
        }
    }
}
