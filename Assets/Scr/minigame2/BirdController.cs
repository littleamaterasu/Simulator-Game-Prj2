using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject obj;
    public GameObject gameController;
    public float flyPower;
    private Rigidbody2D rb;
    void Start()
    {
        obj = gameObject;
        flyPower = 2.0f;   
        rb = obj.GetComponent<Rigidbody2D>();
        if(gameController == null){
            gameController = GameObject.FindGameObjectWithTag("GameController");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameController.GetComponent<GameController>().isGameEnd){
            if(Input.GetMouseButtonDown(0) ){
                rb.AddForce(new Vector2(0, flyPower), ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Pipe" || other.gameObject.tag == "Ground")
            EndGame();
    }

    void OnTriggerEnter2D(Collider2D other) {
        gameController.GetComponent<GameController>().AddPoint();
    }
    void EndGame(){
        gameController.GetComponent<GameController>().EndGame();
    }
}
