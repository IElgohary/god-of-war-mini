using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [Tooltip("Game manages instance.")]
    public static GameManager instance = null;
    [Tooltip("Instance of the player.")]
    public GameObject player;
    [Tooltip("A boolean specifying whether the game is over.")]
    public bool gameOver = false;
    [Tooltip("Kratos current damage points.")]
    public int damage = 0;
    [Tooltip("is Kratos Hitting a weak point?")]
    public bool hittingWeakPoint = false;
    [Tooltip("Array of SpawnPoints")]
    public GameObject[] spawnPoints;
    [Tooltip("Ranger enemy.")]
    public GameObject ranger;
    [Tooltip("Orc enemy")]
    public GameObject orc;

    public float generatedSpawnTime = 1;
    public float currentSpawnTime = 0;

    private GameObject newEnemy;
    private List<MeleeHealth> meleeEnemies = new List<MeleeHealth>();
    private int health;
    private int xp;
    private int Xp;
    private int skillPoints;
    private int prevXp;
    private int newXp;
    private int level;
    private int maxhealth;
    private int currhealth;

    void Awake(){

        if(instance == null){
            instance = this;
        } else if(instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Check how much damage Kratos currently inflicts 
        if(!hittingWeakPoint){
            damage = player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().GetDamage();
        } else {
            damage = 40;
        }
	}

    public void EnemyDead() {
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().updateXP();
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().updateRage();
    }

    public void HealKratos(){
        player.GetComponent<PlayerHealth>().Heal();
    }

    public void IsGameOver(int currentHP){
        if(currentHP <= 0) {
            gameOver = true;
        } else {
            gameOver = false;
        }
    }
    public void getState()
    {
        
        maxhealth = player.GetComponent<PlayerHealth>().maxHealth;
        currhealth = player.GetComponent<PlayerHealth>().currentHealth;
        level =player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().level;
        newXp=player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().newXP;
        prevXp=player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().PrevXP;
        skillPoints=player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().skillPoints;
        Xp=player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().XP;
       
    }
    public void setState()
    {
        player.GetComponent<PlayerHealth>().maxHealth = maxhealth;
        player.GetComponent<PlayerHealth>().currentHealth = currhealth;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().level = level;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().newXP = newXp ;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().PrevXP = prevXp;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().skillPoints = skillPoints;
        player.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().XP = Xp;

    }
}
