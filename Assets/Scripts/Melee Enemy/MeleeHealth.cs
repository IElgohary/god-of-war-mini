using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeHealth : MonoBehaviour
{
    [Tooltip("Colliders that take damage in the Melee Enemy.")]
    public Collider DamageTaker;
    [Tooltip("Whether or not the enemy is alive.")]
    public bool isAlive;
    [Tooltip("Current health of the enemy.")]
    public int currentHealth;

    private int startingHealth = 50;
    private float timeSinceLastHit = 1f;
    private float dissapearSpeed = 2f;
    private float timer = 0f;
    private Animator anim;
    private NavMeshAgent nav;
    private Rigidbody rigidBody;
    private bool dissapearEnemy = false;

    public GameObject fallingDmg;

    // Use this for initialization
    void Start()
    {
        GameManager.instance.RegisterMeleeEnemy (this) ;
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
                takeHit(GameManager.instance.damage);
                timer = 0f;
            }
        }
    }

    void takeHit(int amount)
    {
        if(fallingDmg != null){
            GameObject instance = Instantiate(fallingDmg, transform);
            instance.GetComponent<FallingDmg>().SetText(amount.ToString());
        }

        anim.Play("Get Hit");
        if (currentHealth > 0)
        {
            currentHealth -= amount;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy() {
        GameManager.instance.KilledMeleeEnemy(this) ;
        nav.enabled = false;
        anim.Play("Die");

        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy(){
        yield return new WaitForSeconds(3);
        dissapearEnemy = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }



}
