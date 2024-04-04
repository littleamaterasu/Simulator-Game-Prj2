using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CameraFollow.cs
public class CameraFollow : MonoBehaviour
{
    public GameObject player; // The player object
    public Vector3 offset; // Offset from the player

    void Start(){
        // Set the offset to the difference between the player's position and the camera's position
        offset = transform.position - player.transform.position;
    }
    void Update()
    {
        // Follow the player with a certain offset
        transform.position = player.transform.position + offset;
    }
}
