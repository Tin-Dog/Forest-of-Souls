using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunScript : MonoBehaviour {

    public int isActive = 1;
    public Sprite unlit;
    public Sprite lit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            GetComponent<SpriteRenderer>().sprite = lit;
            isActive = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            GetComponent<SpriteRenderer>().sprite = unlit;
            isActive = 0;
        }
    }
}
