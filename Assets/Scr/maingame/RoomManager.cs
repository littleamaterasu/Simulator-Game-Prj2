using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    List<Vector3> pos = new List<Vector3>();

    [SerializeField]
    List<Button> buttons = new List<Button>();

    [SerializeField]
    TMP_Text noti;

    private bool isTransitioning = false;
    private Vector3 targetPosition;
    private float transitionSpeed = 2.0f; // Adjust the speed as needed

    void Start()
    {
        noti.text = "";
        // Add listeners to buttons
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i; // Local copy of the loop variable
            buttons[i].onClick.AddListener(() => MoveCameraToPosition(index));
        }
    }

    void MoveCameraToPosition(int index)
    {
        if(PlayerPrefs.GetInt("Level", 0) < index)
        {
            StartCoroutine(ShowNotification(index));
            return;
        }
        if (!isTransitioning)
        {
            targetPosition = pos[index];
            StartCoroutine(TransitionToPosition());
        }
    }

    IEnumerator ShowNotification(int index)
    {
        noti.text = "You need at least level " + index.ToString() + " to unlock this room!";
        yield return new WaitForSeconds(3);
        noti.text = "";
    }
        
    IEnumerator TransitionToPosition()
    {
        isTransitioning = true;
        Vector3 startPosition = Camera.main.transform.position;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * transitionSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        Camera.main.transform.position = targetPosition;
        isTransitioning = false;
    }

    void Update()
    {
        // Update logic if necessary
    }
}
