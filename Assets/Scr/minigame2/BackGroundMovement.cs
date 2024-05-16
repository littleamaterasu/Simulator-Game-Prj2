using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
public class BackGroundMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float moveRange = 3.0f;
    private Vector3 oldPosition;
    private GameObject obj;
    void Start()
    {
        obj = gameObject;
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.Translate(new Vector3(-1  * Time.deltaTime * moveSpeed, transform.position.y, 0));
        if(Vector3.Distance(oldPosition, obj.transform.position) > moveRange)
        {
            obj.transform.position = oldPosition;
        }
    }
}
