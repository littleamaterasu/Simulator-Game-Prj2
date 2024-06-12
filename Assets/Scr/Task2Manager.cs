using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task2Manager : MonoBehaviour
{
    int Short = 0;
    int Medium = 0;
    int Long = 0;

    public int ShortFinished = 0;
    public int MediumFinished = 0;
    public int LongFinished = 0;

    [SerializeField]
    TMP_Text ShortText;

    [SerializeField]
    TMP_Text MediumText;

    [SerializeField]
    TMP_Text LongText;

    [SerializeField]
    Image image;

    [SerializeField]
    Image bg;

    [SerializeField]
    Button Continue;


    private int[] Amount = new int[7] { 2, 3, 5, 8, 13, 21, 34 };

    IEnumerator ShowPictureForSecs(int sec)
    {
        image.enabled = true;
        yield return new WaitForSeconds(sec);
        image.enabled = false;
    }

    public void ShowPicture(int sec)
    {
        StartCoroutine(ShowPictureForSecs(sec));
    }

    public bool CheckFinish()
    {
        if (Short == 0 || Medium == 0 || Long == 0) return false;
        if (ShortFinished < Short || MediumFinished < Medium || LongFinished < Long) return false;
        return true;
    }

    public void GenTask2()
    {
        int level = Math.Min(PlayerPrefs.GetInt("Level" + 1, 0), 4);

        ShortFinished -= Short;
        MediumFinished -= Medium;
        LongFinished -= Long;

        Short = UnityEngine.Random.Range(1, Amount[level + 2]);
        Medium = UnityEngine.Random.Range(1, Amount[level + 1]);
        Long = UnityEngine.Random.Range(1, Amount[level]);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Short", Short);
        PlayerPrefs.SetInt("Medium", Medium);
        PlayerPrefs.SetInt("Long", Long);
    }

    private void Start()
    {
        image.enabled = false;
        bg.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);
        Continue.onClick.AddListener(Cont);
        GenTask2();
    }

    void Cont()
    {
        bg.gameObject.SetActive(false);
        Continue.gameObject.SetActive(false);
        GenTask2();
    }

    private void Update()
    {
        ShortText.text = "Short Survey(s): " + ShortFinished.ToString() + '/' + Short.ToString();
        MediumText.text = "Medium Survey(s): " + MediumFinished.ToString() + '/' + Medium.ToString();
        LongText.text = "Long Survey(s): " + LongFinished.ToString() + '/' + Long.ToString();
        if(CheckFinish())
        {
            PlayerPrefs.SetInt("task", PlayerPrefs.GetInt("task", 0) + 1);
            bg.gameObject.SetActive(true);
            Continue.gameObject.SetActive(true);
        }
    }
}
