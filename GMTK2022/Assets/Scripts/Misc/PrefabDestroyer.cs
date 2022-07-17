using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabDestroyer : MonoBehaviour
{
    public float destroyTime;
    public AudioSource speaker;
    public bool lowerAudio;
    public float lowerAmount;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);

        if (speaker != null)
        {
            if (!lowerAudio)
            {
                speaker.volume = PlayerOptions.soundFXVolume;
            }
            else if (lowerAudio)
            {
                speaker.volume = PlayerOptions.soundFXVolume * lowerAmount;
            }
        }
    }
}
