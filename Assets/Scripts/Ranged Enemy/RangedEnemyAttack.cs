using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [Tooltip("Attack range of the boss.")]
    public float range = 5.2f;
    [Tooltip("Time (in seconds) to wait between attacks.")]
    public float timeBetweenAttacks = 2f;
    public Transform fireLocation;
    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private string[] attacks = { "Ranged Attack"};
    private BossHealth enemyHealth;
    public GameObject arrow ;
    Vector3 pos ;
    Quaternion rot ;
    // Use this for initialization
    void Start()
    {
    	pos = new Vector3(fireLocation.position.x,fireLocation.position.y+0.8f,fireLocation.position.z);
    	rot = Quaternion.Euler(gameObject.transform.rotation.x+90,gameObject.transform.rotation.y, gameObject.transform.rotation.z);
    	
    	// arrow = GameObject.instance.Arrow;
        player = GameManager.instance.player;
        anim = GetComponent<Animator>();
        StartCoroutine(attack());
        enemyHealth = GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
    	pos = new Vector3(fireLocation.position.x,fireLocation.position.y+0.8f,fireLocation.position.z);
    	rot = Quaternion.Euler(gameObject.transform.rotation.x+90,gameObject.transform.rotation.y, gameObject.transform.rotation.z);
        
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
            anim.SetBool("playerInRange",true);
            rotateTowards(player.transform);
        }
        else
        {
            playerInRange = false;
            anim.SetBool("playerInRange",false);
        }



    }

    IEnumerator attack()
    {
        if (playerInRange  && !GameManager.instance.gameOver && enemyHealth.isAlive)
        {
            int attackIndex = Random.Range(0, attacks.Length);
            anim.Play(attacks[attackIndex]);

            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(attack());
    }


    // public void disableAttack(string name)
    // {
    //     string[] tmp = new string[attacks.Length - 1];
    //     int idx = 0;
    //     foreach (string attackName in attacks)
    //     {
    //         if (!attackName.Equals(name))
    //         {
    //             if (idx >= tmp.Length)
    //             {
    //                 break;
    //             }
    //             tmp[idx] = attackName;
    //             idx++;
    //         }
    //     }
    //     attacks = tmp;
    // }

    private void rotateTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
    }
    public void FireArrow ()
    {
        GameObject newArrow = Instantiate (arrow ) as GameObject ;
        newArrow.transform.position =  pos;
        newArrow.transform.rotation = rot;
        // new Vector3(gameObject.transform.rotation.x+90,gameObject.transform.rotation.y,gameObject.transform.rotation.z);
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25;
    }
}
