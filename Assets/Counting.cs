using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure you have TextMeshPro package imported

public class Counting : MonoBehaviour
{
    [SerializeField]
    string itemName;

    [SerializeField]
    TMP_Text textComponent; // Add a TMP_Text field for TextMeshPro

    void Start()
    {
        // Update the text with the value from PlayerPrefs
        textComponent.text = PlayerPrefs.GetInt(itemName, 0).ToString();
    }

    void Update()
    {
        // Optionally, update the text continuously if the value can change during gameplay
        textComponent.text = PlayerPrefs.GetInt(itemName, 0).ToString();
    }
}
