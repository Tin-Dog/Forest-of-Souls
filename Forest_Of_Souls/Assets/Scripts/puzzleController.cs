using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleController : MonoBehaviour {

    public sunScript[] sunTiles;
    public moonSwitch[] moonTiles;
    public Sprite unsolved;
    public Sprite moons;
    public Sprite suns;
    public Sprite sunandmoon;
    public bool solved = false;
    public bool sun = false;
    public bool moon = false;

	// Use this for initialization
	void Start () {
	}
	
    bool checkSun()
    {
        for(int i=0; i < sunTiles.Length; i++)
        {
            if (sunTiles[i].isActive == 0)
                return false;
        }
        return true;
    }

    bool checkMoon()
    {
        for (int i = 0; i < moonTiles.Length; i++)
        {
            if (moonTiles[i].isActive == 0)
                return false;
        }
        return true;
    }

    void changeSprite()
    {
        if (solved)
        {
            GetComponent<SpriteRenderer>().sprite = sunandmoon;
        }
        else if (sun)
            GetComponent<SpriteRenderer>().sprite = suns;
        else if (moon)
            GetComponent<SpriteRenderer>().sprite = moons;
        else
            GetComponent<SpriteRenderer>().sprite = unsolved;

    }

    // Update is called once per frame
    void Update () {
        if (checkSun())
            sun = true;
        else
            sun = false;
        if (checkMoon())
            moon = true;
        else
            moon = false;
        if (sun && moon)
            solved = true;

        changeSprite();
    }


}
