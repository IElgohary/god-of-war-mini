using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : MonoBehaviour
{
    [Tooltip("Colliders that take damage in the boss.")]
    public Collider[] DamageTaker;
    [Tooltip("Whether or not the boss is alive.")]
    public bool isAlive;
    [Tooltip("Current health of the boss.")]
    public int currentHealth;
    [Tooltip("is boss stunend?")]
    public bool isStunned = false;

    private int startingHealth = 200;
    private float timeSinceLastHit = 3f;
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
        if(timer >= timeSinceLastHit && !GameManager.instance.gameOver && !isStunned) {
            if(other.tag == "PlayerWeapon") {
                takeHit(GameManager.instance.damage);
                timer = 0f;
            }
        }
    }

    void takeHit(int amount) {
        if ( !gameObject.GetComponent<BossAttack>().isAttacking )
            anim.Play("Get Hit");
        if (currentHealth > 0) {
            currentHealth -= amount;
        }

        if(currentHealth <= 0){
            isAlive = false;
            KillBoss();
        }
    }

    public void weakPoint() {
        StartCoroutine(stun());
    }

    void KillBoss() {
        nav.enabled = false;
        anim.Play("Die");
        anim.SetBool("isStunned", true);
        StartCoroutine(removeBoss());
    }

    IEnumerator removeBoss(){
        yield return new WaitForSeconds(6);
        dissapearEnemy = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    IEnumerator stun(){

        anim.SetBool("isStunned", true);

        isStunned = true;
        try
        {
            GameObject.FindGameObjectWithTag("Flames").SetActive(false);
        }
        catch (System.Exception ex) {}

        yield return new WaitForSeconds(8);
        anim.SetBool("isStunned", false);
        isStunned = false;
        yield return new WaitForSeconds(0.1f);

    }

}
