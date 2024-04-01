using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    // Tốc độ di chuyển
    public float moveSpeed = 5f;
    public Vector3 movingPos;
    bool startMoving = false;
    public Task1 target;
    public string[] dialogue;
    public TMP_Text text;
    public bool isTalking = false;
    public int currentDialogue;
    public bool isMeeting = true;

    private void Start()
    {
        currentDialogue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu người dùng nhấn chuột trái
        if (!isTalking && Input.GetMouseButtonDown(0))
        {
            startMoving = true;

            // Tạo ra một ray từ vị trí của chuột
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Kiểm tra va chạm với đối tượng
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
                // Nếu hitpoint có y < 0
                if (hit.point.y > 0.5f)
                {
                    // Lấy điểm mục tiêu trên mặt phẳng Oxz
                    target = hit.collider.GetComponent<Task1>();
                    Vector3 targetPoint;
                    if (target != null) 
                    {
                        targetPoint = new Vector3(target.transform.position.x, 0, target.transform.position.z); 
                    }
                    else 
                    { 
                        targetPoint = new(hit.point.x, 0, hit.point.z); 
                    }
                    Vector3 currentPoint = new Vector3(transform.position.x, 0, transform.position.z);

                    float d = Vector3.Distance(currentPoint, targetPoint);
                    Vector3 movingPoint = new Vector3((float)((currentPoint.x * 1.5 + targetPoint.x * (d - 1.5)) / d), 0, (float)((currentPoint.z * 1.5 + targetPoint.z * (d - 1.5)) / d));

                    // Di chuyển tới điểm đó
                    SetMovingPos(movingPoint);
                }
                else
                {
                    // Di chuyển trực tiếp tới hitpoint
                    SetMovingPos(hit.point);
                    target = null;
                }
            }
        }
        if (startMoving) MoveTo(movingPos);
        TryCommunicate();
    }

    void TryCommunicate()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) <= 2)
        {
            
            Dialogue();
        }
    }

    // Không nói chuyện và để text là rỗng
    void CancelDialogue()
    {
        isTalking = false;
        currentDialogue = 0;
        text.text = "";
        isMeeting = false;
        target = null;
    }
    
    void Dialogue()
    {
        // Nếu kết thúc đoạn hội thoại thì dừng
        if (currentDialogue >= dialogue.Length)
        {
            /*if(target != null && target.GetComponent<Demo_npc>() == null)
            {
                SceneManager.LoadScene("minigame_1");
            }*/
            CancelDialogue();
            return;
        }

        // Đang nói chuyện
        isTalking = true;

        // Chuyển dần các câu thoại
        text.text = dialogue[currentDialogue];
        if (Input.GetMouseButtonDown(0))
        {
            currentDialogue++;
        }
    }

    void StartMinigame()
    {

    }

    void SetMovingPos(Vector3 pos)
    {
        movingPos.x = pos.x;
        movingPos.y = transform.position.y;
        movingPos.z = pos.z;
    }

    // Hàm di chuyển tới một điểm với di chuyển mượt mà
    void MoveTo(Vector3 targetPoint)
    {
        isMeeting = true;
        // Calculate the direction from current position to the target position
        Vector3 direction = targetPoint - transform.position;

        // Normalize the direction to get a unit vector
        direction.Normalize();

        // Move the object towards the target point
        if(Vector3.Distance(transform.position, targetPoint) >= 0.2f) transform.position += direction * Time.deltaTime * moveSpeed; // Assuming 'speed' is a variable representing the movement speed
    }
}
