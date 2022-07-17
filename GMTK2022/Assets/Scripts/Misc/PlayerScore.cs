using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static int enemiesKilled;
    public PlayerComponents components;
    // Start is called before the first frame update

    private void Start()
    {
        components = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponents>();

        enemiesKilled = 0;
        components.ui.SetScoreText(enemiesKilled);
    }

    private void OnEnable()
    {
        components = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponents>();


        enemiesKilled = 0;
        components.ui.SetScoreText(enemiesKilled);
    }

    public void AddKill()
    {
        enemiesKilled ++;
        components.ui.SetScoreText(enemiesKilled);
    }
}
