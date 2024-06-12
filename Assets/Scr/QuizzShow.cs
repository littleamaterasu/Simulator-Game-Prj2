using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizzShow : MonoBehaviour
{
    [SerializeField]
    TMP_Text First;

    [SerializeField]
    TMP_Text Second;

    [SerializeField]
    TMP_Text Factor;

    public void ShowQuizz(int first, int second, char factor)
    {
        First.text = first.ToString();
        Second.text = second.ToString();
        Factor.text = factor.ToString();
    }
}
