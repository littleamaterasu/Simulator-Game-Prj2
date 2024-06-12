using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizzGen : MonoBehaviour
{
    private int first;
    private int second;
    private char factor;
    private int level;
    private int actualAns;

    [SerializeField]
    TMP_Text ans;

    private char[] factors = { '+', '-' };

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Exp", 0) / 600 + 1;
        this.level = Math.Min(4, level);
    }

    public int GetFirst()
    {
        return first;
    }

    public int GetSecond()
    {
        return second;
    }

    public char GetFactor()
    {
        return factor;
    }

    public void GenerateQuizz()
    {
        this.level = Math.Min(4, level);
        // Gen first, second
        int max =  (int) Math.Pow(10, level);

        first = UnityEngine.Random.Range(0, max);
        second = UnityEngine.Random.Range(0, max - first);

        // Gen factor
        int ran = UnityEngine.Random.Range(0, 2);

        factor = factors[ran];

        switch(ran)
        {
            case 0:
                actualAns = first + second;
                break;
            case 1:
                actualAns = first - second;
                break;
        }

        ans.text = actualAns.ToString();
    }

    public bool CheckQuizz(string ans)
    {
        if(ans == actualAns.ToString()) return true;
        return false;
    }

    public int Point()
    {
        return level * 10;
    }
}
