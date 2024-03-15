using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Minigame_1 : MonoBehaviour
{
    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> gameObjects = new List<GameObject>();
    public Material material;
    public Material origin;
    public bool isEnded = false;
    public Playing p;
    private float elapsedTime = 0.0f; // Thời gian đã trôi qua
    public TMP_Text timeText; // Đối tượng Text để hiển thị thời gian

    void Start()
    {
        AssignPos();
    }

    void Update()
    {
        if (isEnded) return;

        // Cập nhật thời gian đã trôi qua
        if (p.doneShuffle)
        {
            elapsedTime += Time.deltaTime;
        }

        // Hiển thị thời gian đã trôi qua
        UpdateTimeDisplay();

        // Kiểm tra kết thúc trò chơi
        if (!CheckGameEnd())
        {
            // Xử lý khi trò chơi kết thúc
            isEnded = true;
            Debug.Log("Game Over");
        }
    }

    // Kiểm tra điều kiện kết thúc trò chơi
    bool CheckGameEnd()
    {
        bool res = false;
        foreach (GameObject g in gameObjects)
        {
            Position p = g.GetComponent<Position>();
            if (p == null)
            {
                Debug.LogError("Position component is missing on game object: " + g.name);
                return false;
            }

            int t = p.pos;
            if (t >= transforms.Count || t < 0 || transforms[t] == null)
            {
                Debug.LogError("Invalid transform index for game object: " + g.name);
                return false;
            }

            float distance = Vector3.Distance(g.transform.position, transforms[t].position);
            if (distance >= 0.01f) // Điều chỉnh ngưỡng tùy thuộc vào độ chính xác mong muốn
            {
                // Không thỏa mãn điều kiện, sử dụng vật liệu ban đầu
                SetMaterial(g, origin);
            }
            else
            {
                // Thỏa mãn điều kiện, sử dụng vật liệu mới
                SetMaterial(g, material);
                res = true;
            }
        }
        return res;
    }

    // Cập nhật hiển thị thời gian đã trôi qua
    void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + FormatTime(elapsedTime); // Hiển thị thời gian dưới dạng chuỗi định dạng
        }
    }

    // Hàm định dạng thời gian thành chuỗi "HH:MM:SS"
    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        int milliseconds = Mathf.FloorToInt((timeInSeconds * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    // Gán giá trị pos cho các đối tượng sau khi xáo trộn
    public void AssignPos()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (gameObjects[i].TryGetComponent<Position>(out var positionComponent))
            {
                positionComponent.pos = i;
            }
            else
            {
                Debug.LogError("Position component is missing on game object: " + gameObjects[i].name);
            }
        }
    }

    // Thiết lập vật liệu của một đối tượng
    void SetMaterial(GameObject obj, Material mat)
    {
        if (obj.TryGetComponent<MeshRenderer>(out var mr))
        {
            mr.material = mat;
        }
        else
        {
            Debug.LogError("MeshRenderer component is missing on game object: " + obj.name);
        }
    }
}
