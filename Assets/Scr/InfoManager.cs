using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    RoadNPC roadNPC;

    void CheckInfo()
    {
        // Tạo một ray từ điểm chuột vào không gian thế giới
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<RoadNPC>() != null)
            {
                if(roadNPC != null)
                {
                    roadNPC.HideInfo();
                }
                roadNPC = hit.collider.GetComponent<RoadNPC>();
                roadNPC.ShowInfo();
            }
            else
            {
                if(roadNPC != null) 
                {
                    roadNPC.HideInfo();
                }
                roadNPC = null;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckInfo();
    }
}
