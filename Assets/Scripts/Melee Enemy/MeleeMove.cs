using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMove : MonoBehaviour {
    [Tooltip("Instance of the player.")]
    public Transform player;
    [Tooltip("Distance between enemy and player.")]
    public float offset;

    private NavMeshAgent nav;
    private Animator anim;
    private MeleeHealth MeleeHealth;

	// Use this for initialization
	void Start () {

        player = GameManager.instance.GetPlayer().transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        MeleeHealth = GetComponent<MeleeHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameManager.instance.gameOver && MeleeHealth.isAlive){
            float distance = Vector3.Distance(player.position, transform.position);
            nav.SetDestination(player.position);
            //GetComponents<AudioSource>()[2].Play();

            if (distance > offset)
            {
                anim.SetBool("IsWalking", true);
                nav.enabled = true;
            }
            else
            {              
               // GetComponents<AudioSource>()[2].Stop();
                anim.SetBool("IsWalking", false);
                nav.enabled = false;
            }
        } 

	}
}
