using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //chracter movement variables
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float slowDownSpeed;
    Rigidbody2D rigidBody;
    Vector2 targetPosition;
    Vector3 facing;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        //player movement and facing direction
       // LookAtMouse();  
        if (Input.GetKey(KeyCode.W)) //move player up 
        {
            MovePlayer();
        }
        if (!(Input.GetKey(KeyCode.W))) //stop player 
        {
            SlowDown();
        }
    }

    private void SlowDown()
    {
        rigidBody.velocity = new Vector2((rigidBody.velocity.x * slowDownSpeed),(rigidBody.velocity.y * slowDownSpeed));
    }

    //player looks towards mouse at all times 
    private void LookAtMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward); //problem here .forward is the red axis, not the green
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }

    private void MovePlayer() //move player forward towards the mouse 
    { 
        transform.position = Vector2.MoveTowards(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed * Time.deltaTime);
    }
}
