using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour {
    [Tooltip("Name of the attack related to this weakpoint.")]
    public string attackName;
    [Tooltip("number of hits to a weakpoint before it's destroyed.")]
    public int weakPtsCount = 3;
    private BossHealth bossHealth;
    private BossAttack bossAttack;
    private Animator anim;
    private float timeSinceLastHit = 3f;
    private float timer = 0f;
    private int timesHit = 0;

    // Use this for initialization
    void Start () {
        bossHealth = GameObject.FindWithTag("Boss").GetComponent<BossHealth>();
        bossAttack = GameObject.FindWithTag("Boss").GetComponent<BossAttack>();
        anim = GameObject.FindWithTag("Boss").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(delay());
        GameManager.instance.hittingWeakPoint = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.gameOver && !bossHealth.isStunned)
        {
            if (other.tag == "PlayerWeapon")
            {
                Debug.Log(attackName);
                timer = 0f;
                timesHit++;
                bossAttack.disableFlames();
                if (timesHit == weakPtsCount){
                    GameManager.instance.hittingWeakPoint = true;
                    bossHealth.weakPoint();
                    bossAttack.disableAttack(attackName);
                    Destroy(gameObject);

                }
            }
        }
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(0.2f);
    }
}
