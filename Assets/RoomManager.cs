using System.Collections;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject room0;
    public GameObject room1;
    public GameObject room2;
    public GameObject room3;
    public GameObject door1;
    public GameObject door2;
    public GameObject door3;
    public GameObject door4;
    public Interaction inter;

    private void Start()
    {
        InitializeRooms(PlayerPrefs.GetInt("level", 0));
        inter = FindObjectOfType<Interaction>();
    }

    private void InitializeRooms(int x)
    {
        if (x > 0)
        {
            ActivateRoom(room1);
            DeactivateRoom(door1);
        }
        else
        {
            DeactivateRoom(room1);
        }

        if(x > 1)
        {
            ActivateRoom(room2);
            DeactivateRoom(door2);
        }
        else
        {
            DeactivateRoom(room2);
        }

        if(x > 2)
        {
            ActivateRoom(room3);
            DeactivateRoom(door3);
            DeactivateRoom(door4);
        }
        else
        {
            DeactivateRoom(room3);
        }
    }

    private void DeactivateRoom(GameObject room)
    {
        room.SetActive(false);
        DeactivateChildren(room);
    }

    private void ActivateRoom(GameObject room)
    {
        room.SetActive(true);
        ActivateChildren(room);
    }

    private void DeactivateChildren(GameObject room)
    {
        foreach (Transform child in room.transform)
        {
            Demo_npc npc = child.GetComponent<Demo_npc>();
            if (npc != null)
            {
                npc.DisableNPC();
            }
        }
    }

    private void ActivateChildren(GameObject room)
    {
        foreach (Transform child in room.transform)
        {
            Demo_npc npc = child.GetComponent<Demo_npc>();
            if (npc != null)
            {
                npc.EnableNPC();
            }
        }
    }

    private void DisableNPCsInOtherRooms(GameObject currentRoom)
    {
        foreach (Transform room in transform)
        {
            if (room.gameObject != currentRoom)
            {
                foreach (Transform child in room)
                {
                    Demo_npc npc = child.GetComponent<Demo_npc>();
                    if (npc != null)
                    {
                        npc.DisableNPC();
                    }
                }
            }
        }
    }

    private void Update()
    {
        int level = inter.exp / 1200;
        if (level >= 1)
        {
            DisableNPCsInOtherRooms(room1);
            ActivateRoom(room1);
            door1.SetActive(false);
        }

        if (level >= 2)
        {
            DisableNPCsInOtherRooms(room2);
            ActivateRoom(room2);
            door2.SetActive(false);
        }

        if (level >= 3)
        {
            DisableNPCsInOtherRooms(room3);
            ActivateRoom(room3);
            door3.SetActive(false);
            door4.SetActive(false);
        }
    }
}
