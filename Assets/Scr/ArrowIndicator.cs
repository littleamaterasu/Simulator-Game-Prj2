using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArrowIndicator : MonoBehaviour
{
    public GameObject npcObject;
    public Texture2D arrowTexture;
    public float arrowDistanceThreshold = 4f;
    public Material mat;
    private GameObject arrow;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Tạo một GameObject để hiển thị mũi tên
        arrow = new GameObject("Arrow");
        arrow.transform.parent = transform;
        arrow.transform.localPosition = Vector3.zero;

        // Thêm một Renderer và sử dụng Texture2D làm hình ảnh mũi tên
        var renderer = arrow.AddComponent<MeshRenderer>();
        var filter = arrow.AddComponent<MeshFilter>();
        filter.mesh = CreateArrowMesh();
        renderer.material = mat;
        renderer.material.mainTexture = arrowTexture;
    }

    private void Update()
    {
        // Kiểm tra nếu NPC không tồn tại hoặc đã bị hủy
        if (npcObject == null)
        {
            arrow.SetActive(false);
            return;
        }

        // Tính toán khoảng cách giữa người chơi và NPC
        float distance = Vector3.Distance(transform.position, npcObject.transform.position);

        // Kiểm tra nếu khoảng cách lớn hơn ngưỡng cho phép
        if (distance > arrowDistanceThreshold)
        {
            // Hiển thị mũi tên và chỉ về hướng NPC
            arrow.SetActive(true);
            arrow.transform.LookAt(npcObject.transform);
        }
        else
        {
            // Ẩn mũi tên nếu người chơi gần NPC
            arrow.SetActive(false);
        }
    }
    private Mesh CreateArrowMesh()
    {
        // Create a mesh representing the shape of an arrow
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f), // Base of the arrow
            new Vector3(-0.5f, 0f, 1f), // Left corner of the arrow head
            new Vector3(0f, 0f, 2f), // Tip of the arrow
            new Vector3(0.5f, 0f, 1f) // Right corner of the arrow head
        };
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3, 0, 3, 1 };
        mesh.RecalculateNormals();

        return mesh;
    }
    public void PointToTarget(GameObject target)
    {
        npcObject = target;
    }
}