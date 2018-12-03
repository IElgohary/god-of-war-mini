using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakPoint : MonoBehaviour {
    [Tooltip("Name of the attack related to this weakpoint.")]
    public string attackName;

    private BossHealth bossHealth;
    private BossAttack bossAttack;
    private float timeSinceLastHit = 10f;
    private float timer = 0f;

    // Use this for initialization
    void Start () {
        bossHealth = GameObject.FindWithTag("Boss").GetComponent<BossHealth>();
        bossAttack = GameObject.FindWithTag("Boss").GetComponent<BossAttack>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.gameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                timer = 0f;
                bossHealth.weakPoint();
                bossAttack.disableAttack(attackName);
                Destroy(gameObject);

            }
        }
    }
}
