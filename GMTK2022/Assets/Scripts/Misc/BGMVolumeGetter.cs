using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeGetter : MonoBehaviour
{
    public List<AudioClip> bgmTracks;
    public AudioSource speaker;
    public int selectedTrack;

    private static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);

        speaker.volume = PlayerOptions.bgmVolume;



        selectedTrack = Random.Range(0, bgmTracks.Count);

        StartNewTrack();
        StartCoroutine(CheckIfMusicPlaying());
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(this.gameObject);

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            speaker.volume = PlayerPrefs.GetFloat("BGMVolume");
        }
        else
        {
            PlayerPrefs.SetFloat("BGMVolume", 0.3f);
            speaker.volume = PlayerPrefs.GetFloat("BGMVolume");
        }
    }

    public void StartNewTrack()
    {
        if (speaker.isPlaying == false)
        {
            if (selectedTrack + 1 >= bgmTracks.Count)
            {
                selectedTrack = 0;
                speaker.clip = bgmTracks[selectedTrack];
                speaker.Play();
            } 
            else
            {
                selectedTrack++;
                speaker.clip = bgmTracks[selectedTrack];
                speaker.Play();
            }
        }
    }

    public IEnumerator CheckIfMusicPlaying()
    {
        while (gameObject)
        {
            StartNewTrack();
            yield return new WaitForSeconds(1f);
        }
    }
}
