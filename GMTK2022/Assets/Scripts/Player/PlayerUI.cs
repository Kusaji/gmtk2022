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
    public Text weaponInfo;
    public bool isPaused;
    public Text scoreText;
    public Text maxEnemiesText;
    public Text maxMoveSpeedText;

    public Image blasterIcon;
    public Image revolverIcon;
    public Image rifleIcon;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        revolverIcon.color = new Color(1, 1, 1, 0.2f);
        rifleIcon.color = new Color(1, 1, 1, 0.2f);

        SetMaxMoveSpeedText(components.character.MaxStableMoveSpeed);
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

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetHealthText()
    {
        health.text = $"Health: {components.health.currentHealth} | {components.health.maxHealth}";
    }

    public void UnlockRifle()
    {
        rifleIcon.color = new Color(1, 1, 1, 1.0f);
    }

    public void UnlockRevolver()
    {
        revolverIcon.color = new Color(1, 1, 1, 1.0f);
    }

    public void SetScoreText(int enemiesKilled)
    {
        scoreText.text = $"Enemies Killed: {enemiesKilled}";
    }

    public void SetMaxEnemiesText(int maxEnemies)
    {
        maxEnemiesText.text = $"Max Enemies: {maxEnemies}";
    }

    public void SetMaxMoveSpeedText(float moveSpeed)
    {
        maxMoveSpeedText.text = $"Move Speed: {moveSpeed}";
    }
}
