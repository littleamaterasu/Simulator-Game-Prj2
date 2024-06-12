using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    string nextScene;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LoadScene);
        PlayerPrefs.DeleteKey("NextScene");
    }

    void LoadScene()
    {
        nextScene = PlayerPrefs.GetString("NextScene", "maingame");

        SceneManager.LoadScene(nextScene);
    }
}
