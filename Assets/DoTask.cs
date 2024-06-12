using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCSelector : MonoBehaviour
{
    private NPC selectedNPC;

    public Camera main;
    private bool onZoom = false;
    private Vector3 basePos;
    float baseOrtho;

    public GameObject taskUIPanel; // Panel hiển thị nút Do Task và Cancel
    public Button doTaskButton;
    public Button cancelButton;
    public TMP_Text taskInfo;

    private void Start()
    {
        taskUIPanel.SetActive(false);
        basePos = main.transform.position;
        baseOrtho = main.orthographicSize;
        doTaskButton.onClick.AddListener(DoTask);
        cancelButton.onClick.AddListener(CancelTask);
    }

    public void CancelTask()
    {
        taskUIPanel.SetActive(false); // Ẩn panel
        selectedNPC = null;
    }

    public void ShowTaskUI()
    {
        taskUIPanel.SetActive(true); // Hiển thị panel nút Do Task và Cancel
    }

    public void DoTask()
    {
        if (selectedNPC.Check())
        {
            selectedNPC.GainRequirement();
            selectedNPC.InitializeNPCRequirements();
            
        }
        else
        {
            // Tính toán số lượng item còn thiếu
            int playerBook = PlayerPrefs.GetInt("book", 0);
            int playerTask = PlayerPrefs.GetInt("task", 0);
            int playerPaper = PlayerPrefs.GetInt("paper", 0);
            int bookNeeded = selectedNPC.book - playerBook;
            int taskNeeded = selectedNPC.task - playerTask;
            int paperNeeded = selectedNPC.paper - playerPaper;

            string message = "Player does not have enough resources to fulfill the NPC's requirements.";
            message += "\nItems needed:";
            if (bookNeeded > 0) message += "\n- Book: " + bookNeeded;
            if (taskNeeded > 0) message += "\n- Task: " + taskNeeded;
            if (paperNeeded > 0) message += "\n- Paper: " + paperNeeded;

            Debug.LogWarning(message);
        }

        taskUIPanel.SetActive(false);
        selectedNPC = null;
    }

    IEnumerator ZoomIn(int seconds, Vector3 npcPos, float zoomSize)
    {
        onZoom = true;
        Vector3 newPos = npcPos + new Vector3(-10, 10, -10);
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

    void Update()
    {
        // Kiểm tra xem người chơi nhấn chuột hoặc chạm vào màn hình
        if (Input.GetMouseButtonDown(0))
        {
            // Tạo một Raycast từ vị trí của người chơi
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Kiểm tra xem Raycast có chạm vào một đối tượng không
            if (Physics.Raycast(ray, out hit))
            {
                // Lấy đối tượng mà Raycast chạm vào
                GameObject selectedObject = hit.collider.gameObject;

                // Kiểm tra xem đối tượng đó có phải là một NPC không
                NPC npc = selectedObject.GetComponent<NPC>();
                if (npc != null)
                {
                    if (!onZoom)
                    {
                        StartCoroutine(ZoomIn(1, npc.transform.position, 3));
                    }
                    selectedNPC = npc;
                    taskUIPanel.SetActive(true);
                    taskInfo.text = "Book: " + selectedNPC.book + "\n";
                    taskInfo.text += "Task: " + selectedNPC.task + "\n";
                    taskInfo.text += "Paper: " + selectedNPC.paper + "\n";
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && !onZoom)
        {
            StartCoroutine(ZoomOut(1));
        }
    }
}
