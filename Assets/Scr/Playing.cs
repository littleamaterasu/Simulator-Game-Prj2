using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : MonoBehaviour
{
    Vector3[] dir =
    {
        new(0, 0, 1.1f),
        new(0, 0, -1.1f),
        new(0, 1.1f, 0),
        new(0, -1.1f, 0)
    };
    RaycastHit hit;
    LayerMask mask;
    public bool doneShuffle = false;
    private void Start()
    {
        mask = 1 << 2;
        mask = ~mask;
        StartCoroutine(MoveWithDelay());
        
    }

    IEnumerator MoveWithDelay()
    {
        for (int i = 0; i < 1; i++)
        {
            int randomDirection = Random.Range(0, 4);
            MoveInDirection(dir[randomDirection]);
            yield return new WaitForSeconds(.01f);
        }
        doneShuffle = true;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveInDirection(dir[0]);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveInDirection(dir[1]);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveInDirection(dir[2]);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveInDirection(dir[3]);
        }
    }

    void MoveInDirection(Vector3 direction)
    {
        Ray ray = new(transform.position, -direction);

        if (Physics.Raycast(ray, out hit, 1.1f, mask))
        {
            // Nếu hit với một collider
            Transform hitTransform = hit.collider.transform;

            // Di chuyển object hit ngược hướng ray
            hitTransform.position += direction;

            // Di chuyển object hiện tại theo hướng ray
            transform.position -= direction;
        }
    }
}
