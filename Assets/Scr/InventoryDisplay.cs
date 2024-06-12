using UnityEngine;
using TMPro; // Namespace for TextMeshPro

public class InventoryDisplay : MonoBehaviour
{
    public TextMeshProUGUI itemXText; // TMP component for ItemX

    void Update()
    {
        UpdateItemCount();
    }

    void UpdateItemCount()
    {
        float itemCount = PlayerPrefs.GetFloat("ItemX", 0);
        itemXText.text =  itemCount.ToString();
    }
}