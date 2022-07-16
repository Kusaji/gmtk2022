using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Components")]
    public PlayerComponents components;
    public Text health;



    public void SetHealthText()
    {
        health.text = $"Health: {components.health.currentHealth} | {components.health.maxHealth}";
    }
}
