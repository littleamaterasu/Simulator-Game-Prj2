using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class playing_maingame : MonoBehaviour
{
    Interaction interaction;
    public Task task;
    public int cnt = 1;
    int range = 0;
    Ray ray;
    RaycastHit hit;
    public TMP_Text text;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    IEnumerator<WaitForSeconds> _Init()
    {
        yield return new WaitForSeconds(Random.Range(4, 5));
        range = UnityEngine.Random.Range(18, 20);
        task.Init(range);
    }

    void Start()
    {
        interaction = FindAnyObjectByType<Interaction>();
        task = ScriptableObject.CreateInstance<Task>();
        StartCoroutine(_Init());
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
        while (!interaction.isSwitch)
        {
            yield return null; // Chờ một frame
        }

        // Sau khi isMeeting là true, thực hiện task.ChangeTask1()
        task.ChangeTask1();
        interaction.isSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Your Exp: " + interaction.exp.ToString();
        text2.text = "You have: " + interaction.item.ToString() + " item(s)";
        text3.text = "You are at level: " + (interaction.exp / 1200).ToString();
        if (task.Todo.Count > 0)
        {
            text4.text = "Current task require: " + task.Todo[0].require.ToString();
        }
        else text4.text = "";
        
        text5.text = "";
        if (interaction.Notice.Count > 0 && interaction.Notice[0] != null)
        {
            text5.text += interaction.Notice[0];
        }
        if (interaction.Notice.Count > 1 && interaction.Notice[1] != null)
        {
            text5.text += '\n';
            text5.text += interaction.Notice[1];
        }
        task.CheckEnd();
        if(task.done)
        {
            task = ScriptableObject.CreateInstance<Task>();
            interaction.Notice.Add("Complete " + cnt++.ToString() + " task(s)");
            if(interaction.Notice.Count > 2)
            {
                interaction.Notice.RemoveAt(0);
            }
            interaction.exp += 15 * range;
            interaction.Notice.Add("Gain " + (15 * range).ToString() + " exp");
            if (interaction.Notice.Count > 2)
            {
                interaction.Notice.RemoveAt(0);
            }
            StartCoroutine(_Init());
        }
        task.State();
        ExcuteTask();
    }
}
