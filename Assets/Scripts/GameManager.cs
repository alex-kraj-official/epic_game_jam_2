using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 1;
    public float taxInterval = 10f; // Time between tax requests
    public GameObject taxPanel; // UI panel that gives the choice
    public TextMeshProUGUI levelText;

    private bool waitingForChoice = false;


    void Start()
    {
        InvokeRepeating(nameof(TriggerTaxEvent), taxInterval, taxInterval);
        UpdateLevelUI();
    }
    void TriggerTaxEvent()
    {
        if (!waitingForChoice)
        {
            Time.timeScale = 0f; // Pause game
            taxPanel.SetActive(true);
            waitingForChoice = true;
        }
    }
    public void PayWithSheep()
    {
        Debug.Log("Player paid tax.");
        taxPanel.SetActive(false);
        Time.timeScale = 1f;
        waitingForChoice = false;
    }
    public void PayWithPeople()
    {
        Debug.Log("Player paid tax.");
        taxPanel.SetActive(false);
        Time.timeScale = 1f;
        waitingForChoice = false;
    }

    public void FightEnemy()
    {
        Debug.Log("Player chose to fight.");
        taxPanel.SetActive(false);
        Time.timeScale = 1f;
        waitingForChoice = false;

        // Spawn enemy here
        StartFight();
    }

    void StartFight()
    {
        Debug.Log("Enemy fight started!");

        // Placeholder: spawn enemy and start combat
        // When combat ends successfully:
    }



    void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + currentLevel;
    }
}
