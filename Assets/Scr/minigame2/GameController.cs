using System;
using TMPro;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class GameController : MonoBehaviour{
    public bool isGameEnd;
    int gamePoint = 0;
    public TextMeshProUGUI gamePointText;
    public TextMeshProUGUI endPointText;

    public UnityEngine.UI.Button restartButton;
    public UnityEngine.UI.Button backToMainButton;

    public GameObject endPanel;
    public void Start(){
        endPanel.SetActive(false);
        Time.timeScale = 0;
        restartButton.onClick.AddListener(Restart);
        backToMainButton.onClick.AddListener(BackToMainGame);

        isGameEnd = false;
    }

    public void Update(){
            if(Input.GetMouseButtonDown(0)){
                Time.timeScale = 1;
            }
    }

    public void BackToMainGame(){
        PlayerPrefs.SetString("nextScene", "maingame");
        SceneManager.LoadScene("Loading");
    }

    public void Restart(){
        Debug.Log("Restart");
        Startgame();
    }
    public void Startgame()
    {
        SceneManager.LoadScene("minigame_4");
    }

    public void EndGame(){
        endPanel.SetActive(true);
        isGameEnd = true;
        Time.timeScale = 0;
        endPointText.text = "Your point:\n" + gamePoint.ToString();
    }
    public void AddPoint(){
        gamePoint++;
        gamePointText.text = "Point: " + gamePoint.ToString();
    }
}