using UnityEngine;
using UnityEngine.UI;

public class SaveAndQuitButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        // Gắn sự kiện khi nút được nhấn
        button.onClick.AddListener(SaveAndQuit);
    }

    void SaveAndQuit()
    {
        // Lưu dữ liệu của tất cả NPC và Player
        SaveAllNPCData();
        SavePlayerData();

        // Tắt game
        Application.Quit();
    }

    void SaveAllNPCData()
    {
        NPC[] npcs = FindObjectsOfType<NPC>();
        foreach (NPC npc in npcs)
        {
            npc.SaveNPCData();
        }
    }

    void SavePlayerData()
    {
        FindObjectOfType<Player>().SaveData();
    }
}
