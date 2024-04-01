using System.Threading;
using UnityEngine;

public class Demo_npc : Task1
{
    public float speed = 0f;
    public float changeDirectionInterval = .75f;
    public float wallStopDuration = 0.1f; // Thời gian dừng khi đâm vào tường
    public float npcShortStopDuration = 0.1f; // Thời gian dừng ngắn khi đâm vào NPC
    public float npcLongStopDuration = 1.0f; // Thời gian dừng dài khi đâm vào NPC
    public char c;
    private float timer;
    private Vector3 randomDirection;
    private Rigidbody rb;

    private Vector3 startingPosition; // Vị trí ban đầu của NPC

    private void Start()
    {
        timer = changeDirectionInterval;
        GetNewRandomDirection();
        rb = GetComponent<Rigidbody>();

        // Lưu vị trí ban đầu của NPC khi scene bắt đầu
        startingPosition = transform.position;
    }

    private void Update()
    {
        // Di chuyển NPC
        rb.MovePosition(transform.position + randomDirection * speed * Time.deltaTime);

        // Cập nhật đồng hồ
        timer -= Time.deltaTime;

        // Nếu đến lúc thay đổi hướng, chọn hướng mới
        if (timer <= 0)
        {
            GetNewRandomDirection();
            timer = changeDirectionInterval;
        }
    }

    void GetNewRandomDirection()
    {
        // Tạo hướng ngẫu nhiên
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Xác định loại va chạm
        if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine(StopForDuration(wallStopDuration, -randomDirection)); // Dừng lại và đi theo hướng ngược lại
        }
        else if (collision.gameObject.CompareTag("NPC"))
        {
            // Xác suất dừng dài hơn
            float stopProbability = Random.value;

            if (stopProbability <= 0.25f)
            {
                StartCoroutine(StopForDuration(npcLongStopDuration, -randomDirection)); // Dừng lại và đi theo hướng ngược lại
            }
            else
            {
                StartCoroutine(StopForDuration(npcShortStopDuration, -randomDirection)); // Dừng lại và đi theo hướng ngược lại
            }
        }
    }

    System.Collections.IEnumerator StopForDuration(float duration, Vector3 reverseDirection)
    {
        speed = 0f; // Dừng di chuyển
        yield return new WaitForSeconds(duration); // Chờ cho đến khi hết thời gian
        speed = .2f; // Khôi phục tốc độ
        GetNewRandomDirection(); // Chọn hướng mới
    }

    // Lưu vị trí của NPC khi chuyển scene
    public void SavePosition()
    {
        PlayerPrefs.SetFloat("NPC_Position_X" + c, transform.position.x);
        PlayerPrefs.SetFloat("NPC_Position_Y" + c, transform.position.y);
        PlayerPrefs.SetFloat("NPC_Position_Z" + c, transform.position.z);
    }

    // Khôi phục vị trí của NPC khi load lại scene
    public void LoadPosition()
    {
        float x = PlayerPrefs.GetFloat("NPC_Position_X" + c, startingPosition.x);
        float y = PlayerPrefs.GetFloat("NPC_Position_Y" + c, startingPosition.y);
        if (y == 0) return;
        float z = PlayerPrefs.GetFloat("NPC_Position_Z" + c, startingPosition.z);
        transform.position = new Vector3(x, y, z);
    }

}
