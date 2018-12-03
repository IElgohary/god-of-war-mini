using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour {
    [Tooltip("Attack range of the boss.")]
    public float range = 5.2f;
    [Tooltip("Time (in seconds) to wait between attacks.")]
    public float timeBetweenAttacks = 2f;
    [Tooltip("Weapon colliders of the boss.")]
    public Collider[] weaponColliders;
    
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private string[] attacks = { "Basic Attack", "Claw Attack", "Flame Attack"};
    private BossHealth bossHealth;
    private GameObject flames;


    // Use this for initialization
    void Start () {
        flames = GameObject.FindGameObjectWithTag("Flames");
        player = GameManager.instance.player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
        bossHealth = GetComponent<BossHealth>();
        flames.SetActive(false);
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
        if(playerInRange && !GameManager.instance.gameOver && bossHealth.isAlive){
            int attackIndex = Random.Range(0, 3);
            anim.Play(attacks[attackIndex]);
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
        yield return null;
        StartCoroutine(attack());
    }


    public void StartFlames() {
        flames.SetActive(true);
    }

    public void EndFlames()
    {
        flames.SetActive(false);
    }

    private void rotateTowards(Transform player) {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }
}
