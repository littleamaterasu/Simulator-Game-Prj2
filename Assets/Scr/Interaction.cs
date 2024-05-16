﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Interaction : MonoBehaviour
{
    // Tốc độ di chuyển
    public float moveSpeed = 5f;
    public Vector3 movingPos;
    bool startMoving = false;
    public Task1 target;
    public string[] noTaskDialogue;
    public string[] taskDialogue;
    public List<string> Notice;
    public TMP_Text text;
    public bool isTalking = false;
    public bool isSwitch = false;
    public int currentDialogue;
    public int item = 0;
    public int exp = 0;
    public bool isMeeting = true;
    public Button no;
    public Button yes;
    public string minigame;
    public Rigidbody rb;
   
    private void Start()
    {
        currentDialogue = 0;
        yes.onClick.AddListener(Yes);
        no.onClick.AddListener(No);
        yes.gameObject.SetActive(false);
        no.gameObject.SetActive(false);
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

    void Yes()
    {
        no.gameObject.SetActive(false);
        yes.gameObject.SetActive(false);
        if(item >= target.require)
        {
            isSwitch = true;
            item -= target.require;
            Notice.Add("Lost " + target.require.ToString());
            if (Notice.Count > 2)
            {
                Notice.RemoveAt(0);
            }
        }
        else
        {
            Notice.Add("Not enough items");
            if (Notice.Count > 2)
            {
                Notice.RemoveAt(0);
            }
        }
        CancelDialogue();
    }


    void No()
    {
        no.gameObject.SetActive(false);
        yes.gameObject.SetActive(false);
        CancelDialogue();
    }

    void TryCommunicate()
    {
        if (target != null && Vector3.Distance(target.transform.position, transform.position) <= 2)
        {
            Demo_npc tar = target.GetComponent<Demo_npc>();
            if (tar != null)
            {
                tar.speed = 0;
            }
            Dialogue();
        }
    }

    // Không nói chuyện và để text là rỗng
    void CancelDialogue()
    {
        isTalking = false;
        currentDialogue = 0;
        text.text = "";

        Demo_npc tar = target.GetComponent<Demo_npc>();

        if(tar != null)
        {
            tar.speed = 2f;
        }

        if (!target.inTask && tar == null)
        {
            target.Reward();
        }
        target = null;
    }
    
    void Dialogue()
    {
        string[] dialogue;
        if (target.inTask == false)
        {
            // Nếu không ở trong nhiệm vụ thì nói như thường
            dialogue = noTaskDialogue;
        }
            // Nếu trong nhiệm vụ thì nói khác
        else dialogue = taskDialogue;
        
        // Nếu kết thúc đoạn hội thoại thì dừng
        if (currentDialogue >= dialogue.Length)
        {
            if(target.inTask == false)
            {

                CancelDialogue();
                isMeeting = false;
                return;
            }
            
            text.text = "Continue?";
            no.gameObject.SetActive(true);
            yes.gameObject.SetActive(true);
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

    void SetMovingPos(Vector3 pos)
    {
        movingPos.x = pos.x;
        movingPos.y = transform.position.y;
        movingPos.z = pos.z;
    }

    // Hàm di chuyển tới một điểm với di chuyển mượt mà

    void MoveTo(Vector3 targetPosition)
    {
        // Check if the mouse click is not over a UI element
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Calculate the new position
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Move the player to the new position
            transform.position = newPosition;

            // Change the player's facing direction
            if (newPosition != targetPosition)
            {
                transform.LookAt(targetPosition);
            }
        }
    }
}