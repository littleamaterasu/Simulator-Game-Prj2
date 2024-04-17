using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CameraSwitchManager.cs
public class CameraSwitchManager : MonoBehaviour
{
    public CameraRoam roamCamera; // The roaming camera
    public CameraFollow followCamera; // The following camera
    private bool isFollowing; // Flag to track the current camera

    void Start(){
        // Enable the roaming camera and disable the following camera
        roamCamera.enabled = false;
        followCamera.enabled = true;
        isFollowing = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Switch the camera
            isFollowing = !isFollowing;

            // Enable the appropriate camera and disable the other one
            followCamera.enabled = isFollowing;
            roamCamera.enabled = !isFollowing;
        }
    }
}
