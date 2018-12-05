﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeHealth : MonoBehaviour
{
    [Tooltip("Colliders that take damage in the Melee Enemy.")]
    public Collider DamageTaker;
    [Tooltip("Whether or not the boss is alive.")]
    public bool isAlive;
    [Tooltip("Current health of the boss.")]
    public int currentHealth;

    private int startingHealth = 50;
    private float timeSinceLastHit = 1f;
    private float dissapearSpeed = 2f;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private Rigidbody rigidBody;
    private bool dissapearEnemy = false;


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(dissapearEnemy) {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(timer >= timeSinceLastHit && !GameManager.instance.gameOver) {
            if(other.tag == "PlayerWeapon") {
                takeHit();
                timer = 0f;
            }
        }
    }

    void takeHit() {
        if(currentHealth > 0) {
            currentHealth -= 10;
        }

        if(currentHealth <= 0){
            isAlive = false;
            KillMelee();
        }
    }



    void KillMelee() {
        nav.enabled = false;
        anim.Play("Die");

        StartCoroutine(removeBoss());
    }

    IEnumerator removeBoss(){
        yield return new WaitForSeconds(6);
        dissapearEnemy = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }



}
