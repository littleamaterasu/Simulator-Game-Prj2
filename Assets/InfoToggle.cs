using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoToggle : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] text = new TMP_Text[3];
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Toggle);
    }

    void Toggle()
    {
        foreach(TMP_Text tMP_Text in text)
        {
            tMP_Text.enabled = !tMP_Text.enabled;
        }
    }
}
