using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewTask", menuName = "Task")]
public class Task : ScriptableObject
{
    public string taskName = "task";
    public bool isOccupied = false;
    public List<Task1> Todo = new();
    public int length;
    public bool done;
    public bool isInit = false;

    public void Init(int length)
    {
        List<Task1> list = new(FindObjectsOfType<Task1>());
        this.length = Math.Min(length, list.Count);
        Todo.Clear(); // Clear existing tasks before adding new ones
        for (int i = 0; i < this.length; i++)
        {
            int t = UnityEngine.Random.Range(0, list.Count - 1);
            Todo.Add(list[t]);

            list[t].require = UnityEngine.Random.Range(1, 5);
            list.RemoveAt(t);
        }
        Todo[0].inTask = true;
        isInit = true;
        done = false;
    }

    public void ChangeTask1()
    {
        if (Todo.Count > 0)
        {
            Todo[0].Origin();

            // Loại nhiệm vụ khỏi danh sách
            Todo[0].inTask = false;
            Todo.RemoveAt(0);
            if (Todo.Count != 0)
            {
                Todo[0].inTask = true;
            }

            CheckEnd();
        }
    }

    public void State()
    {
        if (Todo.Count > 0)
        {
            Todo[0].Highlight();
        }
    }

    public void CheckEnd()
    {
        if (isInit == false) return;
        if (Todo.Count == 0)
        {
            isInit = false;
            done = true;
        }
    }
}
