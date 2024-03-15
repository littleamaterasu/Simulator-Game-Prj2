using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTask", menuName = "Task")]
public class Task : ScriptableObject
{
    public string taskName = "task";
    public bool isOccupied = false;
    public List<Task1> Todo = new List<Task1>();
    public int length;
    public bool done;

    public void Init(int length)
    {
        List<Task1> list = new(FindObjectsOfType<Task1>());
        this.length = length;
        Todo.Clear(); // Clear existing tasks before adding new ones
        for (int i = 0; i < length; i++)
        {
            int t = Random.Range(0, list.Count);
            Todo.Add(list[t]);
            list.RemoveAt(t);
        }
    }

    public void ChangeTask1()
    {
        if (Todo.Count > 0)
        {
            Todo[0].Origin();
            Todo.RemoveAt(0);
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
        if (Todo.Count == 0)
        {
            done = true;
        }
    }
}
