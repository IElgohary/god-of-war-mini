using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public Slider healthSlider;
    public int startingHealth = 100;
    public float timeSinceLastHit = 2f;
    private float timer = 0f;
    private Animator anim;
    public int currentHealth;
    private AudioSource audio;
    private CapsuleCollider collider;
    private Rigidbody rig;
    public bool shield;
    // Use this for initialization
    void Start()
    {
        collider = gameObject.GetComponent<CapsuleCollider>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        rig = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            shield = Input.GetKey(KeyCode.LeftControl);
            if (shield)
            {
                anim.Play("Shield");
            }

            timer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //to be modified after taggs are set for all bosses
        if (timer >= timeSinceLastHit && !GameManager.instance.gameOver)
            

            if (other.tag == "Weapon")
            {
                takeHit(shield);
                timer = 0;
            }

        if (other.tag == "Chest")
        {
            currentHealth = 100;
        }

    }
    private void FixedUpdate()
    {
       
    }
    void takeHit(bool Shield)
    {
        
        

        if (currentHealth > 0 &&!shield)
        {

            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            audio.PlayOneShot(audio.clip);
           // healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            killPlayer();

        }

    }
    void killPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        collider.enabled = false;
        anim.SetTrigger("herodie");
        rig.isKinematic = false;
        rig.useGravity = false;
        //
     //  characterController.enabled = false;
    }
}