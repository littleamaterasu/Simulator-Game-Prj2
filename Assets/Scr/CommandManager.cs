using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class CommandManager : MonoBehaviour
{
    enum myEnum
    {
        Item1,
        Item2,
        Item3,
        Item4,
        Item5,
        Item6
    };
    [SerializeField]
    private myEnum myEnum1;
    [SerializeField]
    private Commands commands;
    [SerializeField]
    private TMP_Text notification;
    [SerializeField]
    private TMP_Text exp;
    [SerializeField]
    private TMP_Text items;

    private string[] doc = {
        "player.giveItem(<item name>, <amount>): give player <amount> of <item name>(s)",
        "player.giveExp(<amount, maximum is 600>): give player <amount> of Exp (maximum is 600)",
        "item.list: show list of item names",
        "help: show list of commands"
    };
    private string[] list = { "Item1", "Item2", "Item3", "Item4", "Item5", "Item6" };

    void Start()
    {
        commands.enabled = false;
    }

    bool CheckInList(string item)
    {
        foreach (string listItem in list)
        {
            if (listItem.Equals(item))
                return true;
        }
        return false;
    }

    void GiveItem(string item, int amount)
    {
        int a = PlayerPrefs.GetInt(item, 0) + amount;
        PlayerPrefs.SetInt(item, a);
    }

    void GiveExperience(int amount)
    {
        int a = PlayerPrefs.GetInt("exp", 0) + amount;
        PlayerPrefs.SetInt("exp", a);
    }

    void ListItems()
    {
        string dis = "";
        foreach (string item in list)
        {
            dis += item + '\n';
        }
        SetNoti(dis);
    }

    void DisplayHelp()
    {
        string dis = "";
        foreach (string command in doc)
        {
            dis += command + '\n';
        }
        SetNoti(dis);
    }

    void SetNoti(string noti)
    {
        notification.text = "";
        notification.text = noti;
    }

    // Update is called once per frame
    void Update()
    {
        if (commands.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string tmp = commands.tmp.text.Trim(); // Trim to remove leading and trailing spaces

                if (tmp.StartsWith("player.giveItem"))
                {
                    // Parse item name and amount
                    string[] parts = tmp.Split(',');
                    if (parts.Length == 2)
                    {
                        string itemName = parts[0].Substring(16).Trim();
                        if (CheckInList(itemName))
                        {
                            int amount;
                            if (int.TryParse(parts[1].Trim().TrimEnd(')'), out amount))
                            {
                                // Call function to give item
                                GiveItem(itemName, amount);
                                SetNoti("Successfully gave " + amount + " of " + itemName + ".");
                            }
                            else
                            {
                                SetNoti("Invalid amount for giveItem command.");
                            }
                        }
                        else
                        {
                            SetNoti("Item not found in the list.");
                        }
                    }
                    else
                    {
                        SetNoti("Invalid syntax for giveItem command.");
                    }
                }
                else if (tmp.StartsWith("player.giveExp"))
                {
                    // Parse experience amount
                    int expAmount;
                    if (int.TryParse(tmp.Substring(14).TrimEnd(')'), out expAmount))
                    {
                        // Call function to give experience
                        GiveExperience(Math.Max(0, Math.Min(expAmount, 600)));
                        SetNoti("Successfully gave " + expAmount + " experience.");
                    }
                    else
                    {
                        SetNoti("Invalid amount for giveExp command.");
                    }
                }
                else if (tmp.Equals("item.list"))
                {
                    // Call function to list items
                    ListItems();
                }
                else if (tmp.Equals("help"))
                {
                    // Call function to display help
                    DisplayHelp();
                }
                else
                {
                    SetNoti("Unknown command.");
                }

                commands.tmp.text = "";
                commands.enabled = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                commands.enabled = true;
            }
        }

        //Display items
        items.text = "";
        //foreach(string item in list)
        //{
        //    items.text += item + ": " + PlayerPrefs.GetInt(item, 0).ToString() + '\n';
        //}
        items.text = myEnum1.ToString() + ": " + PlayerPrefs.GetInt(myEnum1.ToString(), 0).ToString() + '\n';

        //Display exp
        exp.text = "";
        exp.text += "Your EXP: " + PlayerPrefs.GetInt("exp", 0).ToString();
    }
}
