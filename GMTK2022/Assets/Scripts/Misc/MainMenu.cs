using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public PlayerOptions playerOptions;
    public GameObject controls;
    public GameObject optionsPanel;
    public GameObject menu;

    private void Start()
    {
        playerOptions = GameObject.Find("PlayerOptions").GetComponent<PlayerOptions>();
        Time.timeScale = 1.0f;
    }

    public void OpenControls()
    {
        if (controls.activeSelf == true)
        {
            controls.SetActive(false);
            menu.SetActive(true);
        }
        else
        {
            controls.SetActive(true);
            menu.SetActive(false);

        }
    }

    public void OpenOptions()
    {
        if (optionsPanel.activeSelf == true)
        {
            optionsPanel.SetActive(false);
            menu.SetActive(true);
        }
        else
        {
            optionsPanel.SetActive(true);
            playerOptions.GetSFXVolumeSlider();
            playerOptions.GetBGMVolumeSlider();
            menu.SetActive(false);

        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
