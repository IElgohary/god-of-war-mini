﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyMove : MonoBehaviour
{
    [Tooltip("Instance of the player.")]
    public Transform player;
    [Tooltip("Distance between enemy and player.")]
    public float offset;

    private NavMeshAgent nav;
    private Animator anim;
    private RangedEnemyHealth enemyHealth;

    // Use this for initialization
    void Start()
    {
        player = GameManager.instance.GetPlayer().transform;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<RangedEnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver && enemyHealth.isAlive)
        {
            float distance = Vector3.Distance(player.position, transform.position);
            nav.SetDestination(player.position);
            if (distance > offset)
            {               
              //  GetComponents<AudioSource>()[2].Play();
                anim.SetBool("isWalking", true);
                nav.enabled = true;
            }
            else
            {
               // GetComponents<AudioSource>()[2].Stop();
                anim.SetBool("isWalking", false);
                nav.enabled = false;
            }
        }

    }
}
