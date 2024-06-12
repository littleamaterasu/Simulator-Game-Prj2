using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Button loadMainGameButton;
    [SerializeField]
    Button resetAndLoadMainGameButton;
    [SerializeField]
    Button saveDataButton;

    void Start()
    {
        // Add listeners to buttons
        loadMainGameButton.onClick.AddListener(LoadMainGame);
        resetAndLoadMainGameButton.onClick.AddListener(ResetAndLoadMainGame);
        saveDataButton.onClick.AddListener(SaveDataAndQuit);
    }

    void LoadMainGame()
    {
        PlayerPrefs.SetString("nextScene", "maingame");
        SceneManager.LoadScene("Loading"); // Change "MainGameScene" to your main game scene name
    }

    void ResetAndLoadMainGame()
    {
        // Reset PlayerPrefs values
        PlayerPrefs.SetInt("Exp", 0);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("book", 0);
        PlayerPrefs.SetInt("task", 0);
        PlayerPrefs.SetInt("paper", 0);
        PlayerPrefs.SetInt("Time", 0);

        PlayerPrefs.Save(); // Ensure the changes are saved

        // Load the main game scene
        LoadMainGame();
    }

    void SaveDataAndQuit()
    {

        PlayerPrefs.Save(); // Ensure the changes are saved

        // Quit the application
        Application.Quit();

        // If running in the editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    void Update()
    {
        // Update logic if necessary
    }
}
