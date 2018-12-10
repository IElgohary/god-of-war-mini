using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {

	private Animator anim;
    private bool isClosed = true;
	// Use this for initialization
	void Start () {
	        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   private  void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerWeapon" && isClosed){
            anim.SetBool("Near", true);
            isClosed = false;
            GameManager.instance.HealKratos();
        }
		    
    }
	
}
