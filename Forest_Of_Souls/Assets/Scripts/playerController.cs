using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //chracter movement variables
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float slowDownSpeed;
    Rigidbody2D rigidBody;
    private int direction; //1 = left, 2 = right, 3 = up, 4 = down
    private bool facing;


	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        facing = true;
	}
	
	// Update is called once per frame
	void Update () {
        //flip player to face the direction they are moving
        Flip();

        //player movement
        if (Input.GetKey(KeyCode.W)) //move player up 
        {
            rigidBody.velocity = new Vector2(0, moveSpeed);
            direction = 3;
        }
        if (Input.GetKey(KeyCode.S)) // move player down 
        {
            rigidBody.velocity = new Vector2(0, -moveSpeed);
            direction = 4;
        }
        if (Input.GetKey(KeyCode.A)) //move player left 
        {
            rigidBody.velocity = new Vector2(-moveSpeed, 0);
            direction = 1;
        }
        if (Input.GetKey(KeyCode.D)) //move player right
        {
            rigidBody.velocity = new Vector2(moveSpeed, 0);
            direction = 2;
        }
        if (!(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))) //stop player 
        {
            SlowDown();
        }
    }

    private void SlowDown()
    {
        rigidBody.velocity = new Vector2((rigidBody.velocity.x * slowDownSpeed),(rigidBody.velocity.y * slowDownSpeed));
    }
    void Flip()
    {
        if (direction == 1 && ! facing || direction == 2 && facing) // need to flip the way the chracter is facing 
        {
            facing = !facing;
            //flip player using localScale
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
