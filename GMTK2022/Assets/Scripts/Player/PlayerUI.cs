using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    public PlayerComponents components;
    public GameObject pauseMenu;
    public Text health;
    public bool isPaused;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Scene Called");
        var currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void SetHealthText()
    {
        health.text = $"Health: {components.health.currentHealth} | {components.health.maxHealth}";
    }
}
