using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public float moveSpeed = 10.0f;
	private CharacterController characterController;
	public  LayerMask layerMask;
	Vector3 currLooktarget = Vector3.zero;
	private Animator anim;
	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDirection = new Vector3 (Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));	
		characterController.SimpleMove(moveDirection*moveSpeed);
		if(moveDirection==Vector3.zero){
			anim.SetBool("IsWalking",false);

		}
		else
		{
			anim.SetBool("IsWalking",true);
			
		}
	}

	void FixedUpdate(){

		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin,ray.direction *300,Color.blue); 
		if(Physics.Raycast(ray,out hit,300,layerMask,QueryTriggerInteraction.Ignore))
		 {

			if(hit.point != currLooktarget){
				currLooktarget= hit.point;
			}

			Vector3 targetPosition = new Vector3(hit.point.x,transform.position.y,hit.point.z);
			Quaternion rotation = Quaternion.LookRotation(targetPosition-transform.position);
			transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime);

		}
	}
}
