using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-10*Input.GetAxis("Horizontal") * Time.deltaTime, 0,-10*Input.GetAxis("Vertical") * Time.deltaTime);
	}
}
