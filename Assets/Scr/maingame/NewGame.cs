using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InternExperience : MonoBehaviour
{
    [SerializeField]
    TMP_Text dialogueText; // Reference to the TMP_Text component

    [SerializeField]
    Image bg;

    [SerializeField]
    float typingSpeed = 0.05f; // Speed of typing effect

    private string[] fullTextArray = new string[]
    {
        "As an intern at the company, I have a few tasks to help me get acquainted with my new environment.",
        "The first task assigned to me is to familiarize myself with the resources available, starting with finding and using books.",
        "When I arrived on my first day, my supervisor greeted me warmly and introduced me to the team.",
        "The library is a treasure trove of knowledge, stocked with books covering a wide range of topics relevant to our field.",
        "My task was to find specific books related to our current project and make notes on key concepts and ideas.",
        "Navigating the library was an adventure. The shelves were organized by category, making it easier to find the materials I needed.",
        "I started with technical manuals essential for understanding the tools and technologies we use.",
        "One of the most helpful books I found was a comprehensive guide on project management, offering practical tips on managing timelines and resources effectively.",
        "This initial task of exploring the library and using its resources has given me a solid foundation of knowledge.",
        "I am eager to take on more challenges and continue my growth here at the company."
    };

    private Coroutine typingCoroutine;
    private bool isTyping = false;
    private int currentTextIndex = 0;

    void Start()
    {
        if(!checkFirstTime())
        {
            HideText();
        }
        if (fullTextArray.Length > 0)
        {
            StartTyping();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopTyping();
                ShowFullText();
            }
            else
            {
                if (currentTextIndex < fullTextArray.Length - 1)
                {
                    currentTextIndex++;
                    StartTyping();
                }
                else
                {
                    HideText();
                    PlayerPrefs.SetInt("Time", 1);
                }
            }
        }
    }

    void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(fullTextArray[currentTextIndex]));
    }

    IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in fullText)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        isTyping = false;
    }

    void ShowFullText()
    {
        dialogueText.text = fullTextArray[currentTextIndex];
    }

    bool checkFirstTime()
    {
        if(PlayerPrefs.GetInt("Time", 0) == 0)
        {
            return true;
        }
        return false;
    }

    void HideText()
    {
        dialogueText.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }
}
