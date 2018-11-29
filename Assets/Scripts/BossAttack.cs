using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    public float range = 5.2f;
    public float timeBetweenAttacks = 2f;
    public int currentHealth = 200;
    
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;
    private string[] attacks = { "Basic Attack", "Claw Attack", "Flame Attack"}; 


	// Use this for initialization
	void Start () {

        weaponColliders = GetComponentsInChildren<BoxCollider>();
        player = GameManager.instance.player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
	}
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(transform.position, player.transform.position) < range){
            playerInRange = true;
        } else {
            playerInRange = false;
        }
        
	}

    IEnumerator attack(){
        if(playerInRange && !GameManager.instance.gameOver){
            int attackIndex = Random.Range(0, 3);
            anim.Play(attacks[attackIndex]);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(attack());
    }
}
