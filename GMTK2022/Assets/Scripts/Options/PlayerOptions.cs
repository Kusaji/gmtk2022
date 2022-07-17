using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{
    public float setSoundFXVolume;
    public static float soundFXVolume;

    public float setBGMVolume;
    public static float bgmVolume;

    public Slider sfxVolumeSlider;
    public Slider bgmVolumeSlider;

    public AudioSource bgmSpeaker;

    private static GameObject instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetVolume()
    {
        setSoundFXVolume = sfxVolumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", setSoundFXVolume);
        soundFXVolume = setSoundFXVolume;
    }

    public void SetBGMVolume()
    {

        setBGMVolume = bgmVolumeSlider.value;
        PlayerPrefs.SetFloat("BGMVolume", setBGMVolume);
        bgmVolume = setBGMVolume;
        bgmSpeaker.volume = bgmVolume;
    }

    public void GetSFXVolumeSlider()
    {
        sfxVolumeSlider = GameObject.Find("SFXVolumeSlider").GetComponent<Slider>();
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SetVolume(); });
        sfxVolumeSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void GetBGMVolumeSlider()
    {
        bgmVolumeSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        bgmVolumeSlider.onValueChanged.AddListener(delegate { SetBGMVolume(); });
        bgmVolumeSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            setSoundFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            soundFXVolume = setSoundFXVolume;
        }
        else
        {
            setSoundFXVolume = 1.0f;
            PlayerPrefs.SetFloat("SFXVolume", setSoundFXVolume);
            soundFXVolume = setSoundFXVolume;
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            setBGMVolume = PlayerPrefs.GetFloat("BGMVolume");
            bgmVolume = setBGMVolume;
            bgmSpeaker.volume = setBGMVolume;
        }
        else
        {
            setBGMVolume = 0.3f;
            PlayerPrefs.SetFloat("BGMVolume", setBGMVolume);
            bgmVolume = setBGMVolume;
        }
    }
}
