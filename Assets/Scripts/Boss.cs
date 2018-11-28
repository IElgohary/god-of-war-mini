using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public float range = 5.2f;
    public float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private BoxCollider[] weaponColliders;



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
            anim.Play("Basic Attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(attack());
    }
}
