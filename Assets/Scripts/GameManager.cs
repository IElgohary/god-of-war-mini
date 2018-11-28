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
		
	}

    public void PlayerHit(int currentHP){
        if(currentHP > 0) {
            gameOver = false;
        } else {
            gameOver = true;
        }
    }
}
