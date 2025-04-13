using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int day = 1;
    public float taxInterval = 10f; // Time between tax requests
    public GameObject taxPanel; // UI panel that gives the choice
    public TextMeshProUGUI levelText;

    private bool waitingForChoice = false;

    public EnemySpawner spawner;


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
        TriggerNextWave();
    }

    public void TriggerNextWave()
    {
        switch (currentLevel)
        {
            case 1: spawner.StartWave1(); break;
            case 2: spawner.StartWave2(); break;
            case 3: spawner.StartWave3(); break;
            case 4: spawner.StartWave4(); break;
            case 5: spawner.StartWave5(); break;
            case 6: spawner.StartWave6(); break;
            case 7: spawner.StartWave7(); break;
            case 8: spawner.StartWave8(); break;
        }

        currentLevel++;
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = "Level: " + currentLevel;
    }
}
