using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonFireScript : MonoBehaviour {

    //lighting variables
    [SerializeField]
    private float speed;
    [SerializeField]
    private float brightness; //between 0 and 8 (highest light)
    [SerializeField]
    private float dimmness; //between 0 and 8 (lowest light)

    public Light light; //added in the inspector

    private void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
        float phi = Time.time / speed * 2 * Mathf.PI;
        float amplitude = Mathf.Cos(phi) * dimmness + brightness;
        light.intensity = amplitude;
	}
}
