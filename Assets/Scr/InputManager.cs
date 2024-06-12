using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputField;
    string input;

    // Start is called before the first frame update
    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(delegate { SetInput(inputField.text); ClearInputField(); });
    }

    void SetInput(string input)
    {
        this.input = input;
    }

    void ClearInputField()
    {
        inputField.text = "";
    }

    public string GetInput()
    {
        return input;
    }

    public void Deactivate()
    {
        inputField.DeactivateInputField();
    }

    public void Activate()
    {
        inputField.ActivateInputField();
    }

    
}
