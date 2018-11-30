using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {

    public float range = 5.2f;
    public float timeBetweenAttacks = 2f;
    
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private string[] attacks = { "Basic Attack", "Claw Attack", "Flame Attack"};
    private BossHealth bossHealth;


    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
        bossHealth = GetComponent<BossHealth>();
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
        if(playerInRange && !GameManager.instance.gameOver && bossHealth.isAlive){
            int attackIndex = Random.Range(0, 3);
            anim.Play(attacks[attackIndex]);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(attack());
    }
}
