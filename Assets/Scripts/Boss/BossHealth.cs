using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    public Slider bossHP;

    private int startingHealth = 200;
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
        rigidBody = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        bossHP.value = currentHealth;
        isAlive = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            currentHealth = 0;
            KillBoss();
            bossHP.value = currentHealth;
        }

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
        if (!gameObject.GetComponent<BossAttack>().isAttacking)
        {
            GameObject instance = Instantiate(fallingDmg, transform);
            instance.GetComponent<FallingDmg>().SetText(amount.ToString());
            GameManager.instance.EnemyHit();
            anim.Play("Get Hit");
        }
        if (currentHealth > 0) {
            
            currentHealth -= amount;
            bossHP.value = currentHealth;
        }

        if(currentHealth <= 0){
            isAlive = false;
            KillBoss();
        }
    }

    public void weakPoint() {
        GameObject instance = Instantiate(fallingDmg, transform);
        instance.GetComponent<FallingDmg>().SetText("40");
        GameManager.instance.EnemyHit();
        StartCoroutine(stun());
        if (currentHealth > 0)
        {

            currentHealth -= 40;
            bossHP.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillBoss();
        }
    }

    void KillBoss() {
        nav.enabled = false;
        anim.Play("Die");
        anim.SetBool("isStunned", true);
        StartCoroutine(removeBoss());
        GameManager.instance.EnemyDead();
        GameObject.FindGameObjectWithTag("Game UI").GetComponent<GameUI>().To_Credits();
        bossHP.gameObject.SetActive(false);
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
