using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moonSwitch : MonoBehaviour {

    public int isActive = 0;
    public Sprite unlit;
    public Sprite lit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow")){
            GetComponent<SpriteRenderer>().sprite = lit;
            isActive = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            GetComponent<SpriteRenderer>().sprite = unlit;
            isActive = 0;
        }
    }

}
