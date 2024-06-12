using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    InputManager manager;
    QuizzShow quizzShow;
    QuizzGen quizzGen;

    [SerializeField]
    TMP_Text time;

    [SerializeField]
    TMP_Text count;

    [SerializeField]
    TMP_Text rank;

    [SerializeField]
    RawImage background;

    [SerializeField]
    Button playAgain;

    [SerializeField]
    Button mainGame;

    private bool rightAns = false;

    private bool finish = false;

    private float playingTime = 0f;

    private int quizzCount = 0;

    private int point = 0;

    private List<int> currentRank = new();

    private bool isAdded = false;

    private void Start()
    {
        playAgain.gameObject.SetActive(false);
        mainGame.gameObject.SetActive(false);

        playAgain.onClick.AddListener(OnPlayAgainClicked); // Add listener for playAgain button
        mainGame.onClick.AddListener(OnMainGameClicked);   // Add listener for mainGame button

        RankUpdate(0);
        count.text = quizzCount.ToString();

        background.gameObject.SetActive(false);
        rank.gameObject.SetActive(false);

        quizzGen = GetComponent<QuizzGen>();
        quizzShow = GetComponent<QuizzShow>();

        quizzGen.GenerateQuizz();
        quizzShow.ShowQuizz(quizzGen.GetFirst(), quizzGen.GetSecond(), quizzGen.GetFactor());
    }

    IEnumerator CheckAnswer()
    {
        manager.Deactivate();
        yield return new WaitForSeconds(.1f);
        rightAns = quizzGen.CheckQuizz(manager.GetInput());
        if (rightAns)
        {
            quizzCount++;
            point += quizzGen.Point();

            quizzGen.GenerateQuizz();
            quizzShow.ShowQuizz(quizzGen.GetFirst(), quizzGen.GetSecond(), quizzGen.GetFactor());
        }
        else
        {
            playingTime += 1f;
        }
        manager.Activate();
    }

    void RankUpdate(int newPoint)
    {
        currentRank.Clear();
        for (int i = 1; i <= 5; ++i)
        {
            currentRank.Add(PlayerPrefs.GetInt(i.ToString(), 0));
        }

        for (int i = 0; i < 5; ++i)
        {
            if (newPoint > currentRank[i])
            {
                currentRank.Insert(i, newPoint);
                currentRank.RemoveAt(5);
                break;
            }
        }
    }

    void OnPlayAgainClicked()
    {
        quizzCount = 0;
        point = 0;
        playingTime = 0f;
        manager.Activate();
        background.gameObject.SetActive(false);
        rank.gameObject.SetActive(false);
        finish = false;
        isAdded = false;
        playAgain.gameObject.SetActive(false);
        mainGame.gameObject.SetActive(false);
    }

    void OnMainGameClicked()
    {
        PlayerPrefs.SetString("nextScene", "maingame");
        SceneManager.LoadScene("Loading");
    }

    // Update is called once per frame
    void Update()
    {
        count.text = quizzCount.ToString();
        if (finish)
        {

            mainGame.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            return;
        }

        if (quizzCount == 10 || playingTime > 60)
        {
            point += (int)(60 - playingTime) * 10;
            RankUpdate(point);
            background.gameObject.SetActive(true);
            rank.gameObject.SetActive(true);

            rank.text = "Your point: " + point.ToString() + '\n'
                + "1: " + currentRank[0].ToString() + '\n'
                + "2: " + currentRank[1].ToString() + '\n'
                + "3: " + currentRank[2].ToString() + '\n'
                + "4: " + currentRank[3].ToString() + '\n'
                + "5: " + currentRank[4].ToString();
            manager.Deactivate();
            if (quizzCount >= 10 && !isAdded)
            {
                isAdded = true;
                PlayerPrefs.SetInt("paper", PlayerPrefs.GetInt("paper", 0) + 1);
            }
            finish = true;
        }

        playingTime += Time.deltaTime;
        time.text = "Time used: " + ((int)playingTime).ToString();

        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CheckAnswer());
        }
    }
}
