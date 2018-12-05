﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    [Tooltip("Attack range of the Melee Enemy.")]
    public float range = 2.2f;
    [Tooltip("Time (in seconds) to wait between attacks.")]
    public float timeBetweenAttacks = 2f;
    [Tooltip("Weapon colliders of the Melee Enemy.")]
    public Collider[] weaponColliders;
    
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private string[] attacks = { "Sword Attack", "Sword&Sheild Attack"};
    private MeleeHealth MeleeHealth;


    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
        MeleeHealth = GetComponent<MeleeHealth>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, player.transform.position) < range){
            playerInRange = true;
            rotateTowards(player.transform);
        } else {
            playerInRange = false;
        }
        
	}

    IEnumerator attack(){
        if(playerInRange && attacks.Length > 0 && !GameManager.instance.gameOver && MeleeHealth.isAlive){
            int attackIndex = Random.Range(0, attacks.Length);
            anim.Play(attacks[attackIndex]);

            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(attack());
    }




    public void StartSword() {
        weaponColliders[0].enabled = true;
    }

    public void StopSword()
    {
        weaponColliders[0].enabled = false;
    }

    public void StartSwordandSheild() {
        weaponColliders[1].enabled = true;
    }

    public void StopSwordandSheild()
    {
        weaponColliders[1].enabled = false;
    }

    public void disableAttack(string name){
        string[] tmp = new string[attacks.Length-1];
        int idx = 0;
        foreach (string attackName in attacks)
        {
            if(!attackName.Equals(name)){
                if(idx >= tmp.Length){
                    break;
                }
                tmp[idx] = attackName;
                idx++;
            }
        }
        attacks = tmp;
    }

    private void rotateTowards(Transform player) {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }

}
