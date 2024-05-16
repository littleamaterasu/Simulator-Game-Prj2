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

    public GameObject endPanel;
    public UnityEngine.UI.Button restartButton;
    public void Start(){
        endPanel.SetActive(false);
        Time.timeScale = 0;
        isGameEnd = false;
    }

    public void Update(){
        if(isGameEnd){
            if(Input.GetMouseButtonDown(0)){
                Startgame();
            }
        }
        else{
            if(Input.GetMouseButtonDown(0)){
                Time.timeScale = 1;
            }
        }
    }

    public void BackToMainGame(){
        Debug.Log("Back to main game");
        SceneManager.LoadScene(0);
    }

    public void Restart(){
        Debug.Log("Restart");
        Startgame();
    }
    public void Startgame()
    {
        SceneManager.LoadScene(2);
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