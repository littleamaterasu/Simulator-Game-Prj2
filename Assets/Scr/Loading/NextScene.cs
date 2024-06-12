using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Import the TextMeshPro namespace

public class NextScene : MonoBehaviour
{
    [SerializeField]
    TMP_Text loadingText; // Reference to the TMP_Text component

    private float dotInterval = 0.5f; // Time interval for adding dots
    private int dotCount = 0; // Current number of dots
    private string baseText = "Now"; // Base loading text

    void Start()
    {
        StartCoroutine(ShowLoadingText());
        loadingText.text = baseText;
    }

    IEnumerator ShowLoadingText()
    {
        yield return new WaitForSeconds(dotInterval);
        loadingText.text += " loading ";
        dotCount = 1;
        yield return new WaitForSeconds(dotInterval);
        while (true)
        {
            // Update the loading text
            loadingText.text += ".";

            // Increment the dot count, reset if it exceeds 3
            dotCount = (dotCount + 1) % 4;

            // Wait for the next interval
            yield return new WaitForSeconds(dotInterval);

            // Check if we should load the next scene
            if (dotCount == 0)
            {
                // Load the next scene from PlayerPrefs
                string nextSceneName = PlayerPrefs.GetString("nextScene", "Loading");
                SceneManager.LoadScene(nextSceneName);
                yield break; // Exit the coroutine
            }
        }
    }
}
