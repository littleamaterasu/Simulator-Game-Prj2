using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Commands : MonoBehaviour
{
    [SerializeField]
    public TMP_Text tmp;

    private void Start()
    {
        tmp.text = "";
    }

    public string Get()
    {
        return tmp.text;
    }

    void Update()
    {
        // Kiểm tra nếu có kí tự được nhập vào từ bàn phím
        if (Input.anyKeyDown)
        {
            // Lấy kí tự được nhập gần nhất
            char inputChar = Input.inputString[0];

            // Kiểm tra nếu kí tự không phải là kí tự không hiển thị (không in được)
            if (!char.IsControl(inputChar) && !char.IsWhiteSpace(inputChar))
            {
                // Kiểm tra nếu kí tự không phải là kí tự in thường hoặc in hoa không hiển thị
                if (!char.IsPunctuation(inputChar) && !char.IsSymbol(inputChar))
                {
                    // Xử lý nếu là kí tự backspace
                    if (inputChar == '\b' && tmp.text.Length > 0)
                    {
                        // Xóa kí tự cuối cùng trong TMP_Text
                        tmp.text = tmp.text.Substring(0, tmp.text.Length - 1);
                    }
                    // Nếu không phải là kí tự backspace
                    else
                    {
                        // Thêm kí tự vào trong TMP_Text
                        tmp.text += inputChar;
                    }
                }
            }
        }
    }
}
