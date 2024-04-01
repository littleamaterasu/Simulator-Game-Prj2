using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public float letterDelay = 0.1f; // Thời gian trễ giữa mỗi ký tự
    public float dialogueDelay = 2f; // Thời gian trễ giữa các đoạn hội thoại

    public string[] dialogue;
    private int currentDialogueIndex;

    private Coroutine typingCoroutine;

    private void Start()
    {
        
    }

    public void StartDialogue(string[] dialogueLines)
    {
        dialogue = dialogueLines;
        currentDialogueIndex = 0;
        dialoguePanel.SetActive(true);
        ShowDialogue(dialogue[currentDialogueIndex]);
    }

    void ShowDialogue(string text)
    {
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(AnimateText(text));
    }

    IEnumerator AnimateText(string text)
    {
        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(dialogueDelay); // Chờ một khoảng thời gian trước khi hiển thị đoạn hội thoại tiếp theo

        NextDialogue();
    }

    void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < dialogue.Length)
        {
            ShowDialogue(dialogue[currentDialogueIndex]);
        }
        else
        {
            HideDialogue();
        }
    }

    void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
