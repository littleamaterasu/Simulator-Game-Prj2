using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playing_maingame : MonoBehaviour
{
    Interaction interaction;
    public Task task;
    public int cnt = 1;
    Ray ray;
    RaycastHit hit;
    public TMP_Text text;
    int exp = 0;

    void Start()
    {
        text.text = "Your Exp: " + exp.ToString();
        interaction = FindAnyObjectByType<Interaction>();
        task = ScriptableObject.CreateInstance<Task>();
        task.Init(Random.Range(3, 5));
    }

    void ExcuteTask()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Task1 clickedTask = hit.collider.GetComponent<Task1>();

                if (clickedTask != null)
                {
                    StartCoroutine(ExecuteTaskWithMeetingCheck());
                }
            }
        }
    }

    IEnumerator ExecuteTaskWithMeetingCheck()
    {
        // Kiểm tra xem liệu interaction đã đạt được meeting chưa
        while (interaction.isMeeting)
        {
            yield return null; // Chờ một frame
        }

        // Sau khi isMeeting là true, thực hiện task.ChangeTask1()
        task.ChangeTask1();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Your Exp: " + exp.ToString();
        task.CheckEnd();
        if(task.done)
        {
            task = ScriptableObject.CreateInstance<Task>();
            int range = Random.Range(3, 5);
            task.Init(range);
            Debug.Log("Complete " + cnt++ + " task(s)");
            exp += 200 * range;
        }
        task.State();
        ExcuteTask();
    }
}
