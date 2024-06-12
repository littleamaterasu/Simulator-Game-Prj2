using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFollowObject : MonoBehaviour
{
    public Transform objectToFollow;
    public Vector3 initialOffset;

    private Vector3 originalPosition;

    void Start()
    {
        // Calculate the initial offset from the object's starting position
        if (objectToFollow != null)
        {
            originalPosition = objectToFollow.position;
            initialOffset = transform.position - originalPosition;
        }
        else
        {
            Debug.LogWarning("Object to follow is not assigned.");
        }
    }

    void Update()
    {
        if (objectToFollow != null)
        {
            // Set the position of the canvas to be the same as the position of the object plus the initial offset
            transform.position = objectToFollow.position + initialOffset;
        }
    }
}
