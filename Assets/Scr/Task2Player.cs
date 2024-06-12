using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task2Player : MonoBehaviour
{
    [SerializeField]
    Task2Manager taskManager;
    RoadNPC npc = null;

    Camera main;
    Vector3 basePos;
    float baseOrtho;

    [SerializeField]
    Image image;

    [SerializeField]
    Button MainGame;

    bool onTask = false;
    bool onZoom = false;

    private void Start()
    {
        MainGame.onClick.AddListener(ReturnToMainGame);
        main = Camera.main;
        basePos = main.transform.position;
        baseOrtho = main.orthographicSize;
    }

    void ReturnToMainGame()
    {
        // Thực hiện hành động khi người dùng chọn quay lại trang chính, ví dụ như load lại scene Main Game
        // Dưới đây là một ví dụ cách load lại scene chính
        UnityEngine.SceneManagement.SceneManager.LoadScene("maingame");
    }

    void Interact()
    {
        if (npc != null)
        {
            switch (npc.Type)
            {
                case "Short":
                    ++taskManager.ShortFinished;
                    break;
                case "Medium":
                    ++taskManager.MediumFinished;
                    break;
                case "Long":
                    ++taskManager.LongFinished;
                    break;
            }

            npc.getInfo();
        }
    }
    
    IEnumerator ZoomIn(int seconds, Vector3 npcPos, float zoomSize)
    {
        onZoom = true;
        Vector3 newPos = npcPos + new Vector3 (-10, 10, -10);
        Vector3 dir = newPos - main.transform.position;
        float time = 0;
        float baseZoom = main.orthographicSize;
        while (time < seconds)
        {
            time += Time.deltaTime;
            main.orthographicSize -= (baseZoom - zoomSize) * Time.deltaTime / seconds;
            main.transform.position += dir * Time.deltaTime / seconds;
            yield return null;
        }
        main.orthographicSize = zoomSize;
        onZoom = false;
    }

    IEnumerator ZoomOut(int seconds)
    {
        onZoom = true;
        Vector3 newPos = main.transform.position;
        Vector3 dir = newPos - basePos;
        float time = 0;
        float baseZoom = main.orthographicSize;
        while (time < seconds)
        {
            time += Time.deltaTime;
            main.orthographicSize += (baseOrtho - baseZoom) * Time.deltaTime / seconds;
            main.transform.position -= dir * Time.deltaTime / seconds;
            yield return null;
        }
        main.orthographicSize = baseOrtho;
        main.transform.position = basePos;
        onZoom = false;
    }
    
    IEnumerator WaitForSeconds(int seconds)
    {
        onTask = true;
        float time = 0f;
        taskManager.ShowPicture(seconds);
        while (time < seconds)
        {
            time += Time.deltaTime;
            image.transform.localScale = new Vector3(time / seconds, 1f, 1f);
            yield return null;
        }

        image.transform.localScale = Vector3.zero;
        Interact();
        onTask = false;
    }

    void Update()
    {
        
        // Nếu người chơi đang di chuyển chuột qua một đối tượng có component RoadNPC
        // và đối tượng đó không phải là đối tượng hiện tại, gọi hàm ShowInfo của RoadNPC đó
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            RoadNPC hitNPC = hit.collider.GetComponent<RoadNPC>();
            if(hitNPC != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!onZoom) StartCoroutine(ZoomIn(1, hitNPC.transform.position, 3));
                    if (!onTask) StartCoroutine(WaitForSeconds(hitNPC.Length));
                    
                }
                else if (Input.GetMouseButtonDown(1) && !onZoom)
                {
                    StartCoroutine(ZoomOut(1));
                }
                if (hitNPC != npc)
                {
                    if (npc != null)
                    {
                        npc.HideInfo();
                    }
                    npc = hitNPC;
                    npc.ShowInfo();

                }
            }
            
        }
        else
        {
            if (npc != null)
            {
                npc.HideInfo();
                npc = null;
            }
        }
    }
}
