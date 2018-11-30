using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;


public class follower : MonoBehaviour {
	public Transform target;
	public float smoothing = 5f;
	Vector3 offset;
	// Use this for initialization
	void Awake(){
		Assert.IsNotNull(target);
	}
	void Start () {
		offset=  transform.position-target.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetCampos = target.position+offset;
		transform.position = Vector3.Lerp(transform.position,targetCampos,smoothing*Time.deltaTime);

	}
}
