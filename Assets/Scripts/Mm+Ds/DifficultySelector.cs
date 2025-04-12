using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] private GameObject easyButton;
    [SerializeField] private GameObject normalButton;
    [SerializeField] private GameObject hardButton;

    [SerializeField] public static int difficulty;

    [SerializeField] private GameObject BackBtn;

    private bool BackButtonPressed;

    private void Update()
    {
        CheckInput();
    }

    private void FixedUpdate()
    {
        if (BackButtonPressed) Back();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonPressed = true;
            Back();
        }
    }

    public void SetDifficulty(string level)
    {
        switch (level)
        {
            case "easy":
                difficulty = 0;
                break;
            case "normal":
                difficulty = 1;
                break;
            case "hard":
                difficulty = 2;
                break;
            default:
                difficulty = -1;
                break;
        }

        Debug.Log("Selected difficulty: " + difficulty);
    }

    public void Back()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
