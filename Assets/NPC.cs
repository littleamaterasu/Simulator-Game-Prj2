using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField]
    public int ID;
    public int book = 0;
    public int task = 0;
    public int paper = 0;

    private const string BOOK_KEY = "NPC_Book_";
    private const string TASK_KEY = "NPC_Task_";
    private const string PAPER_KEY = "NPC_Paper_";

    // Start is called before the first frame update
    void Start()
    {
        
        LoadNPCData();
        
    }

    public void InitializeNPCRequirements()
    {
        int level = PlayerPrefs.GetInt("Level", 0) + 1;

        switch (ID)
        {
            case 1:
                task = paper = book = Random.Range(1, level + 1);
                break;
            case 2:
                book = Random.Range(1, level + 1);
                break;
            case 3:
                task = Random.Range(1, level + 1);
                break;
            default:
                paper = Random.Range(1, level + 1);
                break;
        }
    }

    private bool NPCDataExists()
    {
        return PlayerPrefs.HasKey(BOOK_KEY + ID) && PlayerPrefs.HasKey(TASK_KEY + ID) && PlayerPrefs.HasKey(PAPER_KEY + ID);
    }

    public void LoadNPCData()
    {
        if (NPCDataExists())
        {
            book = PlayerPrefs.GetInt(BOOK_KEY + ID);
            task = PlayerPrefs.GetInt(TASK_KEY + ID);
            paper = PlayerPrefs.GetInt(PAPER_KEY + ID);
            Debug.Log("NPC data loaded.");
        }
        else
        {
            InitializeNPCRequirements();
            SaveNPCData();
            Debug.Log("NPC data initialized.");
        }
    }

    public void SaveNPCData()
    {
        PlayerPrefs.SetInt(BOOK_KEY + ID, book);
        PlayerPrefs.SetInt(TASK_KEY + ID, task);
        PlayerPrefs.SetInt(PAPER_KEY + ID, paper);
        PlayerPrefs.Save();
        Debug.Log("NPC data saved.");
    }

    public bool Check()
    {
        int playerBook = PlayerPrefs.GetInt("book", 0);
        int playerTask = PlayerPrefs.GetInt("task", 0);
        int playerPaper = PlayerPrefs.GetInt("paper", 0);

        return book <= playerBook && task <= playerTask && paper <= playerPaper;
    }

    public void GainRequirement()
    {
        // Check if the player has enough resources
        if (Check())
        {
            // Subtract the requirements from the player's resources
            PlayerPrefs.SetInt("book", PlayerPrefs.GetInt("book", 0) - book);
            PlayerPrefs.SetInt("task", PlayerPrefs.GetInt("task", 0) - task);
            PlayerPrefs.SetInt("paper", PlayerPrefs.GetInt("paper", 0) - paper);
            PlayerPrefs.SetInt("Exp", PlayerPrefs.GetInt("Exp", 0) + Random.Range(100, 300));
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Exp", 0) / 600);
        }

    }

    void Update()
    {
        // Update logic here if needed
    }
}
