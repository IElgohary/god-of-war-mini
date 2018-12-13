using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthSlider;

    
    public float timeSinceLastHit = 2f;
    private float timer = 0f;
    private Animator anim;
    public int currentHealth;
    private AudioSource [] audios;
    private CapsuleCollider collider;
    private Rigidbody rig;
    public bool shield;
    public int maxHealth = 100;

    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        audios = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
        //currentHealth = 100;
        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            healthSlider.value = ((float)currentHealth) / maxHealth;
            shield = Input.GetKey(KeyCode.LeftControl);
            if (shield)
            {
                anim.Play("Shield");
            }
            if(transform.position.y < -5) {
                currentHealth = 0;
                GameManager.instance.IsGameOver(currentHealth);
            }
            timer += Time.deltaTime;
        }
    }

    public void Heal(){
        if (currentHealth < maxHealth) StartCoroutine(HealGradually());
    }

    IEnumerator HealGradually() {
        yield return new WaitForSeconds(0.1f);
        currentHealth += 1;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        if (currentHealth < maxHealth) StartCoroutine(HealGradually());
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Boss" || collision.gameObject.tag == "enemy")
        {
            rig.isKinematic = true;
            rig.velocity = Vector3.zero;
            rig.isKinematic = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //to be modified after taggs are set for all bosses
        if (timer >= timeSinceLastHit && !GameManager.instance.gameOver){

            if (other.tag == "DamageDoer" || other.tag == "Flames")
            {
                takeHit(shield);

                timer = 0;
            }
        }

    }

    void takeHit(bool Shield)
    {
        if (currentHealth > 0 && !shield)
        {
            anim.Play("Hurt");
            currentHealth -= 10;
            audios[0].Play();
        }
        if (currentHealth <= 0)
        {
            killPlayer();
        }
        GameManager.instance.IsGameOver(currentHealth);
    }

    void killPlayer()
    {

        audios[1].Play();
        GameObject.FindGameObjectWithTag("Game UI").GetComponent<GameUI>().To_GameOver();

        anim.SetTrigger("herodie");
        rig.isKinematic = false;

    }
}