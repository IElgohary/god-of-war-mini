using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Tooltip("Game manages instance.")]
    public static GameManager instance = null;
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
    
    public int currentLevel;
    public float generatedSpawnTime = 1;
    public float currentSpawnTime = 0;
    public bool goToLevel2 = false;
    private GameObject newEnemy;

    private List<RangedEnemyHealth> killedRangedEnemies = new List<RangedEnemyHealth>();
    private List<RangedEnemyHealth> rangedEnemies = new List<RangedEnemyHealth>();
    private List<MeleeHealth> killedMeleeEnemies = new List<MeleeHealth>();
    private List<MeleeHealth> meleeEnemies = new List<MeleeHealth>();
    public GameObject player;


    public GameObject GetPlayer(){
        return player;
    }

    public void RegisterMeleeEnemy (MeleeHealth enemy) {
        meleeEnemies.Add (enemy) ;
    }

    public void RegisterRangedEnemy (RangedEnemyHealth enemy) {
        rangedEnemies.Add (enemy) ;
    }

    public void KilledMeleeEnemy (MeleeHealth enemy) {
        killedMeleeEnemies.Add (enemy) ;
    }

    public void KilledRangedEnemy (RangedEnemyHealth enemy) {
        killedRangedEnemies.Add (enemy) ;
    }


    void Awake(){

        if(instance == null){
            instance = this;
        } else if(instance != this){
            Destroy(gameObject);
        }
        player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(spawn());
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {

        currentSpawnTime +=Time.deltaTime ;
        if(goToLevel2)
        {
            SceneManager.LoadScene("Boss Level");
            player = GameObject.FindGameObjectWithTag("Player");
            GameObject SpawnPoint = GameObject.FindGameObjectWithTag("kratosSpawn");
            GameObject cameraSpawn = GameObject.FindGameObjectWithTag("Camera");
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            player.GetComponent<Transform>().position =
                      SpawnPoint.GetComponent<Transform>().position;
            mainCamera.GetComponent<Transform>().position = cameraSpawn.GetComponent<Transform>().position;
            goToLevel2 = false;
        }
        // Check how much damage Kratos currently inflicts 
        if (!hittingWeakPoint){
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
    IEnumerator spawn () {
        if (currentSpawnTime > generatedSpawnTime) {
            currentSpawnTime = 0 ;
            if (meleeEnemies.Count < currentLevel && rangedEnemies.Count < currentLevel ) {
                int randomNumber = Random.Range(0, spawnPoints.Length - 1) ;
                GameObject spawnLocation = spawnPoints[randomNumber];
                int randomEnemy = Random.Range(0,2);
                if (randomEnemy == 0) {
                    newEnemy = Instantiate (orc) as GameObject ;
                } else  if (randomEnemy == 1 ){
                    newEnemy = Instantiate (ranger) as GameObject ;
                }
                newEnemy.transform.position = spawnLocation.transform.position;
            }
            if (killedMeleeEnemies.Count == currentLevel && killedRangedEnemies.Count == currentLevel){
                killedRangedEnemies.Clear ();
                killedMeleeEnemies.Clear () ;
                rangedEnemies.Clear () ;
                meleeEnemies.Clear () ;
                yield return new WaitForSeconds(3f);
                currentLevel++;
            }
        }
        yield return null ;
        StartCoroutine(spawn());
    }
}
