using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoam : MonoBehaviour
{

    public float camSpeed = 20;
    public float screenSizeThickness = 10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {   
        MoveCamera();
       
    }
    private void MoveCamera()
    {
    Vector3 pos = transform.position;
    //Up
    if(Input.mousePosition.y >= Screen.height - screenSizeThickness)
    {
        pos.z += camSpeed * Time.deltaTime; // Move forward in the z-axis
    }   
    //Down
    if(Input.mousePosition.y <= screenSizeThickness)
    {
        pos.z -= camSpeed * Time.deltaTime; // Move backward in the z-axis
    } 
    //Right
    if(Input.mousePosition.x >= Screen.width - screenSizeThickness)
    {
        pos.x += camSpeed * Time.deltaTime; // Move right in the x-axis
    }   
    //Left
    if(Input.mousePosition.x <= screenSizeThickness)
    {
        pos.x -= camSpeed * Time.deltaTime; // Move left in the x-axis
    }
    transform.position = pos;
    }
}

    
