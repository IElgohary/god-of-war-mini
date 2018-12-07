using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
	        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   private  void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("PlayerWeapon"))
		anim.SetBool("Near",true);
    }
	
	}
