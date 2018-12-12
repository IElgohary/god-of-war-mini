using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyHealth : MonoBehaviour
{
    [Tooltip("Colliders that take damage in the archer.")]
    public Collider[] DamageTaker;
    [Tooltip("Whether or not the archer is alive.")]
    public bool isAlive;
    [Tooltip("Current health of the archer.")]
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
        GameManager.instance.RegisterRangedEnemy (this) ;
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

        if (dissapearEnemy)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.gameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                takeHit();
                timer = 0f;
            }
        }
    }

    void takeHit()
    {
        GameObject instance = Instantiate(fallingDmg, transform);
        instance.GetComponent<FallingDmg>().SetText("10");

        if (currentHealth > 0)
        {   

            currentHealth -= 10;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    //public void weakPoint()
    //{
    //    if (currentHealth > 0)
    //    {
    //        anim.Play("Get Hit");
    //        currentHealth -= 10;
    //    }

    //    if (currentHealth <= 0)
    //    {
    //        isAlive = false;
    //        KillBoss();
    //    }
    //    stunt();
    //}

    void KillEnemy()
    {
        GameManager.instance.KilledRangedEnemy(this) ;
        nav.enabled = false;
        anim.Play("Die");

        StartCoroutine(removeEnemy());
    }

    IEnumerator removeEnemy()
    {
        yield return new WaitForSeconds(6);
        dissapearEnemy = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    //IEnumerable stunt()
    //{
    //    anim.SetTrigger("isStunned");
    //    yield return new WaitForSeconds(5);
    //    //anim.ResetTrigger("isStunned");
    //}

}
