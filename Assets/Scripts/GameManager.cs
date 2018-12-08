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
}
