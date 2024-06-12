using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoadNPC : MonoBehaviour
{
    [SerializeField]
    private int Id;

    public TMP_Text info;
    private string[] type = new string[3] { "Short", "Medium", "Long" };
    private int[] minLength = new int[4] { 5, 15, 45, 45 * 3 };

    public string Type;
    public int Length;

    LayerMask layerMask;

    private void Start()
    {
        layerMask = 1 << 2;
        layerMask = ~layerMask;
        info = GetComponentInChildren<TMP_Text>();
        info.text = "";
        getInfo();
    }

    public void getInfo()
    {
        int ran = UnityEngine.Random.Range(0, 11);

        int index;

        if (ran < 5) index = 0;
        else if (ran < 9) index = 1;
        else index = 2;

        Type = type[index];

        Length = UnityEngine.Random.Range(minLength[index], minLength[index + 1] - minLength[index]);
    }

    public void ShowInfo()
    {
        info.text += "Information:\nConversation type require: " + Type + '\n' + "Time require: " + Length.ToString() + " seconds";
    }

    public void HideInfo()
    {
        info.text = "";
    }

}
