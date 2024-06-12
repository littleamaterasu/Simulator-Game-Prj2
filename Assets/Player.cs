using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text LevelText;
    [SerializeField] private TMP_Text ExpText;

    private const string LEVEL_KEY = "Level";
    private const string EXP_KEY = "Exp";
    private const string BOOK_KEY = "book";
    private const string TASK_KEY = "task";
    private const string PAPER_KEY = "paper";

    void Start()
    {
        PlayerPrefs.SetInt(BOOK_KEY, 10000);
        PlayerPrefs.SetInt(TASK_KEY, 10000);
        PlayerPrefs.SetInt(PAPER_KEY, 10000);
        LoadPlayerData();
        UpdateUI();
    }

    void LoadPlayerData()
    {
        PlayerPrefs.GetInt(LEVEL_KEY, 0);
        PlayerPrefs.GetInt(EXP_KEY, 0);
        PlayerPrefs.GetInt(BOOK_KEY, 0);
        PlayerPrefs.GetInt(TASK_KEY, 0);
        PlayerPrefs.GetInt(PAPER_KEY, 0);
    }

    void SavePlayerData()
    {
        PlayerPrefs.SetInt(LEVEL_KEY, PlayerPrefs.GetInt(LEVEL_KEY, 0));
        PlayerPrefs.SetInt(EXP_KEY, PlayerPrefs.GetInt(EXP_KEY, 0));
        PlayerPrefs.SetInt(BOOK_KEY, PlayerPrefs.GetInt(BOOK_KEY, 0));
        PlayerPrefs.SetInt(TASK_KEY, PlayerPrefs.GetInt(TASK_KEY, 0));
        PlayerPrefs.SetInt(PAPER_KEY, PlayerPrefs.GetInt(PAPER_KEY, 0));

        PlayerPrefs.Save();
    }

    void UpdateUI()
    {
        LevelText.text = "Level: " + PlayerPrefs.GetInt(LEVEL_KEY).ToString();
        ExpText.text = "Exp: " + PlayerPrefs.GetInt(EXP_KEY).ToString();
    }

    public void SaveData()
    {
        SavePlayerData();
    }

    private void Update()
    {
        UpdateUI();
    }
}
