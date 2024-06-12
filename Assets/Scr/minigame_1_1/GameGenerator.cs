using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;
using DG.Tweening; // Include DOTween namespace
using UnityEngine.SceneManagement;

public class GameGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cubePrefabs; // Danh sách các prefab của cube
    [SerializeField]
    private int numberOfCubes; // Số lượng cube cần tạo
    [SerializeField]
    private Camera mainCamera; // Camera chính
    [SerializeField]
    private Button continueButton; // Nút "Tiếp tục"
    [SerializeField]
    private Button returnToMainButton; // Nút "Về Main Game"
    [SerializeField]
    private TMP_Text DebugLog;

    private List<GameObject> cubes = new();

    private GameObject selectedCube = null; // Cube đang được chọn

    void Start()
    {
        continueButton.gameObject.SetActive(false);
        DebugLog.gameObject.SetActive(false);

        // Khai báo các hàm lắng nghe sự kiện click cho các nút
        continueButton.onClick.AddListener(ContinueGame);
        returnToMainButton.onClick.AddListener(ReturnToMainGame);

        SpawnRandomCubes();
        PositionCamera();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Cube"))
                {
                    GameObject hitCube = hit.collider.gameObject;
                    if (selectedCube == null)
                    {
                        // Chọn cube nếu chưa có cube nào được chọn
                        selectedCube = hitCube;
                    }
                    else
                    {
                        // Đã chọn 2 cubes, đổi vị trí
                        SwapCubes(selectedCube, hitCube);
                        selectedCube = null; // Đặt lại cube đang được chọn
                    }
                }
            }
        }
    }

    void SwapCubes(GameObject cube1, GameObject cube2)
    {
        Vector3 position1 = cube1.transform.position;
        Vector3 position2 = cube2.transform.position;

        // Animate the swap using DOTween
        float duration = 0.5f; // Duration of the animation

        Sequence swapSequence = DOTween.Sequence();
        swapSequence.Append(cube1.transform.DOMove(position2, duration));
        swapSequence.Join(cube2.transform.DOMove(position1, duration));
        swapSequence.OnComplete(() =>
        {
            // This will be called once the animation is complete
            CheckCompletion(); // Check completion after the swap animation
        });

        // Disable further interactions until the animation is complete
        selectedCube = null;
    }

    void SpawnRandomCubes()
    {
        List<int> availableIndices = new List<int>(); // Danh sách các chỉ số có sẵn
        for (int i = 0; i < numberOfCubes; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < numberOfCubes; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count); // Chọn một chỉ số ngẫu nhiên từ danh sách các chỉ số có sẵn
            int selectedIndex = availableIndices[randomIndex]; // Lấy chỉ số được chọn
            availableIndices.RemoveAt(randomIndex); // Xóa chỉ số đã được chọn khỏi danh sách

            GameObject selectedPrefab = cubePrefabs;

            Vector3 spawnPosition = new Vector3(i * 0.16f, 0f, 1f); // Vị trí spawn của cube
            Quaternion spawnRotation = Quaternion.identity; // Hướng quay của cube

            GameObject newCube = Instantiate(selectedPrefab, spawnPosition, spawnRotation); // Tạo mới một cube
            newCube.tag = "Cube"; // Gắn tag "Cube" để xác định cube khi va chạm

            // Thêm collider nếu chưa có
            if (newCube.GetComponent<Collider>() == null)
            {
                newCube.AddComponent<BoxCollider>();
            }

            // Tính toán giá trị scale theo y
            float scaleY = 1.8f + selectedIndex / 4f;
            // Áp dụng scale vào cube
            newCube.transform.localScale = new Vector3(5f, scaleY, 1f);
            cubes.Add(newCube);
        }
    }

    void CheckCompletion()
    {
        bool isCompleted = true;

        for (int i = 0; i < cubes.Count - 1; i++)
        {
            if (Math.Abs((cubes[i].transform.localScale.y - 1.8f) * 4f * 0.16f - cubes[i].transform.position.x) > 0.01f)
            {
                isCompleted = false;
                break;
            }
        }

        if (isCompleted)
        {
            DebugLog.text = "You have completed the puzzle!";
            DebugLog.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("book", PlayerPrefs.GetInt("book", 0) + 1);
            // Thực hiện hành động khi hoàn thành, ví dụ như hiển thị thông báo chiến thắng
            ClearCubes();
        }
    }

    void ClearCubes()
    {
        foreach (GameObject cube in cubes)
        {
            Destroy(cube);
        }
        cubes.Clear();
    }

    void ContinueGame()
    {
        // Thực hiện hành động khi người dùng chọn tiếp tục, ví dụ như khởi động lại màn chơi
        continueButton.gameObject.SetActive(false);
        DebugLog.gameObject.SetActive(false);
        SpawnRandomCubes();
    }

    void ReturnToMainGame()
    {
        // Thực hiện hành động khi người dùng chọn quay lại trang chính, ví dụ như load lại scene Main Game
        // Dưới đây là một ví dụ cách load lại scene chính
        PlayerPrefs.SetString("nextScene", "maingame");
        SceneManager.LoadScene("Loading");
    }

    void PositionCamera()
    {
        // Tính toán vị trí trung tâm của tất cả các cubes
        float centerX = (numberOfCubes - 1) / 2f * .16f; // Tọa độ x của trung tâm
        Vector3 centerPosition = new Vector3(centerX, 0.5f, 0f);

        // Đặt camera nhìn vào vị trí trung tâm
        mainCamera.transform.position = new Vector3(centerX, 0.5f, 2f); // Điều chỉnh vị trí này cho phù hợp
        mainCamera.transform.LookAt(centerPosition);
    }
}
