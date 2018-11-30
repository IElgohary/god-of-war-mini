﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour {
    [Tooltip("Instance of the player.")]
    public Transform player;
    [Tooltip("Distance between enemy and player.")]
    public float offset;
    private NavMeshAgent nav;
    private Animator anim;
    private BossHealth bossHealth;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        bossHealth = GetComponent<BossHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameManager.instance.gameOver && bossHealth.isAlive){
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            float distance = Vector3.Distance(player.position, transform.position);
            nav.SetDestination(player.position);
            if (distance > offset)
            {
                anim.SetBool("IsWalking", true);
                nav.enabled = true;
            }
            else
            {
                anim.SetBool("IsWalking", false);
                nav.enabled = false;
            }
        } 

	}
}
