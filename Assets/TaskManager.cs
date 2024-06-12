using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TaskManager : MonoBehaviour
{
    public GameObject panel; // The panel to display NPC info
    public Button toggleButton; // The button to toggle the panel visibility
    public GameObject textPrefab; // The prefab for TMP_Text elements
    public float lineHeight = 0.5f; // Spacing between each line

    private List<NPC> npcs; // List to store all NPCs
    private List<GameObject> infoObjects; // List to store created information objects

    void Start()
    {
        // Initialize the NPC list
        npcs = new List<NPC>(FindObjectsOfType<NPC>());

        // Initialize the info objects list
        infoObjects = new List<GameObject>();

        // Set up the button click event
        toggleButton.onClick.AddListener(TogglePanel);

        // Initially hide the panel
        panel.SetActive(false);

        // Create and populate the information objects
        CreateNPCInfoObjects();
    }

    void TogglePanel()
    {
        // Toggle the panel's active state
        panel.SetActive(!panel.activeSelf);
        // Toggle the prefab's active state
        textPrefab.SetActive(!textPrefab.activeSelf);
    }

    void CreateNPCInfoObjects()
    {
        float currentY = 0f; // Track current Y position
        foreach (NPC npc in npcs)
        {
            // Create a new GameObject to hold NPC information
            GameObject newInfoObject = new GameObject("NPCInfo", typeof(RectTransform));
            newInfoObject.transform.SetParent(panel.transform, false);
            RectTransform infoTransform = newInfoObject.GetComponent<RectTransform>();

            // Set Y position
            infoTransform.anchoredPosition = new Vector2(-270f, currentY);

            // Create and configure TMP_Text component for all NPC information
            CreateTextObject(newInfoObject.transform, $"Name: {npc.ID} - Book: {npc.book}, Task: {npc.task}, Paper: {npc.paper}", ref currentY);

            // Add the new info object to the list
            infoObjects.Add(newInfoObject);
        }
    }

    void CreateTextObject(Transform parent, string description, ref float currentY)
    {
        // Create and configure TMP_Text component
        TMP_Text text = Instantiate(textPrefab, parent).GetComponent<TMP_Text>();
        text.text = description;
        text.rectTransform.anchoredPosition = new Vector2(0f, currentY); // Set text position within the parent

        currentY -= lineHeight; // Update Y position for next item
    }
}
