using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playing_maingame : MonoBehaviour
{
    public Task task;
    public int cnt = 1;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        task = ScriptableObject.CreateInstance<Task>();
        task.Init(Random.Range(3, 5));
    }

    void ExcuteTask()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                Task1 clickedTask = hit.collider.GetComponent<Task1>();
                if(clickedTask != null)
                {
                    task.ChangeTask1();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        task.CheckEnd();
        if(task.done)
        {
            task = ScriptableObject.CreateInstance<Task>();
            task.Init(Random.Range(3, 5));
            Debug.Log("Complete " + cnt++ + " task(s)");
        }
        task.State();
        ExcuteTask();
    }
}
