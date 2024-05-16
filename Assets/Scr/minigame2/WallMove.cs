using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 1.0f;
    private float oldPosition;
    private GameObject obj;
    public float minY;
    public float maxY;
    void Start()
    {
        obj = gameObject;
        oldPosition = 10;
        minY = -1;
        maxY = 1;
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.Translate(new Vector3(-1  * Time.deltaTime * moveSpeed, 0, 0));

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("reset")){
            obj.transform.position = new Vector3(oldPosition, Random.Range(minY,maxY +1), 0);
        }        
    }
}